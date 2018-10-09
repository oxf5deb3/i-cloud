using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Statistical;

namespace VMS.IServices
{
    public interface IStatisticalService:IService
    {
        DriverLicenseStatisticalDTO queryByJZ(String year);

        DriverLicenseStatisticalDTO queryByXS(String year);
    }
}
