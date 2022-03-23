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

        public MainModel Model { get; }
        public IRelayCommand MessageBoxCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public MainViewModel()
        {
            MessageBoxCommand = new RelayCommand(() => { System.Windows.MessageBox.Show(Model.Text); });
            AddCommand = new RelayCommand(() =>
            {
                Model.Items.Add(new ItemModel() { Name = DateTime.Now.ToString() });
            });
            RemoveCommand = new RelayCommand(() =>
            {
                Model.Items.Remove(Model.Item);
                if (Model.Items.Count > 0)
                    Model.Item = Model.Items[0];
                RefreshProperty(nameof(Model));
            });
            Model = new MainModel();
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