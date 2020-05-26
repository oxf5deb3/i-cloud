using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS.ESIApi.Models
{
    public class ESIUserProfileDTO
    {
        public string user_id { get; set; }

        public string user_name { get; set; }

        public string sex { get; set; }

        public int age { get; set; }

        public string tel { get; set; }
        public string email { get; set; }

        public string status { get; set; }

        public string user_type { get; set; }

        public DateTime last_login_time { get; set; }

        public DateTime create_date { get; set; }

        public string helper_tel { get; set; }
    }
}