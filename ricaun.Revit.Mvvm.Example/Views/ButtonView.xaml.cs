using System;
using System.Threading.Tasks;
using System.Windows;

namespace ricaun.Revit.Mvvm.Example.Views
{
    public partial class ButtonView : Window
    {
        public string Text { get; set; } = string.Empty;
        public IRelayCommand Command { get; private set; } = new AsyncRelayCommand<object>(async (text) =>
        {
            Console.WriteLine(text);
            await Task.Delay(500);
            Console.WriteLine("Exception");
            throw new Exception();
            await Task.Delay(500);
            Console.WriteLine("Never Show");
        });
        public ButtonView()
        {
            InitializeComponent();
            InitializeWindow();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
        }
        #endregion
    }
}