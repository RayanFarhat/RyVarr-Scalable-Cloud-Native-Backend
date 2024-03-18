using Autodesk.Revit.DB;
using RyVarrRevit.Analysis;
using RyVarrRevit.RevitAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyVarrRevit.RC
{
    public partial class RCModel
    {
        /// <summary>
        /// get node name that inside the FEModel nodes
        /// </summary>
        /// <param name="p">3D point</param>
        /// <returns></returns>
        public string getNodeName(XYZ p)
        {
            var xStr = p.X.ToString("0.######");
            var yStr = p.Y.ToString("0.######");
            var zStr = p.Z.ToString("0.######");
            return xStr + "," + yStr + "," + zStr;
        }
        /// <summary>
        /// Add node to the FEModel
        /// </summary>
        /// <param name="p">The 3D point to add</param>
        public void addNode(XYZ p)
        {
            var nodeName = getNodeName(p);
            var nodeX = Math.Round(p.X, 6);
            var nodeY = Math.Round(p.Y, 6);
            var nodeZ = Math.Round(p.Z, 6);
            var node = new Node3D(nodeName, nodeX, nodeY, nodeZ);
            FEModel.Nodes.Add(nodeName, node);
        }
        /// <summary>
        /// check distance between two points is less than 50cm
        /// </summary>
        /// <param name="p1">first point</param>
        /// <param name="p2">second point</param>
        /// <returns>True if two point close by 50cm,False else</returns>
        public bool isClose(XYZ p1, XYZ p2)
        {
            return Math.Pow(
                Math.Pow(p1.X - p2.X, 2) +
                Math.Pow(p1.Y - p2.Y, 2) +
                Math.Pow(p1.Z - p2.Z, 2)
                , 0.5) < Adapter.ConvertToXYZ(500);
        }
        /// <summary>
        /// check distance between two points is less than 50cm
        /// </summary>
        /// <param name="p">3D point</param>
        /// <param name="n">Node3D</param>
        /// <returns>True if two point close by 50cm,False else</returns>
        public bool isClose(XYZ p, Node3D n)
        {
            return Math.Pow(
                Math.Pow(p.X - n.X, 2) +
                Math.Pow(p.Y - n.Y, 2) +
                Math.Pow(p.Z - n.Z, 2)
                , 0.5) < Adapter.ConvertToXYZ(500);
        }
    }
}
