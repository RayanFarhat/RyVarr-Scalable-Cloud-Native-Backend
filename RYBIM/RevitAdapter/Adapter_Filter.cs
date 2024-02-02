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
        public static List<Element> getAllColomns()
        {
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            return collector.WherePasses(filter).WhereElementIsNotElementType().ToElements().ToList();
        }
        public static IList<Element> getAllBeams()
        {
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            return collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
        }
        public static IList<Floor> getAllSlabs()
        {
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            IList<Element> slabs = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            IList<Floor> structuralSlabs = new List<Floor>();
            // filter only strutural slabs
            foreach (Element e in slabs)
            {
                if (e.get_Parameter(BuiltInParameter.FLOOR_PARAM_IS_STRUCTURAL).AsValueString() == "Yes")
                {
                    structuralSlabs.Add(e as Floor);
                }
            }
            return structuralSlabs;
        }
        public static FilteredElementCollector GetLevels()
        {
            return new FilteredElementCollector(Adapter.doc)
                    .WhereElementIsNotElementType()
                    .OfCategory(BuiltInCategory.INVALID)
                    .OfClass(typeof(Level));
        }
        public static List<Element> getConcreteRectangularColumnsSymbols()
        {
            return new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(BuiltInCategory.OST_StructuralColumns)
                    .Where(e => e.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsValueString() == "Concrete-Rectangular-Column").ToList();
        }
        public static List<Element> getConcreteRectangularBeamsSymbols()
        {
            return new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(BuiltInCategory.OST_StructuralFraming)
                    .Where(e => e.get_Parameter(BuiltInParameter.ALL_MODEL_FAMILY_NAME).AsValueString() == "Concrete-Rectangular Beam").ToList();
        }
        public static List<Element> getConcreteFloorsSymbols()
        {
            return new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(BuiltInCategory.OST_Floors)
                    .ToList();
        }
    }
}
