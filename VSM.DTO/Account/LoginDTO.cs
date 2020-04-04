using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class LoginDTO
    {
        public string user_id { get; set; }
        public string user_name { get; set; }

        public string user_pwd { get; set; }

        public string login_ip { get; set; }
    }
}
