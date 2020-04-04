using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_role
    {
        public decimal id { get; set; }

        public string role_name { get; set; }

        public string status { get; set; }

        public string memo { get; set; }

        public string create_id { get; set;}

        public DateTime create_date { get; set; }
    }
}
