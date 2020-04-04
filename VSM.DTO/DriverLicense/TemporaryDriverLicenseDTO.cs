using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class TemporaryDriverLicenseDTO : BaseReportDTO
    {
        public decimal id  { get; set; }
        public string id_no {get;set;}
        public string name {get;set;}
        
        public string sex {get;set;}

        public string birthday { get; set; }

        public string now_addr { get; set; }

        public string old_addr { get; set; }

        public string nation_no { get; set; }

        public string folk { get; set; }

        public string check_man { get; set; }

        public string check_date { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }

        public string permitted_card_type_no { get; set; }

        public string region_no { get;set;}

        public LoginDTO userInfo { get;set;}

        public string user_photo_base64 { get; set; }

        public string user_photo_path { get; set; }

        public string permitted_car_type_name { get; set; }

        public string region_name { get; set; }

        public string TotalCount { get; set; }

        public string modify_oper_id { get; set; }

    }
}
