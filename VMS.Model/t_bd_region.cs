using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_bd_region
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string region_no { get; set; }

        public string region_name { get; set; }

        public string memo { get; set; }
    }
}
