using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class LoginLogDTO
    {
        public string region_no { get; set; }
        public string ip { get; set; }

        public string login_id { get; set; }

        public DateTime login_date { get; set; }
    }
}
