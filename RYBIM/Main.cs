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
using RYBIM.Analysis;
using System.Data;
using RYBIM.Mathematics;
using Autodesk.Revit.DB.Structure;
using System.Xml.Linq;

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

                var model = new FEModel3D();
                model.AddNode(0, 0, 0, "n1");
                model.AddNode(10, 0, 0, "n2");
                model.add_material(29000.0, 11200, 0.3, 2.836e-4, null, "mat");
                model.AddMember("n1", "n2", "mat", 100, 100, 250, 20, "elem");
                model.def_support("n1", true, true, true, true, true, true);
                model.def_support("n2", true, true, true, true, true, true);

                model.AddNode(20, 0, 0, "n3");
                model.AddNode(30, 0, 0, "n4");
                model.AddMember("n3", "n4", "mat", 100, 100.0, 250, 20, "elem2");
                model.def_support("n3", true, true, true, true, true, true);
                model.def_support("n4", true, true, true, true, true, false);

                var factors = new Dictionary<string, double>
                {
                    { "D", 1.4 }
                };
                // model.add_load_combo(factors, "1.4D");

                model.add_member_pt_load("elem", Direction.Fy, -100, 5);
                model.add_member_pt_load("elem2", Direction.Fy, -100, 5);
                //model.Add_node_load("n2", Direction.FY, -100);

                model.Analyze();
                throw new Exception($"{model.Members["elem2"].d()}");

                model.Members["elem"].plot_Shear(Direction.Fy);
                model.Members["elem2"].plot_Shear(Direction.Fy);
                //.Members["elem"].plot_Moment(Direction.Mz);
                //model.Members["elem2"].plot_Moment(Direction.Mz);
                //model.Members["elem"].plot_Deflection(Direction.Fy);
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
//TODO : equivalent frame method for flat slabs