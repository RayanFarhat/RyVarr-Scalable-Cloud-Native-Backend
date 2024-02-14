using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYBIM.Analysis
{
    public struct DistributedLoad
    {
        /// <summary>
        /// e global direction the load is being applied in.
        /// </summary>
        public Direction direction { get; set; }
        /// <summary>
        ///The start place of the load.
        /// </summary>
        public double x1 { get; set; }
        /// <summary>
        ///The end place of the load.
        /// </summary>
        public double x2 { get; set; }
        /// <summary>
        ///The start numeric value (magnitude) of the load from x1.
        /// </summary>
        public double w1 { get; set; }
        /// <summary>
        ///The end numeric value (magnitude) of the load to x2.
        /// </summary>
        public double w2 { get; set; }
    }
}
