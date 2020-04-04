using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_bd_breakrules_type
    {
        public decimal id { get; set; }
        public string name { get; set; }
        public string punish_desc { get; set; }
        public string memo { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
    }
}
