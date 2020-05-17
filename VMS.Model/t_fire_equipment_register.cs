using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_fire_equipment_register
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public decimal id { get; set; }

        public string eq_name { get; set; }

        public string eq_type { get; set; }

        public int? eq_qty { get; set; }

        public string install_addr { get; set; }

        public string usage_desc { get; set; }

        public DateTime install_date { get; set; }

        public string person_liable { get; set; }

        public string oper_id { get; set; }

        public DateTime oper_date { get; set; }

        public string modify_oper_id { get; set; }

        public DateTime modify_date { get; set; }

        public string img_url { get; set; }

        public string img0_url { get; set; }

        public string img1_url { get; set; }
    }
}
