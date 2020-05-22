using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.Model
{
    public class t_accident_records
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public decimal id { get; set; }
        public DateTime happen_date { get; set; }
        public string happen_addr { get; set; }
        public string first_party_man { get; set; }
        public string first_party_addr { get; set; }
        public string second_party_man { get; set; }
        public string second_party_addr { get; set; }
        public string accident_desc { get; set; }
        public string mediation_unit { get; set; }
        public DateTime mediation_date { get; set; }
        public string draw_recorder { get; set; }
        public string accident_mediator { get; set; }
        public string oper_id { get; set; }
        public DateTime oper_date { get; set; }
        public string modify_oper_id { get; set; }
        public DateTime modify_date { get; set; }
        public string img_url { get; set; }



        public string duty { get; set; }

        public string dingPartyAddr { get; set; }

        public string dingPartyMan { get; set; }

        public string bingPartyMan { get; set; }

        public string bingPartyAddr { get; set; }
    }
}
