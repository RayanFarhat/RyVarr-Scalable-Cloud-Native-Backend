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
    /// The main model that analyze the revit physical model.
    /// </summary>
    public partial class RCModel
    {
        #region Properties

        /// <summary>physical
        /// A dictionary that save the analytical elements Ids linked with there analytical elements Ids.
        /// </summary>
        public Dictionary<string, string> Elements { get; set; }
        /// <summary>
        /// A list that save positions of the nodes.
        /// </summary>
        public List<XYZ> Nodes { get; set; }

        #endregion
        public RCModel() { 
            Nodes = new List<XYZ>();
            Elements = new Dictionary<string, string>();
        }

        /// <summary>
        /// check distance between two points is less than 50cm
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <returns></returns>
        public bool isClose(XYZ p1, XYZ p2)
        {
            return Math.Pow(
                Math.Pow(p1.X - p2.X, 2) +
                Math.Pow(p1.Y - p2.Y, 2) +
                Math.Pow(p1.Z - p2.Z, 2)
                , 0.5) < Adapter.ConvertToXYZ(500);
        }
    }
}
