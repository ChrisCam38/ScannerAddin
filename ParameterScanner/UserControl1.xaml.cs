using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ParameterScanner
{

    public partial class MainWindow : Window
    {
        // References to Revit's UI and Document objects
        UIDocument uiDoc;
        Document doc;
        public IsolateElementsHandler isolateHandler;
        public ExternalEvent isolateEvent;
        public MainWindow(UIApplication uiApp)
        {
            InitializeComponent();
            uiDoc = uiApp.ActiveUIDocument;
            doc = uiDoc.Document;

            // Initialize the handler and external event for element isolation
            isolateHandler = new IsolateElementsHandler();
            isolateEvent = ExternalEvent.Create(isolateHandler);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        // Searches elements based on parameter name and value
        private void SearchElements(string paramName, string paramValue, bool isolate)
        {
            List<ElementId> elementIds = FindElements(paramName, paramValue);

            

            if (elementIds.Count > 0)
            {
                MessageBox.Show($"{elementIds.Count} elements found this tha value", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                try
                {
                    // Isolate or select elements
                    if (isolate)
                    {
                        isolateHandler.ElementIds = elementIds;
                        isolateEvent.Raise();
                        uiDoc.Selection.SetElementIds(elementIds);
                        MessageBox.Show($"{elementIds.Count} elements isolated.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    else
                    {
                        uiDoc.Selection.SetElementIds(elementIds);
                        MessageBox.Show($"{elementIds.Count} elements selected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show("No elements found with the specified parameter value.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private List<ElementId> FindElements(string paramName, string paramValue)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            ICollection<Element> elements = collector.WhereElementIsNotElementType().ToElements();

            List<ElementId> elementIds = new List<ElementId>();

            foreach (Element element in elements)
            {
                Parameter param = element.LookupParameter(paramName);

                if (param != null && (string.IsNullOrEmpty(paramValue) || param.AsValueString() == paramValue))
                {

                    elementIds.Add(element.Id);
                }
            }

            return elementIds;
        }
        // Event handler for the "Isolate" button
        private void Isolate_Btn(object sender, RoutedEventArgs e)
        {
            string paramName = InputBoxPN.Text;
            string paramValue = InputBoxPV.Text;

            if (string.IsNullOrWhiteSpace(paramName))
            {
                MessageBox.Show("Parameter Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SearchElements(paramName, paramValue, true);
        }
        // Event handler for the "Select" button
        private void Select_Btn(object sender, RoutedEventArgs e)
        {
            string paramName = InputBoxPN.Text;
            string paramValue = InputBoxPV.Text;

            if (string.IsNullOrWhiteSpace(paramName))
            {
                MessageBox.Show("Parameter Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SearchElements(paramName, paramValue, false);
        }
    }
}
