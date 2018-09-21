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

        public DateTime birthday { get; set; }
    }
}
