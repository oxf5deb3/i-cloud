using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class ResDTO
    {
        public string id { get; set; }

        public string pid { get; set; }

        public string level { get; set; }
        public string res_desc { get; set; }

        public string res_type_id { get; set; }
        public string res_type_oper_id { get; set; }
        public string sort_code { get; set; }
        public string type_desc { get; set; }
    }
}
