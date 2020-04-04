using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_group_role
    {

        public decimal id{get;set;}
        public decimal role_id{get;set;}
        public string role_name { get; set; }
        public decimal group_id { get; set; }

        public string create_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
