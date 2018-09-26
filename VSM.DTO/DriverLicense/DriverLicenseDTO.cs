using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class DriverLicenseDTO : BaseReportDTO
    {

        public string id_no{get;set;}
        public string name{get;set;}
        
        public string sex{get;set;}

        public string addr { get; set; }

        public string region_name { get; set; }

        public string permitted_car_type_name { get; set; }

        public string work_unit { get; set; }

        public DateTime first_get_license_date { get; set; }

        public DateTime valid_date_start { get; set; }
        public DateTime valid_date_end { get; set; }
        public DateTime birthday { get; set; }
    }
}
