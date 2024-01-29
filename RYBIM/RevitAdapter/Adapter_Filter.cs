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
        public static IList<Element> getAllColomns()
        {
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralColumns);
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            return collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
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
    }
}
