using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.DTO.DriverLicense;
using VMS.DTO.DrivingPermit;

namespace VMS.IServices
{
    public interface IDriverLicenseService : IService
    {
        List<DriverLicenseDTO> Query<DriverLicenseDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err);

        /// <summary>
        /// 根据名字查询临时驾驶证
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool QueryTemporaryDrivingLicense(string name);

        /// <summary>
        /// 添加临时驾驶证
        /// </summary>
        /// <param name="driverLicenseDTO"></param>
        /// <returns></returns>
        bool AddTemporaryDrivingLicense(TemporaryDriverLicenseDTO temporaryDriverLicenseDTO);

        List<TemporaryDriverLicenseDTO> queryTemporaryDrivingLicense(int index, int pageSize, TemporaryDriverLicenseDTO data);

        /// <summary>
        /// 添加驾驶证
        /// </summary>
        /// <param name="driverLicenseDTO"></param>
        /// <returns></returns>
        bool AddDrivingLicense(DriverLicenseDTO driverLicenseDTO);

        /**
         * 正式行驶证分页查询
         * */
        List<DrivingPermitDTO> queryDrivingPermitByPage(int index, int pageSize, DrivingPermitDTO data);

        /// <summary>
        /// 添加临时行驶证
        /// </summary>
        /// <param name="temporaryDrivingPermitDTO"></param>
        /// <returns></returns>
        bool AddTemporaryDrivingPermit(TemporaryDrivingPermitDTO temporaryDrivingPermitDTO);

        List<TemporaryDrivingPermitDTO> queryTemporaryDrivingByPage(int index, int pageSize, TemporaryDrivingPermitDTO data);

        /// <summary>
        /// 添加行驶证
        /// </summary>
        /// <param name="drivingPermitDTO"></param>
        /// <returns></returns>
        bool AddDrivingPermit(DrivingPermitDTO drivingPermitDTO);


        BaseResponseDTO validataTemp(String temp_car_number, String engine_no, String car_frame_no,String id_card);

        BaseResponseDTO validata(String car_number, String engine_no, String car_frame_no);

        BaseResponseDTO validataByDriverLicense(String id_card);


    }
}
