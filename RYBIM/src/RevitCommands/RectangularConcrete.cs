using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RYBIM.RevitAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
namespace RYBIM.RevitCommands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class RectangularConcrete : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
          ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Adapter.Init(commandData.Application);
                string userInput = UIAdapter.TextBoxes[0].Value as string;
                if (UIAdapter.RadioButtonGroups[0].Current.Name == "toggleButtonPoint")
                {
                    XYZ selectedPoint = Adapter.selectPoint();
                }
                else if (UIAdapter.RadioButtonGroups[0].Current.Name == "toggleButtonTwoPoint")
                {
                    XYZ selectedPoint1 = Adapter.selectPoint();
                    XYZ selectedPoint2 = Adapter.selectPoint();
                }
                else if (UIAdapter.RadioButtonGroups[0].Current.Name == "toggleButtonElements")
                {
                    var elems = Adapter.MultipleSelect();
                }
                else
                {
                    TaskDialog.Show("RYBIM Erorr", "There is no selection method has been chosen");
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Autodesk.Revit.UI.Result.Failed;
            }

            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
