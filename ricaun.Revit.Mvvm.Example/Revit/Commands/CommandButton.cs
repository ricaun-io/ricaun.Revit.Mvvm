using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.Mvvm.Example.Views;

namespace ricaun.Revit.Mvvm.Example.Revit.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CommandButton : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            UIApplication uiapp = commandData.Application;

            new ButtonView().Show();

            return Result.Succeeded;
        }
    }

}
