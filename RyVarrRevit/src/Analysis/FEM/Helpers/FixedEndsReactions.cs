using RyVarrRevit.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyVarrRevit.Analysis
{
    internal static class FixedEndsReactions
    {
        /// <summary>
        ///   Returns the fixed end reaction vector for a point load.
        /// </summary>
        ///   <param name="P">The magnitude of the point load</param>
        ///   <param name="x">The location of the point load relative to the start of the member</param>
        ///   <param name="L">The length of the member</param>
        ///   <param name="D">The direction of the point load. Must be one of FY or FZ</param>
        public static Vector FER_PtLoad(double P, double x, double L, Direction D)
        {
            var b = L - x;
            var FER = new Vector(12);
            if (D == Direction.FY || D == Direction.Fy)
            {
                FER[1] = -P * Math.Pow(b, 2) * (L + 2 * x) / Math.Pow(L, 3);
                FER[5] = -P * x * Math.Pow(b, 2) / Math.Pow(L, 2);
                FER[7] = -P * Math.Pow(x, 2) * (L + 2 * b) / Math.Pow(L, 3);
                FER[11] = P * b * Math.Pow(x, 2) / Math.Pow(L, 2);
            }
            else if(D == Direction.FZ || D == Direction.Fz)
            {
                FER[2] = -P * Math.Pow(b, 2) * (L + 2 * x) / Math.Pow(L, 3);
                FER[4] = P * x * Math.Pow(b, 2) / Math.Pow(L, 2);
                FER[8] = -P * Math.Pow(x, 2) * (L + 2 * b) / Math.Pow(L, 3);
                FER[10] = -P * b * Math.Pow(x, 2) / Math.Pow(L, 2);
            }
            return FER;
        }
        /// <summary>
        ///   Returns the fixed end reaction vector for a concentrated moment.
        /// </summary>
        ///   <param name="M">The magnitude of the moment</param>
        ///   <param name="x">The location of the moment relative to the start of the member</param>
        ///   <param name="L">The length of the member</param>
        ///   <param name="D">The direction of the point load. Must be one of MY or MZ</param>
        public static Vector FER_Moment(double M, double x, double L, Direction D)
        {
            var b = L - x;
            var FER = new Vector(12);
            if (D == Direction.MZ || D == Direction.Mz)
            {
                FER[1] = 6 * M * x * b / Math.Pow(L, 3);
                FER[5] = M * b * (2*x - b)/Math.Pow(L, 2);
                FER[7] = -6 * M * x * b / Math.Pow(L, 3);
                FER[11] = M * x * (2 * b - x) / Math.Pow(L, 2);
            }
            else if (D == Direction.MY || D == Direction.My)
            {
                FER[2] = -6 * M * x * b / Math.Pow(L, 3);
                FER[4] = M * b * (2 * x - b) / Math.Pow(L, 2);
                FER[8] = 6 * M * x * b / Math.Pow(L, 3);
                FER[10] = M * x * (2 * b - x) / Math.Pow(L, 2);
            }
            return FER;
        }

        /// <summary>
        ///   Returns the fixed end reaction vector for an axial point load(Direction FX).
        /// </summary>
        ///   <param name="P">The magnitude of the point load</param>
        ///   <param name="x">The location of the point load relative to the start of the member</param>
        ///   <param name="L">The length of the member</param>
        public static Vector FER_AxialPtLoad(double P, double x, double L)
        {
            var b = L - x;
            var FER = new Vector(12);
            FER[0] = -P * b / L;
            FER[6] = -P * x / L;
            return FER;
        }
        /// <summary>
        ///   Returns the fixed end reaction vector for a distributed axial load(Direction FX).
        /// </summary>
        ///   <param name="x1">The start place of the load relative to the start of the member.</param>
        ///   <param name="x2">The end place of the load relative to the start of the member.</param>
        ///   <param name="w1">The start numeric value (magnitude) of the load from x1</param>
        ///   <param name="w2">The end numeric value (magnitude) of the load to x2.</param>
        ///   <param name="L">The length of the member</param>
        public static Vector FER_AxialLinLoad(double x1, double x2, double w1, double w2, double L)
        {
            //TODO: make sure the var x is right (the correct location)
            // trapezoidal distributed load is equal to w1 triangle load + w2 triangle load
            var p = Math.Abs(x1 - x2)*(w1+w2)/2;
            // location of load based on the law 
            var x = Math.Abs(x1 - x2)*(w1 + 2*w2)/(3*(w1+w2));
            if (w1 == 0 && w2 == 0)
            {
                return new Vector(12);
            }
            return FER_AxialPtLoad(p,x,L);
        }
        /// <summary>
        ///   Returns the fixed end reaction vector for a concentrated torque.
        /// </summary>
        ///   <param name="T">The magnitude of the torque</param>
        ///   <param name="x">The location of the torque relative to the start of the member</param>
        ///   <param name="L">The length of the member</param>
        public static Vector FER_Torque(double T, double x, double L)
        {
            var b = L - x;
            var FER = new Vector(12);
            FER[3] = -T * b / L;
            FER[9] = -T * x / L;
            return FER;
        }
        /// <summary>
        ///   eturns the fixed end reaction vector for a linear distributed load.
        /// </summary>
        ///   <param name="x1">The start place of the load relative to the start of the member.</param>
        ///   <param name="x2">The end place of the load relative to the start of the member.</param>
        ///   <param name="w1">The start numeric value (magnitude) of the load from x1</param>
        ///   <param name="w2">The end numeric value (magnitude) of the load to x2.</param>
        ///   <param name="L">The length of the member</param>
        ///   <param name="D">The direction of the point load. Must be one of FY or FZ</param>

        public static Vector FER_LinLoad(double x1, double x2, double w1, double w2, double L, Direction D)
        {
            // trapezoidal distributed load is equal to w1 triangle load + w2 triangle load
            var p = Math.Abs(x1 - x2) * (w1 + w2) / 2;
            // location of load based on the law 
            var x = Math.Abs(x1 - x2) * (w1 + 2 * w2) / (3 * (w1 + w2));
            if (w1 == 0 && w2 == 0)
            {
                return new Vector(12);
            }
            return FER_PtLoad(p,x,L,D);
        }
    }
}
