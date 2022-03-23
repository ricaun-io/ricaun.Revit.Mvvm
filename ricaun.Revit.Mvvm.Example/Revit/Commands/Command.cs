using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.Mvvm.Example.ViewModels;
using System;

namespace ricaun.Revit.Mvvm.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public static MainViewModel MainViewModel = new MainViewModel();
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            MainViewModel.Show();

            return Result.Succeeded;
        }
    }
}
