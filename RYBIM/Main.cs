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
using System.Reflection.Emit;
using System.Windows.Media;
using RYBIM.Mathematics;

namespace RYBIM
{
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
    /// <remarks>
    /// This application's main class. The class must be Public.
    /// </remarks>
    public class Main : IExternalApplication
    {
        // Both OnStartup and OnShutdown must be implemented as public method
        public Result OnStartup(UIControlledApplication application)
        {
            string assembly =  Assembly.GetExecutingAssembly().Location;
            UIAdapter.Init(application);
            UIAdapter.CreateTab("RYBIM");

            // RectangularConcrete panel
            UIAdapter.AddPanel("Rectangular Concrete");
            UIAdapter.AddTextBox(0, "RectangularConcreteTextBox", "Ask RYBIM", "Responsible for copying, editing, and creating rectangular concrete elements.",
                "For now, to make sure that the plugin work with no issues,\n" +
                "make sure that the family name for rectangular concrete columns is 'Concrete-Rectangular-Column'\n" +
                "and the family name for rectangular concrete beams is 'Concrete-Rectangular Beam'\n" +
                "and Paramaters for them for width and height is 'b' and 'h'\n" +
                "and Thickness Parameter for Floors is 'Default Thickness'\n");
            UIAdapter.AddPushBtn(0, "Run", "RYBIM.Commands.RectangularConcrete", "press to generate RYBIM results");
            UIAdapter.TextBoxes[0].EnterPressed += ProcessText;
            void ProcessText(object sender, Autodesk.Revit.UI.Events.TextBoxEnterPressedEventArgs args)
            {
                string strText = (sender as TextBox).Value as string;
                TaskDialog.Show("Revit", strText);
            }
            UIAdapter.panels[0].AddSlideOut();
            // add radio button group
            RadioButtonGroupData radioData = new RadioButtonGroupData("RectangularConcreteRadioGroup");
            RadioButtonGroup radioButtonGroup = UIAdapter.panels[0].AddItem(radioData) as RadioButtonGroup;

            // create toggle buttons and add to radio button group
            ToggleButtonData tb1 = new ToggleButtonData("toggleButtonPoint", "Point");
            tb1.ToolTip = "RYBIM will perform your command based on origin point, press run and then select point";
            ToggleButtonData tb2 = new ToggleButtonData("toggleButtonTwoPoint", "TwoPoint");
            tb2.ToolTip = "RYBIM will perform your command based on origin point, press run and then select two point";
            ToggleButtonData tb3 = new ToggleButtonData("toggleButtonElements", "Elements");
            tb3.ToolTip = "RYBIM will perform your command based on selected elements, select element adn then press run";
            radioButtonGroup.AddItem(tb1);
            radioButtonGroup.AddItem(tb2);
            radioButtonGroup.AddItem(tb3);
            UIAdapter.AddRadioButtonGroup(radioButtonGroup);

            //text panel
            UIAdapter.AddPanel("textPanel");
            UIAdapter.AddPushBtn(1, "btn", "RYBIM.Test", "press this btn");

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }
}
