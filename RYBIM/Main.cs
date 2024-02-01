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
                

                IList<Element> Columns = Adapter.getAllColomns();
                string str = "";

                foreach (Element e in Columns)
                {
                    str += "Vertices for " + e.Name + "\n";
                    List<XYZ> vertices = Adapter.GetXYZsFromElemnt(e);
                    foreach (XYZ v in vertices)
                    {
                        str += $"X: {v.X}, Y: {v.Y}\n";
                    }
                    str += "\n";
                }
                UIAdapter.TextBoxes[0].Value = str;
                using (Transaction transaction = new Transaction(Adapter.doc, "Create Slab"))
                {
                    transaction.Start();

                    // Create a level for the slab
                    Level level = Level.Create(Adapter.doc, 0.0);

                    // Create a sketch plane for the slab
                    SketchPlane sketchPlane = SketchPlane.Create(Adapter.doc, Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero));

                    // Define the geometry for the slab (here we use a simple rectangle)
                    XYZ[] points = new XYZ[]
                    {
                        new XYZ(-23.8173050301309, 10.2714167952434, 0),
                        new XYZ(-13.1076238767901, 10.2714167952434, 0),
                        new XYZ(-13.1076238767901, 5.35015695272375, 0),
                        new XYZ(-5.11674833183194, 5.35015695272372, 0),
                        new XYZ(-5.11674833183194, 10.2714167952434, 0),
                        new XYZ(-5.11674833183193, 21.7543564277893, 0),
                        new XYZ(-13.1076238767901, 21.7543564277893, 0),
  
                    };
                    FloorType floorType
                      = new FilteredElementCollector(Adapter.doc)
                        .OfClass(typeof(FloorType))
                        .First<Element>(
                          e => e.Name.Equals("Concrete-Commercial 362mm"))
                          as FloorType;
                    // Create a closed loop to define the boundary
                    CurveLoop curveLoop = new CurveLoop();
                    for (int i = 0; i < points.Length; i++)
                    {
                        Line line = Line.CreateBound(points[i], points[(i + 1) % points.Length]);
                        curveLoop.Append(line);
                    }
                    Line slopeArrow = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0, 1, 0));
                    var x = new List<CurveLoop>();
                    x.Add(curveLoop);
                    // Create the slab using the sketch plane and the closed loop
                    Floor.Create(Adapter.doc, x, floorType.Id, level.Id, true, slopeArrow,0);
                    transaction.Commit();
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
