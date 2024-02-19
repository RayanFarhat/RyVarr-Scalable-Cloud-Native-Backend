using Autodesk.Revit.UI.Selection;
using RYBIM.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    public partial class Member3D
    {
        /// <summary>
        ///   Returns the member's elastic local end force vector for the given load combination.
        /// </summary>
        public Vector f(string combo_name = "Combo 1")
        {
            var f = k().Multiply(d(combo_name));
            f = Vector.FromMatrix(f);
            return f.Add(fer(combo_name));
        }
        /// <summary>
        ///   Returns the member's global end force vector for the given load combination.
        /// </summary>
        public Vector F(string combo_name = "Combo 1")
        {
            //T^-1 * f
            var T_inv = this.T().Invert();
            return T_inv.Multiply(f(combo_name));
        }
        /// <summary>
        ///    Returns the member's local fixed end reaction vector, ignoring the effects of end releases.
        /// </summary>
        public Vector fer(string combo_name = "Combo 1")
        {
            var FER = new Vector(12);

            var combo = this.Model.LoadCombos[combo_name];
            //Loop through each load case and factor in the load combination
            foreach (var kvp in combo.Factors)
            {
                var case_name = kvp.Key;
                var factor = kvp.Value;
                // Sum the fixed end reactions for the point loads & moments
                foreach (var ptLoad in this.PtLoads)
                {
                    // Check if the current point load corresponds to the current load case
                    if (ptLoad.CaseName == case_name)
                    {
                        if (ptLoad.direction == Direction.FX)
                            FER.Add(FixedEndReactions.FER_AxialPtLoad(factor*ptLoad.P,ptLoad.X,L()));
                        else if (ptLoad.direction == Direction.FY || ptLoad.direction == Direction.FZ)
                            FER.Add(FixedEndReactions.FER_PtLoad(factor * ptLoad.P, ptLoad.X, L(), ptLoad.direction));
                        else if (ptLoad.direction == Direction.MX)
                            FER.Add(FixedEndReactions.FER_Torque(factor * ptLoad.P, ptLoad.X, L()));
                        else if (ptLoad.direction == Direction.MY || ptLoad.direction == Direction.MZ)
                            FER.Add(FixedEndReactions.FER_Moment(factor * ptLoad.P, ptLoad.X, L(), ptLoad.direction));

                    }
                }
                // Sum the fixed end reactions for the distributed loads
                foreach (var distLoad in this.DistLoads)
                {
                    // Check if the current distributed load corresponds to the current load case
                    if (distLoad.CaseName == case_name)
                    {
                        if (distLoad.direction == Direction.FX)
                            FER.Add(FixedEndReactions.FER_AxialLinLoad(distLoad.x1,distLoad.x2,distLoad.w1,distLoad.w2,L()));
                        else if (distLoad.direction == Direction.FY || distLoad.direction == Direction.FZ)
                            FER.Add(FixedEndReactions.FER_LinLoad(distLoad.x1, distLoad.x2, distLoad.w1, distLoad.w2, L(), distLoad.direction));
                    }
                }
            }
            //Return the fixed end reaction vector, uncondensed
            return FER;
        }
        /// <summary>
        ///     Returns the global fixed end reaction vector.
        /// </summary>
        public Vector FER(string combo_name = "Combo 1")
        {
            return T().Invert().Multiply(fer(combo_name));
        }
    }
}
