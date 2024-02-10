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

        #endregion
        public Node3D(string name, double X, double Y, double Z) {
            this.Name = name;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
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
