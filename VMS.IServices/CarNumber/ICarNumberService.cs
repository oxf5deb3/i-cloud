using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.DrivingPermit;

namespace VMS.IServices
{
    public interface ICarNumberService : IService
    {
        List<DrivingPermitDTO> queryDrivingPermitByPage(int index, int pageSize, DrivingPermitDTO data);

        List<TemporaryDrivingPermitDTO> queryTemporaryDrivingByPage(int index, int pageSize, TemporaryDrivingPermitDTO data);

    }
}
