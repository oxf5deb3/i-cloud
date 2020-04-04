using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_stolen
    {
        public int stolen_id { get; set; }

        public string accepting_cases { get; set; }

        public string total_stolencar_big { get; set; }
        public string total_stolencar_small { get; set; }
        public string total_stolencar_motorcycle { get; set; }
        public string recover_bigcar { get; set; }

        public string recover_smallcar { get; set; }
        public string recover_motorcycle { get; set; }
        public string undetected { get; set; }
        public string remark { get; set; }


    }
}
