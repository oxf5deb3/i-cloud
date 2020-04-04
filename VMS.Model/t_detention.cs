using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_detention
    {
        public int detention_id { get; set; }

        public string total_detention { get; set; }
        public string total_detention_boy { get; set; }
        public string total_detention_girl { get; set; }
        public string already_release { get; set; }
        public string now_detention_total { get; set; }
        public string now_detention_boy { get; set; }

        public string now_detention_girl { get; set; }
        public string remark { get; set; }
        public string traffic_accident_detention { get; set; }
        public string not_cooperate_detention { get; set; }
        public string devolve_police_total { get; set; }
        public string devolve_police_boy { get; set; }
        public string devolve_police_girl { get; set; }
        public string devolve_procuratorate_total { get; set; }
        public string devolve_procuratorate_boy { get; set; }
        public string devolve_procuratorate_girl { get; set; }

    }
}
