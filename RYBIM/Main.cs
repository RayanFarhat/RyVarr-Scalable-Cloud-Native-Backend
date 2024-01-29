using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using Autodesk.Revit.Creation;
using RYBIM.Adapter;
using System.Runtime.Remoting.Messaging;

namespace RYBIM
{
    /// <remarks>
    /// This application's main class. The class must be Public.
    /// </remarks>
    public class Main : IExternalApplication
    {
        // Both OnStartup and OnShutdown must be implemented as public method
        public Result OnStartup(UIControlledApplication application)
        {
            UIAdapter.Init(application);
            UIAdapter.CreateTab("RYBIM");
            UIAdapter.AddPanel("textPanel");
            UIAdapter.AddPushBtn(0, "btn", "RYBIM.HelloWorld", "press this btn");
            UIAdapter.AddTextBox(0, "textbox", "enter text", "text tooltib", "long text here");
            UIAdapter.TextBoxes[0].EnterPressed += ProcessText;
            void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args)
            {
                string strText = (sender as TextBox).Value as string;
                TaskDialog.Show("Revit", strText);
            }
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.ReadOnly)]
    public class HelloWorld : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);

                // Apply the filter to the elements in the active document
                // Use shortcut WhereElementIsNotElementType() to find wall instances only
                FilteredElementCollector collector = new FilteredElementCollector(uidoc.Document);
                IList<Element> Columns = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
                String prompt = "The Columns in the current document are:\n";
                foreach (Element e in Columns)
                {
                    prompt += e.Name + "\n";
                }
                TaskDialog.Show("Revit", prompt);
            }
            catch (Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
        /// </ExampleMethod>
    }
}
