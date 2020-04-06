using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_sys_user_group
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public decimal id { get; set; }

        public string user_id { get; set; }

        public string user_name { get; set; }

        public string group_id { get; set; }

        public string create_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
