using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using VMS.DTO;
using VMS.DTO.DriverLicense;
using VMS.DTO.DrivingPermit;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class DriverLicenseApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<DriverLicenseDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<DriverLicenseDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageindex = CommonHelper.GetInt(condition["page"],0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                var lst = obj.Query<DriverLicenseDTO>(condition, false, pagesize, pageindex, true, "id_no", ref total, ref err);
                ret.total = total;
                ret.rows.AddRange(lst);
                return ret;

            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        //新增正式驾照
        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        public BaseResponseDTO addZsDriverLicense([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();

            try
            {

                var dtoData = data.ToObject<DriverLicenseDTO>();

                dtoData.userInfo = operInfo;

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddDrivingLicense(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }

        //新增临时驾照
        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        public BaseResponseDTO addLsDriverLicense([FromBody]JObject data)
        {

            var ret = new BaseResponseDTO();
            try {
                

                var dtoData = data.ToObject<TemporaryDriverLicenseDTO>();

                dtoData.userInfo = operInfo;

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddTemporaryDrivingLicense(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }
            }
            catch (Exception ex)
            {
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }
        //初始化证件编码
        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        public BaseResponseDTO initCarNo([FromBody]JObject data)
        {
            BaseResponseDTO ret = new BaseResponseDTO();
           var type= data.ToObject<DriverLicenseDTO>();
            ret.message=IDUtils.generateId(int.Parse(type.id_no));
            return ret;
        }

        //新增正式驾照
        [System.Web.Mvc.HttpPost]
        [AllowAnonymous]
        public BaseResponseDTO addLSXSDriverLicense([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();

            try
            {

                var dtoData = data.ToObject<TemporaryDrivingPermitDTO>();

                dtoData.userInfo = operInfo;

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddTemporaryDrivingPermit(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {

                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }

        

    }

   

     

}
