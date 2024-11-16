
namespace WpfApp1;
internal class Item : BindableBase
{
    public int Id { get; set; }
    public string _name = "";
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}
