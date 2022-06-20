using ricaun.Revit.Mvvm.Example.Models;
using ricaun.Revit.Mvvm.Example.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ricaun.Revit.Mvvm.Example.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainModel Model { get; }
        public IRelayCommand MessageBoxCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }
        public MainViewModel()
        {
            MessageBoxCommand = new RelayCommand(() => { System.Windows.MessageBox.Show(Model.Text); });
            AddCommand = new RelayCommand<ItemModel>((item) =>
            {
                Model.Items.Add(new ItemModel() { Name = DateTime.Now.ToString() });
                Model.Item = Model.Items[0];
                RefreshProperty(nameof(Model));
            }
            //, (item) => { return true; }
            );
            RemoveCommand = new RelayCommand<ItemModel>((item) =>
            {
                Model.Items.Remove(item);
                if (Model.Items.Count > 0)
                    Model.Item = Model.Items[0];
                RefreshProperty(nameof(Model));
            }
            , (item) => { return item != null; }
            );
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
            mainView.ShowDialog();
        }
    }
}