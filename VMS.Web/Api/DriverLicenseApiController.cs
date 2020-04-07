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
    public class DriverLicenseApiController : BaseApiController
    {
        /// <summary>
        /// 正式驾驶证查询
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort")?CommonHelper.GetString(condition["sort"]):"";
                var order = condition.ContainsKey("order")?(CommonHelper.GetString(condition["order"])=="asc"?true:false):true;
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                List<DriverLicenseDTO> lst = obj.QueryPage(condition, sort, order, pageindex, pagesize, ref total, ref err);
                //List<DriverLicenseDTO> lst = obj.Query<DriverLicenseDTO>(condition, false, pagesize, pageindex, true, "id_no", ref total, ref err);
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
                    logService.WriteOperateLog(new OperateLogDTO() { oper_desc = "驾驶证查询", memo = "", region_no = "", oper_id = operInfo.user_id, oper_date = DateTime.Now });
                }
                catch (Exception E)
                {

                }
                return ret;

            }
            catch (Exception ex)
            {
                Log4NetHelper.Error(this.GetType().FullName + ".Query", ex);
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        /// <summary>
        /// 查询正式行驶证(分页)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO CarLicenseByPage([FromBody]JObject data)
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
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        // 修改正式驾照
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyZsDriverLicense([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<DriverLicenseDTO>();
                var service = Instance<IDriverLicenseService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyZsDriverLicense(dto);

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

        // 修改临时驾照
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyLsDriverLicense([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TemporaryDriverLicenseDTO>();
                var service = Instance<IDriverLicenseService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyLsDriverLicense(dto);

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

        // 修改正式行驶证
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyZsDrivingPermit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<CarLicenseDTO>();
                var service = Instance<IDriverLicenseService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyZsDrivingPermit(dto);

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

        // 修改临时行驶证
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyLsDrivingPermit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TemporaryDrivingPermitDTO>();
                var service = Instance<IDriverLicenseService>.Create;
                dto.userInfo = operInfo;
                bool res = service.ModifyLsDrivingPermit(dto);

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

        //新增正式驾照
        [System.Web.Mvc.HttpPost]
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
        public BaseResponseDTO initCarNo([FromBody]JObject data)
        {
            BaseResponseDTO ret = new BaseResponseDTO();
           var type= data.ToObject<DriverLicenseDTO>();
            ret.message=IDUtils.generateId(int.Parse(type.id_no));
            return ret;
        }

        //新增正式驾照
        [System.Web.Mvc.HttpPost]
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

        //增加正式行驶证
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddDrivingPermit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();

            try
            {

                var dtoData = data.ToObject<CarLicenseDTO>();

                dtoData.userInfo = operInfo;

                var service = Instance<IDriverLicenseService>.Create;

                bool res = service.AddDrivingPermit(dtoData);

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

        /// <summary>
        /// 临时行驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]

        public BaseResponseDTO queryTemporaryDrivingLicense([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<TemporaryDriverLicenseDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var dtoData = data.ToObject<TemporaryDriverLicenseDTO>();
                string err = string.Empty;
                var obj = Instance<IDriverLicenseService>.Create;
                List<TemporaryDriverLicenseDTO> lst = obj.queryTemporaryDrivingLicense(pageindex, pagesize, dtoData);
                // 读取图片base64内容
                lst.ForEach(p =>
                {
                    p.user_photo_base64 = FileUtils.fileToBase64(p.user_photo_path);
                });
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
                var obj = Instance<IDriverLicenseService>.Create;
                List<TemporaryDrivingPermitDTO> lst = obj.queryTemporaryDrivingByPage(pageindex, pagesize, dtoData);
                lst.ForEach(p =>
                {
                    p.car_1_value = p.car_1_img_path==null?"":FileUtils.fileToBase64(p.car_1_img_path);
                    p.car_2_value = p.car_2_img_path == null ? "" : FileUtils.fileToBase64(p.car_2_img_path);
                    p.vin_no_value = p.vin_no_img_path == null ? "" : FileUtils.fileToBase64(p.vin_no_img_path);
                    p.engine_no_value = p.engine_no_img_path == null ? "" : FileUtils.fileToBase64(p.engine_no_img_path);
                    p.user_photo_base64 = p.user_photo_path == null ? "" : FileUtils.fileToBase64(p.user_photo_path);
                });
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
       public BaseResponseDTO validataTemp([FromBody]JObject data)
       {

           var dataDTO=data.ToObject<TemporaryDrivingPermitDTO>();

           var obj = Instance<IDriverLicenseService>.Create;

           BaseResponseDTO message = obj.validataTemp(dataDTO.temp_number, dataDTO.engine_no, 
               dataDTO.vin,dataDTO.id_card);

            return message;

       }

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO validata([FromBody]JObject data)
        {
            var dataDTO=data.ToObject<CarLicenseDTO>();

           var obj = Instance<IDriverLicenseService>.Create;

           BaseResponseDTO message = obj.validata(dataDTO.car_number, dataDTO.motor_no, dataDTO.carframe_no);

            return message;
        }


        

          [System.Web.Mvc.HttpPost]
        public BaseResponseDTO validataByDriverLicense([FromBody]JObject data)
        {
            var dataDTO = data.ToObject<DriverLicenseDTO>();

           var obj = Instance<IDriverLicenseService>.Create;

           BaseResponseDTO message = obj.validataByDriverLicense(dataDTO.id_card);

            return message;
        }

       [System.Web.Mvc.HttpPost]
          public BaseResponseDTO initModifyUser()
          {
              BaseResponseDTO message = new BaseResponseDTO();
              message.message = operInfo.user_name;
              return message;
          }

        #region 打印模板
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddPrintTemplate([FromBody]JObject data)
        {
            BaseResponseDTO resp = new BaseResponseDTO();
            var dataDTO = data.ToObject<PrintTemplateDTO>();
            dataDTO.oper_id = operInfo.user_id;
            var obj = Instance<IDriverLicenseService>.Create;
            var flag = obj.AddPrintTemplate(dataDTO);
            resp.success = flag;
            return resp;
        }
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO RemovePrintTemplate([FromBody]JObject data)
        {
            BaseResponseDTO resp = new BaseResponseDTO();

            var dataDTO = data.ToObject<PrintTemplateDTO>();
            var obj = Instance<IDriverLicenseService>.Create;
            var flag = obj.DeletePrintTemplate(dataDTO);
            resp.success = flag;
            return resp;
        }
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO UpdatePrintTemplate([FromBody]JObject data)
        {
            BaseResponseDTO resp = new BaseResponseDTO();

            var dataDTO = data.ToObject<PrintTemplateDTO>();
            var obj = Instance<IDriverLicenseService>.Create;
            var flag = obj.UpdatePrintTemaplte(dataDTO);
            resp.success = flag;
            return resp;
        }
        [System.Web.Mvc.HttpPost]
        public BaseTResponseDTO<List<PrintTemplateDTO>> LoadTemplate([FromBody]JObject data)
        {
            BaseTResponseDTO<List<PrintTemplateDTO>> resp = new BaseTResponseDTO<List<PrintTemplateDTO>>();
            var dataDTO = data.ToObject<PrintTemplateDTO>();
            var obj = Instance<IDriverLicenseService>.Create;
            var lst = obj.LoadTemplateByOperId(dataDTO.type, operInfo.user_id);
            resp.data.AddRange(lst);
            return resp;
        }

        #endregion
    }

   
   
     

}
