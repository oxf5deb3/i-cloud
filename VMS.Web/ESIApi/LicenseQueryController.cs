using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.ESIApi
{
    /// <summary>
    /// (正式，临时)驾驶证查询，(正式，临时)行驶证查询
    /// </summary>
    public class LicenseQueryController: BaseApiController
    {
        /// <summary>
        /// 正式驾驶证查询(分页)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public GridResponseDTO<DriverLicenseDTO> DriverLicensePageList([FromBody]JObject data)
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
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                List<DriverLicenseDTO> lst = obj.QueryPage(condition, sort, order, pageindex, pagesize, ref total, ref err);
                // 读取图片base64内容
                lst.ForEach(p =>
                {
                    p.user_photo_base64 = FileUtils.fileToBase64(p.user_photo_path);
                });

                ret.total = total;
                ret.rows.AddRange(lst);
                try
                {
                    var logService = Instance<ILogService>.Create;
                    logService.WriteOperateLog(new OperateLogDTO() { oper_desc = "驾驶证查询", memo = "", region_no = "", oper_id = "", oper_date = DateTime.Now });
                }
                catch (Exception E)
                {

                }
                return ret;

            }
            catch (Exception ex)
            {
                //throw ex;
                Log4NetHelper.Error(this.GetType().FullName + ".Query", ex);
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 正式行驶证查询(分页)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public GridResponseDTO<CarLicenseDTO> CarLicensePageList([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<CarLicenseDTO>();
            try
            {

                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                var dtoData = data.ToObject<CarLicenseDTO>();
                string err = string.Empty;
                int total = 0;
                var obj = Instance<IDriverLicenseService>.Create;
                List<CarLicenseDTO> lst = obj.CarLicenseQueryPageList(condition, sort, order, pageindex, pagesize, ref total, ref err);
                lst.ForEach(p =>
                {
                    p.car_1_value = p.car_1_img_path == null ? "" : FileUtils.fileToBase64(p.car_1_img_path);
                    p.car_2_value = p.car_2_img_path == null ? "" : FileUtils.fileToBase64(p.car_2_img_path);
                    p.vin_no_value = p.vin_no_img_path == null ? "" : FileUtils.fileToBase64(p.vin_no_img_path);
                    p.engine_no_value = p.engine_no_img_path == null ? "" : FileUtils.fileToBase64(p.engine_no_img_path);
                    p.user_photo_base64 = p.user_photo_path == null ? "" : FileUtils.fileToBase64(p.user_photo_path);
                });
                ret.total = total;
                ret.rows.AddRange(lst);
                return ret;

            }
            catch (Exception ex)
            {
                //throw ex;
                Log4NetHelper.Error(this.GetType().FullName + ".Query", ex);
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 临时驾驶证查询(分页)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public GridResponseDTO<DriverLicenseDTO> LSDriverLicensePageList([FromBody]JObject data)
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
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                List<DriverLicenseDTO> lst = obj.QueryPage(condition, sort, order, pageindex, pagesize, ref total, ref err);
                // 读取图片base64内容
                lst.ForEach(p =>
                {
                    p.user_photo_base64 = FileUtils.fileToBase64(p.user_photo_path);
                });

                ret.total = total;
                ret.rows.AddRange(lst);
                try
                {
                    var logService = Instance<ILogService>.Create;
                    logService.WriteOperateLog(new OperateLogDTO() { oper_desc = "驾驶证查询", memo = "", region_no = "", oper_id ="", oper_date = DateTime.Now });
                }
                catch (Exception E)
                {

                }
                return ret;

            }
            catch (Exception ex)
            {
                //throw ex;
                Log4NetHelper.Error(this.GetType().FullName + ".Query", ex);
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }


       
        /// <summary>
        /// 临时行驶证查询(分页)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public GridResponseDTO<DriverLicenseDTO> LSCarLicensePageList([FromBody]JObject data)
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
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                List<DriverLicenseDTO> lst = obj.QueryPage(condition, sort, order, pageindex, pagesize, ref total, ref err);
                // 读取图片base64内容
                lst.ForEach(p =>
                {
                    p.user_photo_base64 = FileUtils.fileToBase64(p.user_photo_path);
                });

                ret.total = total;
                ret.rows.AddRange(lst);
                try
                {
                    var logService = Instance<ILogService>.Create;
                    logService.WriteOperateLog(new OperateLogDTO() { oper_desc = "驾驶证查询", memo = "", region_no = "", oper_id = "", oper_date = DateTime.Now });
                }
                catch (Exception E)
                {

                }
                return ret;

            }
            catch (Exception ex)
            {
                //throw ex;
                Log4NetHelper.Error(this.GetType().FullName + ".Query", ex);
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
    }
}