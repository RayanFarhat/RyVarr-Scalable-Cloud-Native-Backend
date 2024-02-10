using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    /// <summary>
    ///  A 3D finite element model object. This object has methods and dictionaries to create, store,
    ///  and retrieve results from a finite element model.
    /// </summary>
    public partial class FEModel3D
    {

        #region Properties
        public Dictionary<string, Node3D> Nodes = new Dictionary<string, Node3D>();
        public Dictionary<string, Material> Materials = new Dictionary<string, Material>();

        #endregion
        public FEModel3D() {
        }
    }
}
