using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ricaun.Revit.UI;
using System;

namespace ricaun.Revit.Mvvm.Example.Revit
{

    [Console]
    public class App : IExternalApplication
    {
        private static RibbonPanel ribbonPanel;
        public Result OnStartup(UIControlledApplication application)
        {
            ribbonPanel = application.CreatePanel("Mvvm");
            ribbonPanel.CreatePushButton<Commands.Command>()
                .SetText(Properties.Resource.Text)
                .SetLargeImage("/UIFrameworkRes;component/ribbon/images/revit.ico")
                .SetToolTip(LanguageExtension.GetLanguageType().ToString());

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ribbonPanel?.Remove();
            return Result.Succeeded;
        }
    }

}