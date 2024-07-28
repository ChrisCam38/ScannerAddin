using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace ParameterScanner
{
    public class IsolateElementsHandler : IExternalEventHandler
    {
        public List<ElementId> ElementIds { get; set; }

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            using (Transaction trans = new Transaction(doc, "Isolate Elements"))
            {
                trans.Start();
                doc.ActiveView.IsolateElementsTemporary(ElementIds);
                trans.Commit();
            }
        }

        public string GetName()
        {
            return "Isolate Elements Handler";
        }
    }
}
