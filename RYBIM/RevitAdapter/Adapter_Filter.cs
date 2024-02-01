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
        public static List<Element> getAllColomns()
        {
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            return collector.WherePasses(filter).WhereElementIsNotElementType().ToElements().ToList();
        }
        public static List<Element> getConcreteRectangularColumnsSymbols()
        {
            return new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(BuiltInCategory.OST_StructuralColumns)
                    .Where(e =>
                    {
                        bool isConcreteRect = false;
                        foreach (Parameter para in e.Parameters)
                        {
                            if (para.Definition.Name == "Family Name" && para.AsValueString() == "Concrete-Rectangular-Column")
                            {
                                isConcreteRect = true;
                                break;
                            }
                        }
                        return isConcreteRect;
                    }).ToList();
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
                foreach (Parameter item in e.Parameters)
                {
                    if (item.Definition.Name == "Structural")
                    {
                        if (item.AsValueString()=="Yes")
                        {
                            structuralSlabs.Add(e as Floor);
                        }
                    }
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
    }
}
