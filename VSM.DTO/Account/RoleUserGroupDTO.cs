using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class RoleUserGroupDTO
    {
        public RoleUserGroupDTO()
        {
            users = new List<innerUser>();
            groups = new List<innerGroup>();
        }
        public decimal id { get; set; }

        public string role_name { get; set; }

        public string create_id { get; set; }

        public DateTime create_date { get; set; }

        public string status { get; set; }

        public string memo { get; set; }

        public List<innerUser> users { get; set; }
        public List<innerGroup> groups { get; set; }

        public string user_id { get; set; }

        public string user_name { get; set; }
    }

    public class innerGroup
    {
        public string id { get; set; }

        public string group_name { get; set; }
    }
    public class innerUser
    {

        public string user_id { get; set; }

        public string user_name { get; set; }
    }

}
