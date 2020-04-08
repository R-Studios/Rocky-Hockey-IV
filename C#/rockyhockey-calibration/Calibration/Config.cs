using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calibration
{
    [Serializable]

    public class Config
    {
        private Config() { }
        private static Config instance;

        public static Config Instance => instance ?? (instance = new Config());

        public string LastDirectoryUsed { get; set; }
    }
    
}
