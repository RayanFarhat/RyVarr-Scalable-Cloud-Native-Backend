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
        public static void ShowParameters(Element e)
        {
            string str = "Parameters for " + e.Name + "\n";
            if (e.Location is LocationPoint locationPoint)
            {
                XYZ point = locationPoint.Point;
                str += $"at: X: {point.X}, Y: {point.Y}, Z: {point.Z}\n";
            }
            foreach (Parameter para in e.Parameters)
            {
                str += para.Definition.Name + " : " + para.AsValueString() + "\n";
            }
            TaskDialog.Show("RYBIM Debug", str);
        }
        public static void ShowXYZs(Element e)
        {
            string str = "Vertices for " + e.Name + "\n";
            List<XYZ> vertices = GetXYZsFromElemnt(e);
            foreach (XYZ v in vertices)
            {
                str += $"X: {v.X}, Y: {v.Y}, Z: {v.Z}\n";
            }
            TaskDialog.Show("RYBIM Debug", str);
        }
    }
}
