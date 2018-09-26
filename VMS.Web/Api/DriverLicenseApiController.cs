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
    public class DriverLicenseApiController : ApiController
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
    }
}
