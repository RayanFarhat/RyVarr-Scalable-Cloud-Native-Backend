using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    public class Material
    {
        #region Properties
        public string Name { get; protected set; }
        public double E { get; protected set; }
        public double G { get; protected set; }
        public double nu { get; protected set; }
        public double rho { get; protected set; }
        public double? fy { get; protected set; }

        #endregion
        public Material(string name, double E, double G, double nu, double rho, double? fy=null)
        {
            Name = name;
            this.E = E;
            this.G = G;
            this.nu = nu;
            this.rho = rho;
            this.fy = fy;
        }
    }
}
