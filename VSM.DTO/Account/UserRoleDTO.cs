using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class UserRoleDTO
    {
        public decimal id { get; set; }

        public string user_id { get; set; }

        public string user_pwd { get; set; }

        public string user_name { get; set; }

        public string sex { get; set; }

        public int age { get; set; }

        public string tel { get; set; }
        public string email { get; set; }

        public string status { get; set; }

        public DateTime last_login_time { get; set; }

        public DateTime create_date { get; set; }

       public  List<RoleDTO> roles { get; set; }
    }
}
