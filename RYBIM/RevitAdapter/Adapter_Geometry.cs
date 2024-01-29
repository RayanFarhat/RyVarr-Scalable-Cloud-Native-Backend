using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.RevitAdapter
{
    public partial class Adapter
    {
        public static List<XYZ> GetXYZsFromElemnt(Element e)
        {
            // size is 8 for box (column for example)
            List<XYZ> vertices= new List<XYZ>();
            GeometryElement geometryElement = e.get_Geometry(new Options());
            Solid solid = GetSolid(geometryElement);
            foreach (Edge edge in solid.Edges)
            {
                foreach (XYZ vertex in edge.Tessellate())
                {
                    bool exist = false;
                    foreach (XYZ v in vertices)
                    {
                        if (Math.Abs(v.X - vertex.X) < 0.0001 && Math.Abs(v.Y - vertex.Y) < 0.0001 && Math.Abs(v.Z - vertex.Z) < 0.0001) { exist = true; }
                    }
                    if (!exist)
                    {
                        vertices.Add(vertex);
                    }
                }
            }
            return vertices;
        }
        public static Solid GetSolid(GeometryElement ge)
        {
            foreach (GeometryObject geometryObject in ge)
            {
                if (geometryObject is Solid solid)
                {
                    if (solid.Edges.Size != 0)
                    {
                        return solid;
                    }
                }
                // sometimes there is no solid and we need to compute it
                else if (geometryObject is GeometryInstance geometryInstance)
                {
                    GeometryElement NestedGeometryElement = geometryInstance.GetInstanceGeometry();
                    foreach (GeometryObject NestedGeometryObject in NestedGeometryElement)
                    {
                        if (NestedGeometryObject is Solid NestedSolid)
                        {
                            if (NestedSolid.Edges.Size != 0)
                            {
                                return NestedSolid;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
