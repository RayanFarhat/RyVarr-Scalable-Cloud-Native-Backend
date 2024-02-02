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
using RYBIM.RevitAdapter;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media.Media3D;
using System.Security.Cryptography;
using Autodesk.Revit.DB.Structure;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Net;
using static Autodesk.Revit.DB.SpecTypeId;

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
            UIAdapter.AddPushBtn(0, "btn", "RYBIM.Test", "press this btn");
            UIAdapter.AddTextBox(0, "textbox", "enter text", "text tooltib", "long text here");
            UIAdapter.TextBoxes[0].EnterPressed += ProcessText;
            void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args)
            {
                string strText = (sender as TextBox).Value as string;
                TaskDialog.Show("Revit", strText);
            }
            AddSlideOut(UIAdapter.panels[0]);
            void AddSlideOut(RibbonPanel panel)
            {
                string assembly =  Assembly.GetExecutingAssembly().Location;

                panel.AddSlideOut();
                // add radio button group
                RadioButtonGroupData radioData = new RadioButtonGroupData("radioGroup");
                RadioButtonGroup radioButtonGroup = panel.AddItem(radioData) as RadioButtonGroup;

                // create toggle buttons and add to radio button group
                ToggleButtonData tb1 = new ToggleButtonData("toggleButton1", "Red");
                tb1.ToolTip = "Red Option";
                ToggleButtonData tb2 = new ToggleButtonData("toggleButton2", "Green");
                tb2.ToolTip = "Green Option";
                ToggleButtonData tb3 = new ToggleButtonData("toggleButton3", "Blue");
                tb3.ToolTip = "Blue Option";
                radioButtonGroup.AddItem(tb1);
                radioButtonGroup.AddItem(tb2);
                radioButtonGroup.AddItem(tb3);
            }
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Test : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Adapter.Init(commandData.Application);

                var slabs = Adapter.getConcreteRectangularBeamsSymbols();
                foreach (var item in slabs)
                {
                    Adapter.ShowParameters(item);
                }
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
