using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Adapter
{
    public partial class Adapter
    {
        public static UIApplication uiapp;
        public static UIDocument uiddoc;
        public static Application app;
        public static Document doc;
        public static void Init(UIApplication uiApplication)
        {
            uiapp = uiApplication;
            uiddoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uiddoc.Document;
        }
    }
}
