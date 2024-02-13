using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RYBIM.Analysis
{
    /// <summary>
    /// A class representing a node in a 3D finite element model.
    /// </summary>
    public class Node3D
    {
        #region Properties
        /// <summary>
        /// A unique name for the node assigned by the user.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Global X coordinate.
        /// </summary>
        public double X { get; protected set; }
        /// <summary>
        /// Global Y coordinate.
        /// </summary>
        public double Y { get; protected set; }
        /// <summary>
        /// Global Z coordinate.
        /// </summary>
        public double Z { get; protected set; }

        /// <summary>
        /// A list of loads applied to the node (Direction, P)
        /// </summary>
        public List<NodeLoad> NodeLoads { get; protected set; }

        public Dictionary<string, double> DX { get; protected set; }
        public Dictionary<string, double> DY { get; protected set; }
        public Dictionary<string, double> DZ { get; protected set; }
        public Dictionary<string, double> RX { get; protected set; }
        public Dictionary<string, double> RY { get; protected set; }
        public Dictionary<string, double> RZ { get; protected set; }
        public Dictionary<string, double> RxnFX { get; protected set; }
        public Dictionary<string, double> RxnFY { get; protected set; }
        public Dictionary<string, double> RxnFZ { get; protected set; }
        public Dictionary<string, double> RxnMX { get; protected set; }
        public Dictionary<string, double> RxnMY { get; protected set; }
        public Dictionary<string, double> RxnMZ { get; protected set; }
        public bool support_DX { get; protected set; }
        public bool support_DY { get; protected set; }
        public bool support_DZ { get; protected set; }
        public bool support_RX { get; protected set; }
        public bool support_RY { get; protected set; }
        public bool support_RZ { get; protected set; }

        #endregion
        public Node3D(string name, double X, double Y, double Z) {
            this.Name = name;
            this.X = X;
            this.Y = Y;
            this.Z = Z;

            NodeLoads = new List<NodeLoad>();

            // Initialize the dictionaries of calculated node displacements
            DX = new Dictionary<string, double>();
            DY = new Dictionary<string, double>();
            DZ = new Dictionary<string, double>();
            RX = new Dictionary<string, double>();
            RY = new Dictionary<string, double>();
            RZ = new Dictionary<string, double>();

            // Initialize the dictionaries of calculated node reactions
            RxnFX = new Dictionary<string, double>();
            RxnFY = new Dictionary<string, double>();
            RxnFZ = new Dictionary<string, double>();
            RxnMX = new Dictionary<string, double>();
            RxnMY = new Dictionary<string, double>();
            RxnMZ = new Dictionary<string, double>();

            // Initialize all support conditions to `False`
            support_DX = false; 
            support_DY = false;
            support_DZ = false;
            support_RX = false;
            support_RY = false;
            support_RZ = false;
        }

        /// <summary>
        /// Returns the distance to another node.
        /// </summary>
        public double Distance(Node3D other)
        {
            return Math.Pow(
                Math.Pow(this.X - other.X,2)+
                Math.Pow(this.Y - other.Y, 2) +
                Math.Pow(this.Z - other.Z, 2)
                , 0.5);
        }
    }
}
