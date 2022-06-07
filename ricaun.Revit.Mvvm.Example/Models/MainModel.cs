namespace ricaun.Revit.Mvvm.Example.Models
{
    public class MainModel
    {
        public ObservableCollection<ItemModel> Items { get; private set; }
        public ItemModel Item { get; set; }
        public string Text { get; set; } = "Text";
        public MainModel()
        {
            Items = new ObservableCollection<ItemModel>();
            for (int i = 0; i < 5; i++)
            {
                Items.Add(new ItemModel() { Name = $"{i}" });
            }
        }
    }
    public class ItemModel
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }

}