using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class OperateLogDTO
    {
        public string region_no { get; set; }

        public string oper_desc { get; set; }

        public string memo{get; set;}

        public string oper_id { get; set; }

        public DateTime oper_date { get; set; }
    }
}
