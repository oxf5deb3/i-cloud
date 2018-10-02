using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.DriverLicense
{
    public  class DriverLicenseDTO
    {
        public string id_no { get; set; }

        public string name { get; set; }

        public string sex { get; set; }

        public string birthday { get; set; }

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


    }
}
