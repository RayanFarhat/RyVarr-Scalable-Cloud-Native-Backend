using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.RevitAdapter
{
    public partial class Adapter
    {
        public static UIApplication uiapp;
        public static UIDocument uidoc;
        public static Application app;
        public static Document doc;
        public static void Init(UIApplication uiApplication)
        {
            uiapp = uiApplication;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;
        }
        public static double ConvertToCM(double value) { return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Centimeters); }
        public static double ConvertToXYZ(double value) { return UnitUtils.ConvertToInternalUnits(value, UnitTypeId.Centimeters); }

    }
}
