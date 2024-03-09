using RYBIM.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    public partial class FEModel3D
    {
        /// <summary>
        /// Adds a new material to the model.
        /// </summary>
        ///   <param name="name">A unique user-defined name for the material.</param>
        public void add_material(double E, double G, double nu, double rho, double? fy = null, string name = null)
        {
            // Name the material  or check it doesn't already exist
            if (name != null)
            {
                if (Materials.ContainsKey(name))
                {
                    throw new Exception($"Material  name '{name}' already exists");
                }
            }
            else
            {
                // As a guess, start with the length of the dictionary
                name = "M" + Materials.Count;
                var count = 1;
                while (Materials.ContainsKey(name))
                {
                    name = "M" + Materials.Count + count.ToString();
                    count++;
                }
            }
            var new_material = new Material(name, E, G, nu, rho, fy);
            Materials.Add(name, new_material);
        }
        /// <summary>
        /// Adds a load combination to the model.
        /// </summary>
        ///   <param name="name">A unique name for the load combination (e.g. '1.2D+1.6L+0.5S' or 'Gravity Combo').</param>
        ///   <param name="factors">A dictionary containing load cases and their corresponding factors (e.g. {'D':1.2, 'L':1.6, 'S':0.5}).</param>
        ///   <param name="combo_tags">A list of tags used to categorize load combinations. Default is `None`. This can be useful for filtering results later on,
        ///   or for limiting analysis to only those combinations with certain tags. This feature is provided for convenience. It is not necessary to use tags.</param>
        public void add_load_combo(Dictionary<string, double> factors, string name = null, List<string> combo_tags = null)
        {
            // Name the material  or check it doesn't already exist
            if (name != null)
            {
                if (LoadCombos.ContainsKey(name))
                {
                    throw new Exception($"Load Combos  name '{name}' already exists");
                }
            }
            else
            {
                // As a guess, start with the length of the dictionary
                name = "LC" + LoadCombos.Count;
                var count = 1;
                while (LoadCombos.ContainsKey(name))
                {
                    name = "LC" + LoadCombos.Count + count.ToString();
                    count++;
                }
            }
            var new_loadCombo = new LoadCombo(name, factors, combo_tags);
            LoadCombos.Add(name, new_loadCombo);
        }
        /// <summary>
        /// Returns the global displacement vector for the model.
        /// </summary>
        /// <param name="combo_name">The name of the load combination to get the results for. Defaults to 'Combo 1'.</param>
        /// <returns>The global displacement vector for the model</returns>
        public Vector D(string combo_name = "Combo 1")
        {
            return _D[combo_name];
        }
    }
}
