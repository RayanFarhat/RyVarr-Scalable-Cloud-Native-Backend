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
        // must inside Transaction
        public static FamilyInstance CreateColumn(XYZ location, FamilySymbol symbol, Level level)
        {
            return doc.Create.NewFamilyInstance(location, symbol, level, Autodesk.Revit.DB.Structure.StructuralType.Column);
        }
        public static Floor CreateFloor(XYZ[] vertices, string floorTypeName, Level level)
        {
            FloorType floorType
                      = new FilteredElementCollector(Adapter.doc)
                        .OfClass(typeof(FloorType))
                        .First<Element>(
                          e => e.Name.Equals(floorTypeName))
                          as FloorType;

            CurveLoop curveLoop = new CurveLoop();
            for (int i = 0; i < vertices.Length; i++)
            {
                Line line = Line.CreateBound(vertices[i], vertices[(i + 1) % vertices.Length]);
                curveLoop.Append(line);
            }
            var curveLoopList = new List<CurveLoop>();
            curveLoopList.Add(curveLoop);

            Line slopeArrow = Line.CreateBound(new XYZ(0, 0, vertices[0].Z), new XYZ(0, 1, vertices[0].Z));
            return Floor.Create(Adapter.doc, curveLoopList, floorType.Id, level.Id, true, slopeArrow, 0);
        }
    }
}
