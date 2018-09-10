using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_resource
    {
        public string id { get; set; }

        public string pid { get; set; }

        public string level { get; set; }

        public string res_uri { get; set; }

        public string res_img { get; set; }

        public string res_desc { get; set; }

        public string res_type_id { get; set; }

        public string create_id { get; set; }

        public DateTime create_date { get; set; }

        public string sort_code { get; set; }
    }
}
