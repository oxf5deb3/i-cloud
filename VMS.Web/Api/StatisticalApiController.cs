using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using VMS.DTO;
using VMS.DTO.Statistical;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class StatisticalApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<DriverLicenseStatisticalDTO> queryDriverLicenseStatistical([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<DriverLicenseStatisticalDTO>();
            try
            {
                #region 参数检验

                #endregion
                
                var condition = data.ToDictionary();
                var year = CommonHelper.GetString(condition["year"]);

                var dtoData = data.ToObject<DriverLicenseStatisticalDTO>();
                string err = string.Empty;
                var obj = Instance<IStatisticalService>.Create;
                DriverLicenseStatisticalDTO model = obj.queryByJZ(year);
                ret.rows.Add(model);
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