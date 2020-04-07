using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMS.DTO
{
    public class DCShowDTO
    {
        public DriverLicenseDTO dl { get; set; }

        public CarLicenseDTO dp { get; set; }

        public TemporaryDriverLicenseDTO tdl { get; set; }

        public TemporaryDrivingPermitDTO tdp { get; set; }
    }
}
