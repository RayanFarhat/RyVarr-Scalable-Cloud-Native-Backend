using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.RevitAdapter
{
    public partial class Adapter
    {
        public static XYZ selectPoint()
        {
            return uidoc.Selection.PickPoint("Pick a point");
        }
        public static IList<Element> MultipleSelect()
        {
            // user must select before press the command
            List<Element> elements = new List<Element>();
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();
            foreach (ElementId id in selectedIds)
            {
                Element element = uidoc.Document.GetElement(id);
                elements.Add(element);
            }
            return elements;
        }
    }
}
