using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockyHockey.Common
{
    [Serializable]
    public class PuckDetectionConfig
    {
        /// <summary>
        /// minimal size of drawn circles
        /// </summary>
        public int MinDetectionRadius { get; set; } = 10;

        /// <summary>
        /// maximal size of found circles
        /// </summary>
        public int MaxDetectionRadius { get; set; } = 30;

        /// <summary>
        /// miimal distance between circles
        /// </summary>
        public double MinDistance { get; set; } = 100;

        /// <summary>
        /// some kind of ratio between input and processing object
        /// </summary>
        public double DP { get; set; } = 1;

        /// <summary>
        /// algorithm specific param; some kind of ratio between input and processing object
        /// </summary>
        public double Param1 { get; set; } = 1;

        /// <summary>
        /// algorithm specific param; seems to determine what minimal size the objects need so that circles can be found around them
        /// </summary>
        public double Param2 { get; set; } = 15;
    }
}
