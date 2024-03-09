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
        /// Performs first-order static analysis.
        /// </summary>
        /// <param name="check_stability">hen set to True, checks the stiffness matrix for any unstable degrees of freedom
        /// and reports them back to the console. This does add to the solution time. Defaults to True.</param>
        public void Analyze(bool check_stability = true , List<string> Combo_tags = null)
        {
            // Prepare the model for analysis
            Analyzer.prepareModel(this);
            var K = this.K();
            // Identify which load combinations have the tags the user has given
            var comboList = Analyzer.identify_combos(this);
            // Step through each load combination
            foreach (var combo in comboList)
            {
                // Get the global fixed end reaction vector
                var FER = this.FER(combo.Name);
                // Get the global nodal force vector      
                var P = this.P(combo.Name);
                // Calculate the unknown displacements D
                var D = K.Solve(P.Subtract(FER));
                // Store the calculated displacements to the model and the nodes in the model
                Analyzer.StoreDisplacements(this,D, combo);
            }
            Analyzer.calcReactions(this, Combo_tags);
        }
    }
}
