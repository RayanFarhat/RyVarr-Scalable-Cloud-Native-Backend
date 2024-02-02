using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.RevitAdapter
{
    public partial class Adapter
    {
        // must inside Transaction
        public static FamilyInstance CreateColumn(XYZ locationinMin, FamilySymbol symbol, Level level, double Height=0)
        {
            if (!symbol.IsActive)
            {
                symbol.Activate();
            }
            var halfWidth = symbol.LookupParameter("b").AsDouble() / 2;
            var halfDepth = symbol.LookupParameter("h").AsDouble() / 2;
            var location = new XYZ(locationinMin.X + halfWidth, locationinMin.Y + halfDepth, locationinMin.Z);
            // if height is not defined then make the height desided by level
            if (Height != 0)
            {
                Line columnAxis = Line.CreateBound(location, new XYZ(location.X, location.Y, location.Z + Height));
                return doc.Create.NewFamilyInstance(columnAxis, symbol, level, StructuralType.Column);
            }
            return doc.Create.NewFamilyInstance(location, symbol, level, StructuralType.Column);
        }
        public static FamilyInstance CreateColumnAs3D(XYZ locationinMin, double Width, double Depth, double Height)
        {
            var symbols = getConcreteRectangularColumnsSymbols();
            foreach (var symbol in symbols)
            {
                if (Width == symbol.LookupParameter("b").AsDouble() && Depth == symbol.LookupParameter("h").AsDouble())
                {
                    return CreateColumn(locationinMin, symbol as FamilySymbol, GetLevels().ElementAt(0) as Level,Height);
                }
            }
            return null;
        }
        public static FamilyInstance CreateBeam(XYZ startPoint, XYZ endPoint, FamilySymbol symbol, Level level)
        {
            if (!symbol.IsActive)
            {
                symbol.Activate();
            }
            Curve beamLine = Line.CreateBound(startPoint, endPoint);
            return doc.Create.NewFamilyInstance(beamLine, symbol, level, StructuralType.Beam);
        }
        public static FamilyInstance CreateBeamAs3D(XYZ locationinMin, double Width, double Depth, double Height)
        {
            var symbols = getConcreteRectangularBeamsSymbols();
            foreach (var symbol  in symbols )
            {
                var b = symbol.LookupParameter("b").AsDouble();
                var h = symbol.LookupParameter("h").AsDouble();
                if (h == Height)
                {
                    if (b == Width)
                    {
                        XYZ p1 = new XYZ(locationinMin.X+(Width/2), locationinMin.Y, locationinMin.Z+Height);
                        XYZ p2 = new XYZ(locationinMin.X+(Width/2), locationinMin.Y+Depth, locationinMin.Z+Height);
                        return CreateBeam(p1,p2,symbol as FamilySymbol, GetLevels().ElementAt(0) as Level);
                    }
                    else if(b == Depth)
                    {
                        XYZ p1 = new XYZ(locationinMin.X, locationinMin.Y + (Depth/2), locationinMin.Z + Height);
                        XYZ p2 = new XYZ(locationinMin.X + Width , locationinMin.Y + (Depth / 2), locationinMin.Z + Height);
                        return CreateBeam(p1, p2, symbol as FamilySymbol, GetLevels().ElementAt(0) as Level);
                    }
                }
            }
            return null;
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
            return Floor.Create(doc, curveLoopList, floorType.Id, level.Id, true, slopeArrow, 0);
        }
        public static TextNote CreateText(string text, XYZ location, View view,double width,double rotation)
        {
            ElementId defaultTextTypeId = Adapter.doc.GetDefaultElementTypeId(ElementTypeGroup.TextNoteType);
            TextNoteOptions opts = new TextNoteOptions(defaultTextTypeId);
            opts.HorizontalAlignment = HorizontalTextAlignment.Left;
            opts.Rotation = rotation;
            return TextNote.Create(doc, view.Id, location, width, text, opts);
        }
    }
}
