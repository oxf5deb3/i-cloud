using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VMS.ESIApi.Models
{
    public class ESIUserLoginDTO
    {
        public string user_id { get; set; }

        public string user_name { get; set; }

        public string email { get; set; }

        public string user_type { get; set; }

        public DateTime create_date { get; set; }

        public string token { get; set; }

        public string menu_right { get; set; }
    }
}