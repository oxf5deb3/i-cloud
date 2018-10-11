using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class TemporaryDrivingPermitDTO
    {
        public decimal id { get; set; }
        public string check_man { get; set; }
        public string old_addr { get; set; }
        public string now_addr { get; set; }
        public string folk { get; set; }
        public string nation_no { get; set; }
        public string birthday { get; set; }
        public string sex { get; set; }
        public string permitted_card_type_no { get; set; }
        public string name { get; set; }
        public string check_date { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }
        public string car_type { get; set; }



        public string label_type { get; set; }
        public string cargo { get; set; }
        public string passenger { get; set; }

        public string vin { get; set; }
        public string engine_no { get; set; }
        public string temp_number { get; set; }

        public LoginDTO userInfo { get; set; }

        public string user_photo_base64 { get; set; }

        public string id_no { get; set; }

        public string id_card { get; set; }

        public string addr { get; set; }

        public string region_no { get; set; }



        public string car_1_value { get; set; }
        public string car_2_value { get; set; }

        public string engine_no_value { get; set; }

        public string vin_no_value { get; set; }

        public string TotalCount { get; set; }

        public string region_name { get; set; }

        public string permitted_card_type_name { get; set; }
        public string user_photo_path { get; set; }
        public string car_1_img_path { get; set; }
        public string car_2_img_path { get; set; }
        public string engine_no_img_path { get; set; }
        public string vin_no_img_path { get; set; }
    }
}
