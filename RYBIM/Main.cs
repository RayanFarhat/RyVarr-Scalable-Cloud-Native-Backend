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
                model.AddNode(0,0,0,"n1");
                model.AddNode(20,0,0,"n2");
                model.add_material(29000, 11200, 0.3, 33,null,"mat");
                model.AddMember("n1", "n2", "mat", 100, 150, 2, 50, "elem");
                model.def_support("n1", true, true, true, true, false, false);
                model.def_support("n2", true, true, true, false, false, false);

                var factors = new Dictionary<string, double>
                {
                    { "D", 1.4 }
                };
               // model.add_load_combo(factors, "1.4D");

                model.add_member_pt_load("elem", Direction.Fy, -20, 10);

                model.Analyze();
                //var f = new Form1(model.Members["elem"].Shear_Array(Direction.FY,120,"1.4D"));
                double[][] twoDArray = new double[2][];
                twoDArray[0] = new double[]{ 0,2,4};
                twoDArray[1] = new double[] { 6, 3, 4 };
                var ff = new Form1(twoDArray);
                //ff.ShowDialog();
                //model.Members["elem"].Update_segments();
                var mem = model.Members["elem"];
                mem.Update_segments();

                TaskDialog.Show("sss", model.K().ToString());

                // Prepare the model for analysis
                Analyzer.prepareModel(model);
                var K = model.K();
                // Identify which load combinations have the tags the user has given
                var comboList = Analyzer.identify_combos(model);
                // Step through each load combination
                foreach (var combo in comboList)
                {
                    // Get the global fixed end reaction vector
                    var FER = model.FER(combo.Name);
                    // Get the global nodal force vector      
                    var P = model.P(combo.Name);
                    // Calculate the unknown displacements D
                    //TODO check why Solve return NaN values
                    var tmpK = ((Matrix)K.Clone());
                    var D = tmpK.Solve(P.Subtract(FER));
                    TaskDialog.Show("sss", tmpK.ToString());

                    TaskDialog.Show("sss", D.ToString());

                    // Store the calculated displacements to the model and the nodes in the model
                    Analyzer.StoreDisplacements(model, D, combo);
                }
                Analyzer.calcReactions(model, null);
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