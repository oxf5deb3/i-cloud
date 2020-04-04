using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class BreakRulesDTO
    {
        public decimal? id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public int age { get; set; }
        public string phone { get; set; }
        public string id_card { get; set; }
        public string car_no { get; set; }
        public string driver_no { get; set; }
        public string driving_no { get; set; }
        public string carframe_no { get; set; }
        public decimal breakrule_type_id { get; set; }
        public string breakrule_type_name
        {
            get; set;
        }
        public string breakrule_addr { get; set; }
        public DateTime breakrule_date { get; set; }
        public string memo { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
    }
}
