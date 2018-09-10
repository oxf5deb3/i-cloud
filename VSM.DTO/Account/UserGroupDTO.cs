using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class UserGroupDTO
    {
        public decimal id { get; set; }

        public string group_name { get; set; }

        public string create_id { get; set; }

        public DateTime create_date { get; set; }

        public string status { get; set; }

        public string memo { get; set; }
    }
}
