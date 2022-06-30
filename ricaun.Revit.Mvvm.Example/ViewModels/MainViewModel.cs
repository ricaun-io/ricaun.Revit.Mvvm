using ricaun.Revit.Mvvm.Example.Models;
using ricaun.Revit.Mvvm.Example.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ricaun.Revit.Mvvm.Example.ViewModels
{
    public class MainViewModel
    {
        public MainModel Model { get; }
        public IRelayCommand MessageBoxCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand RemoveCommand { get; }

        public IAsyncRelayCommand AsyncAddCommand { get; }
        public IAsyncRelayCommand AsyncRemoveCommand { get; }
        public MainViewModel()
        {
            MessageBoxCommand = new RelayCommand(() => { System.Windows.MessageBox.Show(Model.Text); });
            AddCommand = new RelayCommand(() =>
            {
                Model.Items.Add(new ItemModel() { Name = DateTime.Now.ToString() });
                Model.Item = Model.Items[0];

            }, () => { return !AsyncAddCommand.IsExecuting; });
            RemoveCommand = new RelayCommand<ItemModel>((item) =>
            {
                Model.Item = null;
                Model.Items.Remove(item);
                if (Model.Items.Count > 0)
                    Model.Item = Model.Items[0];
                //RefreshProperty(nameof(Model));
            }
            , (item) => { return item != null; }
            );

            AsyncAddCommand = new AsyncRelayCommand(async () =>
            {
                await Task.Delay(1000);
                Model.Items.Add(new ItemModel() { Name = $"Async {DateTime.Now}" });
                Model.Text = $"Async {DateTime.Now}";
            });

            AsyncRemoveCommand = new AsyncRelayCommand<ItemModel>(async (item) =>
            {
                await Task.Delay(1000);
                Model.Item = null;
                Model.Items.Remove(item);
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
            mainView.ShowDialog();
        }
    }
}