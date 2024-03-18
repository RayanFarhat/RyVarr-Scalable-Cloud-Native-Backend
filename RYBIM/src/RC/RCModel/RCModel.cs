using RYBIM.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using RYBIM.RevitAdapter;


namespace RYBIM.RC
{
    /// <summary>
    /// The main model that analyze the revit physical RC model.It work with the FEModel3D not independent.
    /// </summary>
    public partial class RCModel
    {
        #region Properties

        /// <summary>physical
        /// A dictionary that save the analytical elements Ids linked with there analytical elements Ids.
        /// </summary>
        public FEModel3D FEModel { get; set; }

        /// <summary>physical
        /// A dictionary that save the analytical elements Ids linked with there analytical elements Ids.
        /// </summary>
        public Dictionary<string, string> Elements { get; set; }

        #endregion
        public RCModel() { 
            FEModel = new FEModel3D();
            Elements = new Dictionary<string, string>();
        }
    }
}
