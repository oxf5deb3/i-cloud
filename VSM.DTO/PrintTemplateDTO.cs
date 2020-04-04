using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class PrintTemplateDTO
    {
        public decimal? id { get; set; }

        public string status { get; set; }

        public int type { get; set; }

        public string oper_id { get; set; }

        public string name { get; set; }

        public string html { get; set; }

        public string selected { get; set; }
    }
}
