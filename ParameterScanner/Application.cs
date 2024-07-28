using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; //Used for diagnotic purposes
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.IO;



namespace ParameterScanner
{
    public class Application: IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel panel = RibbonPanel(application);
            //Assembly Path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            if(panel.AddItem(new PushButtonData("Parameter Scanner", "Parameter Scanner", thisAssemblyPath, "ParameterScanner.Command"))
                is PushButton button)
            {
                button.ToolTip = "Search specific paramater";

                Uri uri = new Uri(Path.Combine(Path.GetDirectoryName(thisAssemblyPath), "Resources", "parameterScanner.ico"));
                BitmapImage bitmap = new BitmapImage(uri);
                button.LargeImage = bitmap;
            }

            return Result.Succeeded;
        }

        //Ribbon tab
        public RibbonPanel RibbonPanel (UIControlledApplication a)
        {
            string tab = "Parameters";
            RibbonPanel ribbonPanel = null;

            try
            {
                a.CreateRibbonTab(tab);
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }

            try
            {
                a.CreateRibbonPanel(tab, "Scanner Tools");
            }
            catch (Exception error)
            {
                Debug.WriteLine(error.Message);
            }

            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels.Where(p => p.Name == "Scanner Tools")) 
            {
                ribbonPanel = p;
            }

            return ribbonPanel;

        }

    }
}
