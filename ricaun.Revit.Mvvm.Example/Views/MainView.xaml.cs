using ricaun.Revit.Mvvm.Example.ViewModels;
using ricaun.Revit.UI;
using System;
using System.Windows;
using System.Windows.Input;

namespace ricaun.Revit.Mvvm.Example.Views
{
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            InitializeWindow();
            this.Title = nameof(MainView);
            this.DataContext = viewModel;
            this.Icon = Properties.Resource.Text.GetBitmapSource();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.MinWidth = 240;
            this.MinHeight = 240;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
            this.PreviewKeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Escape) Close(); };
        }
        #endregion
    }
}