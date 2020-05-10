using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.Account
{
    public class SysNewsDTO
    {
        public decimal? id { get; set; }
        public string title { get; set; }
        public string user_name { get; set; }
        public string create_id { get; set; }
        public DateTime? create_date { get; set; }
    }
}
