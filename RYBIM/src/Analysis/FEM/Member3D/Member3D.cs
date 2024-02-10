using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    /// <summary>
    ///  A class representing a 3D frame element in a finite element model.
    /// </summary>
    internal partial class Member3D
    {
        #region Main Properties
        /// <summary>
        ///  A unique name for the member given by the user
        /// </summary>
        public string Name { get; protected set; }

        public Node3D i_node { get; protected set; }
        public Node3D j_node { get; protected set; }

        /// <summary>
        ///  Members need a link to the model they belong to
        /// </summary>
        public FEModel3D Model { get; protected set; }

        #endregion
        #region Material Properties
        public string MaterialName { get; protected set; }
        /// <summary>
        ///  The modulus of elasticity of the element
        /// </summary>
        public double E { get; protected set; }
        /// <summary>
        /// The shear modulus of the element
        /// </summary>
        public double G { get; protected set; }
        #endregion
        #region Section Properties
        /// <summary>
        ///  The cross-sectional area
        /// </summary>
        public double A { get; protected set; }
        /// <summary>
        ///  The y-axis moment of inertia
        /// </summary>
        public double Iy { get; protected set; }
        /// <summary>
        ///  The z-axis moment of inertia
        /// </summary>
        public double Iz { get; protected set; }
        /// <summary>
        ///  The torsional constant
        /// </summary>
        public double J { get; protected set; }
        #endregion


        public Member3D(string name, Node3D i,Node3D j,string materialName,FEModel3D model,
            double Iy, double Iz, double J, double A) {
            this.Name = name;
            this.i_node = i;
            this.j_node = j;
            this.MaterialName = materialName;
            this.Model = model;
            this.E = model.Materials[materialName].E;
            this.G = model.Materials[materialName].G;
            this.A = A;
            this.Iy = Iy;
            this.Iz = Iz;
            this.J = J;
        }

        public double L()
        {
            return this.i_node.Distance(this.j_node);
        }
    }
}
