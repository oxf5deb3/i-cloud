using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO.DrivingPermit
{
    public  class DrivingPermitDTO
    {
        public string card_owner { get; set; }
        public string duty { get; set; }
        public string work_unit { get; set; }
        public string region_no { get; set; }
        public string plate_no { get; set; }
        public string brand_no { get; set; }
        public string motor_no { get; set; }
        public string cardframe_no { get; set; }
        public string card_color { get; set; }
        public string produce_date { get; set; }
        public string issue_license_date { get; set; }
    }
}
