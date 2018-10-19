using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_fire_equipment_register
    {
        public decimal id { get; set; }

        public string eq_name { get; set; }

        public string install_addr { get; set; }

        public string usage_desc { get; set; }

        public DateTime install_date { get; set; }

        public string person_liable { get; set; }

        public string oper_id { get; set; }

        public DateTime oper_date { get; set; }

        public string modify_oper_id { get; set; }

        public DateTime modify_date { get; set; }

        public string img_url { get; set; }
    }
}
