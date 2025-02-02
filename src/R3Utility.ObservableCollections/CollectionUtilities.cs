using ObservableCollections;
using R3;
using System.Collections.Specialized;

namespace R3Utility.ObservableCollections;
internal static class CollectionUtilities
{
    /// <summary>
    /// Core logic of ObserveElementXXXXX methods.
    /// </summary>
    /// <typeparam name="T">Type of element.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">source collection</param>
    /// <param name="subscribeAction">element subscribe logic.</param>
    /// <returns></returns>
    internal static Observable<TResult> ObserveElementCore<T, TResult>(IObservableCollection<T> source, Func<T, Observer<TResult>, IDisposable> subscribeAction)
        where T : notnull
    {
        return Observable.Create<TResult>(observer =>
        {
            //--- cache element property subscriptions
            var subscriptionCache = new Dictionary<T, IDisposable>();

            //--- subscribe / unsubscribe property which all elements have
            void subscribe(T element)
            {
                if (!subscriptionCache.ContainsKey(element))
                {
                    var subscription = subscribeAction(element, observer);
                    subscriptionCache.Add(element, subscription);
                }
            }

            void subscribeAll(IEnumerable<T> elements)
            {
                foreach (var x in elements)
                {
                    subscribe(x);
                }
            }

            void unsubscribeAll()
            {
                foreach (var x in subscriptionCache.Values)
                {
                    x.Dispose();
                }
                subscriptionCache.Clear();
            }

            subscribeAll(source);

            //--- hook collection changed
            var disposable = source.ObserveChanged().Subscribe(x =>
            {
                if (x.Action == NotifyCollectionChangedAction.Remove
                || x.Action == NotifyCollectionChangedAction.Replace)
                {
                    //  If the same value appears multiple times in the collection
                    //  and at least one instance still exists, do not unsubscribe.
                    if (!source.Contains(x.OldItem))
                    {
                        //--- unsubscribe
                        subscriptionCache[x.OldItem].Dispose();
                        subscriptionCache.Remove(x.OldItem);
                    }
                }

                if (x.Action == NotifyCollectionChangedAction.Add
                || x.Action == NotifyCollectionChangedAction.Replace)
                {
                    subscribe(x.NewItem);
                }

                if (x.Action == NotifyCollectionChangedAction.Reset)
                {
                    unsubscribeAll();
                    subscribeAll(source);
                }
            });

            //--- unsubscribe
            return Disposable.Create(() =>
            {
                disposable.Dispose();
                unsubscribeAll();
            });
        });
    }
}
