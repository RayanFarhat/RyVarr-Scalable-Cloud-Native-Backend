using RYBIM.Mathematics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    internal partial class Member3D
    {
        /// <summary>
        ///  Returns the uncondensed local stiffness matrix for the member.
        /// </summary>
        public Matrix k_unc()
        {
            var E = this.E;
            var G = this.G;
            var Iy = this.Iy;
            var Iz = this.Iz;
            var J = this.J;
            var A = this.A;
            var L = this.L();

            double[,] kRaw = new double[,]
                {
                    { A*E/L, 0, 0, 0, 0, 0, -A*E/L, 0, 0, 0, 0, 0 },//DXi
                    { 0, 12*E*Iz/(L*L*L), 0, 0, 0, 6*E*Iz/(L*L), 0, -12*E*Iz/(L*L*L), 0, 0, 0, 6*E*Iz/(L*L) },//DYi
                    { 0, 0, 12*E*Iy/(L*L*L), 0, -6*E*Iy/(L*L), 0, 0, 0, -12*E*Iy/(L*L*L), 0, -6*E*Iy/(L*L), 0 },//DZi
                    { 0, 0, 0, G*J/L, 0, 0, 0, 0, 0, -G*J/L, 0, 0 },//RXi
                    { 0, 0, -6*E*Iy/(L*L), 0, 4*E*Iy/L, 0, 0, 0, 6*E*Iy/(L*L), 0, 2*E*Iy/L, 0 },//RYi
                    { 0, 6*E*Iz/(L*L), 0, 0, 0, 4*E*Iz/L, 0, -6*E*Iz/(L*L), 0, 0, 0, 2*E*Iz/L },//RZi
                    { -A*E/L, 0, 0, 0, 0, 0, A*E/L, 0, 0, 0, 0, 0 },//DXj 
                    { 0, -12*E*Iz/(L*L*L), 0, 0, 0, -6*E*Iz/(L*L), 0, 12*E*Iz/(L*L*L), 0, 0, 0, -6*E*Iz/(L*L) },//DYj
                    { 0, 0, -12*E*Iy/(L*L*L), 0, 6*E*Iy/(L*L), 0, 0, 0, 12*E*Iy/(L*L*L), 0, 6*E*Iy/(L*L), 0 },//DZi
                    { 0, 0, 0, -G*J/L, 0, 0, 0, 0, 0, G*J/L, 0, 0 },//RXj
                    { 0, 0, -6*E*Iy/(L*L), 0, 2*E*Iy/L, 0, 0, 0, 6*E*Iy/(L*L), 0, 4*E*Iy/L, 0 },//Yj
                    { 0, 6*E*Iz/(L*L), 0, 0, 0, 2*E*Iz/L, 0, -6*E*Iz/(L*L), 0, 0, 0, 4*E*Iz/L }//RZj
                };

            var k = new Matrix(kRaw);
            return k;
        }
    }
}
