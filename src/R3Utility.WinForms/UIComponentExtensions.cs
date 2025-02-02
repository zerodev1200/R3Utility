using R3;

namespace R3Utility.WinForms;

public static class UIComponentExtensions
{
    // TextBox
    public static Observable<EventArgs> TextChangedAsObservable(this TextBoxBase textBox, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => textBox.TextChanged += h,
                            h => textBox.TextChanged -= h,
                            cancellationToken);
    }

    // Button
    public static Observable<EventArgs> ClickAsObservable(this Button button, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => button.Click += h,
                            h => button.Click -= h,
                            cancellationToken);
    }

    // CheckBox
    public static Observable<EventArgs> CheckedChangedAsObservable(this CheckBox checkBox, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => checkBox.CheckedChanged += h,
                            h => checkBox.CheckedChanged -= h,
                            cancellationToken);
    }

    // ComboBox
    public static Observable<EventArgs> SelectedIndexChangedAsObservable(this ComboBox comboBox, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => comboBox.SelectedIndexChanged += h,
                            h => comboBox.SelectedIndexChanged -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> SelectionChangeCommittedAsObservable(this ComboBox comboBox, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => comboBox.SelectionChangeCommitted += h,
                            h => comboBox.SelectionChangeCommitted -= h,
                            cancellationToken);
    }

    // DragDrop
    public static Observable<DragEventArgs> DragEnterAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.DragEnter += h,
                            h => control.DragEnter -= h,
                            cancellationToken);
    }

    public static Observable<DragEventArgs> DragOverAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.DragOver += h,
                            h => control.DragOver -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> DragLeaveAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => control.DragLeave += h,
                            h => control.DragLeave -= h,
                            cancellationToken);
    }

    public static Observable<DragEventArgs> DragDropAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DragEventHandler, DragEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.DragDrop += h,
                            h => control.DragDrop -= h,
                            cancellationToken);
    }

    // Mouse
    public static Observable<MouseEventArgs> MouseDownAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.MouseDown += h,
                            h => control.MouseDown -= h,
                            cancellationToken);
    }

    public static Observable<MouseEventArgs> MouseMoveAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.MouseMove += h,
                            h => control.MouseMove -= h,
                            cancellationToken);
    }

    public static Observable<MouseEventArgs> MouseUpAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                            h => (sender, e) => h(e),
                            h => control.MouseUp += h,
                            h => control.MouseUp -= h,
                            cancellationToken);
    }

    // Focus
    public static Observable<EventArgs> EnterAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => control.Enter += h,
                            h => control.Enter -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> LeaveAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => control.Leave += h,
                            h => control.Leave -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> GotFocusAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => control.GotFocus += h,
                            h => control.GotFocus -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> LostFocusAsObservable(this Control control, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => control.LostFocus += h,
                            h => control.LostFocus -= h,
                            cancellationToken);
    }

    // DataGridView
    public static Observable<DataGridViewCellEventArgs> CellValueChangedAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellEventHandler, DataGridViewCellEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.CellValueChanged += h,
                            h => dataGridView.CellValueChanged -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellEventArgs> CellClickAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellEventHandler, DataGridViewCellEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.CellClick += h,
                            h => dataGridView.CellClick -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellEventArgs> CellDoubleClickAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellEventHandler, DataGridViewCellEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.CellDoubleClick += h,
                            h => dataGridView.CellDoubleClick -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellMouseEventArgs> ColumnHeaderMouseClickAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellMouseEventHandler, DataGridViewCellMouseEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.ColumnHeaderMouseClick += h,
                            h => dataGridView.ColumnHeaderMouseClick -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellMouseEventArgs> RowHeaderMouseClickAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellMouseEventHandler, DataGridViewCellMouseEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.RowHeaderMouseClick += h,
                            h => dataGridView.RowHeaderMouseClick -= h,
                            cancellationToken);
    }

    public static Observable<EventArgs> SelectionChangedAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.SelectionChanged += h,
                            h => dataGridView.SelectionChanged -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellEventArgs> RowEnterAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellEventHandler, DataGridViewCellEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.RowEnter += h,
                            h => dataGridView.RowEnter -= h,
                            cancellationToken);
    }

    public static Observable<DataGridViewCellEventArgs> RowLeaveAsObservable(this DataGridView dataGridView, CancellationToken cancellationToken = default)
    {
        return Observable.FromEvent<DataGridViewCellEventHandler, DataGridViewCellEventArgs>(
                            h => (sender, e) => h(e),
                            h => dataGridView.RowLeave += h,
                            h => dataGridView.RowLeave -= h,
                            cancellationToken);
    }
}
