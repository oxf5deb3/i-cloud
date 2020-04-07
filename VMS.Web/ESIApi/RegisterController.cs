using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using VMS.DTO;
using VMS.ESIApi.Models;
using VMS.IServices;
using VMS.ServiceProvider;

namespace VMS.ESIApi
{
    /// <summary>
    /// 注册中心 驾驶证，行驶证，临时驾驶证，临时行驶证，用户注册
    /// </summary>
    public class RegisterController : BaseApiController
    {
        /// <summary>
        /// 新增正式驾驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       [System.Web.Mvc.HttpPost]
       public BaseESIReponseDTO AddDriverLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();
            try
            {
                var dtoData = data.ToObject<DriverLicenseDTO>();
                dtoData.userInfo = new LoginDTO();
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
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }
        /// <summary>
        /// 新增临时驾驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddLsDriverLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();
            try
            {
                var dtoData = data.ToObject<TemporaryDriverLicenseDTO>();
                dtoData.userInfo = new LoginDTO();
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
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }
        /// <summary>
        /// 新增正式行驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddDrivingLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();

            try
            {

                var dtoData = data.ToObject<CarLicenseDTO>();

                dtoData.userInfo = new LoginDTO();

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddDrivingPermit(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }
        /// <summary>
        /// 新增临时行驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseESIReponseDTO AddLsDrivingLincese([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();

            try
            {

                var dtoData = data.ToObject<TemporaryDrivingPermitDTO>();

                dtoData.userInfo = new LoginDTO();

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddTemporaryDrivingPermit(dtoData);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }

        }
    }
}