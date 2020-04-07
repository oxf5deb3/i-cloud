using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class CarNumberApiController : BaseApiController
    {

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyTempCarNumber([FromBody]JObject data) {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TemporaryDrivingPermitDTO>();
                var service = Instance<ICarNumberService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyTempCarNumber(dto);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "修改失败";
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


        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyCarNumber([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<CarLicenseDTO>();
                var service = Instance<ICarNumberService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyCarNumber(dto);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "修改失败";
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

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<CarLicenseDTO> queryDrivingPermit([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<CarLicenseDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var dtoData = data.ToObject<CarLicenseDTO>();
                string err = string.Empty;
                var obj = Instance<ICarNumberService>.Create;
                List<CarLicenseDTO> lst = obj.queryDrivingPermitByPage(pageindex, pagesize, dtoData);
                ret.total = Int16.Parse(lst[0].TotalCount);
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


        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public BaseResponseDTO queryTemporaryDrivingByPage([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<TemporaryDrivingPermitDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var dtoData = data.ToObject<TemporaryDrivingPermitDTO>();
                string err = string.Empty;
                var obj = Instance<ICarNumberService>.Create;
                List<TemporaryDrivingPermitDTO> lst = obj.queryTemporaryDrivingByPage(pageindex, pagesize, dtoData);
                ret.total = Int16.Parse(lst[0].TotalCount);
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


    }
}