using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    //系统日志接口
    public class LogApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<LoginLogDTO> LoginLogQuery([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<LoginLogDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                string err = string.Empty;
                var obj = Instance<ILoginLogService>.Create;
                var lst = obj.Query<LoginLogDTO>(condition, false, pagesize, pageindex, false, "id", ref total, ref err);
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
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<OperateLogDTO> OperateLogQuery([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<OperateLogDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                string err = string.Empty;
                var obj = Instance<IOperateLogService>.Create;
                var lst = obj.Query<OperateLogDTO>(condition, false, pagesize, pageindex, true, "id", ref total, ref err);
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
    }
}