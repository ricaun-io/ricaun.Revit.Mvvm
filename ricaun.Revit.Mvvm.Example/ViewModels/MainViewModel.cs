using ricaun.Revit.Mvvm.Example.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ricaun.Revit.Mvvm.Example.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public class ItemModel
        {
            public string Name { get; set; }
            public override string ToString()
            {
                return string.Format("{0}", Name);
            }
        }
        public ObservableCollection<ItemModel> Items { get; private set; }
        public ItemModel Item { get; set; }
        public string Text { get; set; } = "Text";
        public IRelayCommand MessageBoxCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public MainViewModel()
        {
            MessageBoxCommand = new RelayCommand(() => { System.Windows.MessageBox.Show(Text); });
            AddCommand = new RelayCommand(() => { Items.Add(new ItemModel() { Name = DateTime.Now.ToString() }); });
            RemoveCommand = new RelayCommand(() =>
            {
                Console.WriteLine(Item);
                try
                {
                    if (Items.Count > 0)
                        Items.RemoveAt(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            });
            Items = new ObservableCollection<ItemModel>();
        }

        MainView mainView;
        public void Show()
        {
            if (mainView == null)
            {
                mainView = new MainView(this);
                mainView.Closed += (s, e) => { mainView = null; };
            }
            mainView.Activate();
            mainView.Show();
        }
    }
}