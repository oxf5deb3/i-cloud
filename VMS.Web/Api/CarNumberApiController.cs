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
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<DrivingPermitDTO> queryDrivingPermit([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<DrivingPermitDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var dtoData = data.ToObject<DrivingPermitDTO>();
                string err = string.Empty;
                var obj = Instance<ICarNumberService>.Create;
                List<DrivingPermitDTO> lst = obj.queryDrivingPermitByPage(pageindex, pagesize, dtoData);
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