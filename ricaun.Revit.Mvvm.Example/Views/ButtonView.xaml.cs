using System;
using System.Threading.Tasks;
using System.Windows;

namespace ricaun.Revit.Mvvm.Example.Views
{
    public partial class ButtonView : Window
    {
        public string Text { get; set; } = "Not Null";
        public IAsyncRelayCommand AsyncCommandObject { get; } = new AsyncRelayCommand<object>(async (obj) =>
        {
            await Task.Delay(1000);
            int i = 1; i /= 0;
        }).SetExceptionHandler(ExceptionHandler);

        public IAsyncRelayCommand AsyncCommand { get; } = new AsyncRelayCommand(async () =>
        {
            await Task.Delay(1000);
            int i = 1; i /= 0;
        }).SetExceptionHandler(ExceptionHandler);

        public IRelayCommand CommandObject { get; } = new RelayCommand<object>((obj) =>
        {
            int i = 1; i /= 0;
        }).SetExceptionHandler(ExceptionHandler);

        public IRelayCommand Command { get; } = new RelayCommand(() =>
        {
            var task = Task.Run(async () =>
            {
                await Task.Delay(500);
            });
            task.GetAwaiter().GetResult();
            int i = 1; i /= 0;
        }).SetExceptionHandler(ExceptionHandler);

        public static void ExceptionHandler(Exception exception)
        {
            Console.WriteLine(exception);
        }

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