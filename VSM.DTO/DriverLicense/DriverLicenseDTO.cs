using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public  class DriverLicenseDTO: BaseDTO
    {
        public decimal id { get; set; }

        public string id_no { get; set; }

        public string modify_oper_id { get; set; }

        public string name { get; set; }

        public string sex { get; set; }

        public DateTime? birthday { get; set; }

        public string region_no { get; set; }

        public string addr { get; set; }

        public string work_unit { get; set; }

        public string permitted_card_type_no { get; set; }

        public string first_get_license_date { get; set; }

        public string valid_date_start { get; set; }

        public string valid_date_end { get; set; }

        public string id_card { get; set; }

        public string user_photo_path { get; set; }

        public LoginDTO userInfo { get; set; }

        public string user_photo_base64 { get; set; }

        public string permitted_car_type_name { get; set; }

        public string region_name { get; set; }




        
    }
}
