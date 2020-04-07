using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;

namespace VMS.IServices
{
    public interface ICarNumberService : IService
    {
        List<CarLicenseDTO> queryDrivingPermitByPage(int index, int pageSize, CarLicenseDTO data);

        List<TemporaryDrivingPermitDTO> queryTemporaryDrivingByPage(int index, int pageSize, TemporaryDrivingPermitDTO data);

        bool ModifyCarNumber(CarLicenseDTO dto);

        bool ModifyTempCarNumber(TemporaryDrivingPermitDTO dto);

    }
}
