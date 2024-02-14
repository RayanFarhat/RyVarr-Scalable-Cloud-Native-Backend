using RYBIM.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RYBIM.Analysis
{
    internal partial class Member3D
    {
        /// <summary>
        ///   Returns the member's local displacement vector.
        /// </summary>
        public Matrix d(string combo_name = "Combo 1")
        {
            return T().Multiply(D(combo_name));
        }
        /// <summary>
        ///  Returns the member's global displacement vector.
        /// </summary>
        public Matrix D(string combo_name = "Combo 1")
        {
            var D = new Matrix(12, 1);
            D[0, 0] = this.i_node.DX[combo_name];
            D[1, 0] = this.i_node.DY[combo_name];
            D[2, 0] = this.i_node.DZ[combo_name];
            D[3, 0] = this.i_node.RX[combo_name];
            D[4, 0] = this.i_node.RY[combo_name];
            D[5, 0] = this.i_node.RZ[combo_name];
            D[6, 0] = this.j_node.DX[combo_name];
            D[7, 0] = this.j_node.DY[combo_name];
            D[8, 0] = this.j_node.DZ[combo_name];
            D[9, 0] = this.j_node.RX[combo_name];
            D[10, 0] = this.j_node.RY[combo_name];
            D[11, 0] = this.j_node.RZ[combo_name];
            return D;
        }
    }
}
