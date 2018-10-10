using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

using VMS.DTO;
using VMS.Models;
using VMS.ServiceProvider;
using VMS.IServices;
using System.Text;
using VMS.Model;

namespace VMS.Api
{
    //准驾车型
    public class PermittedCarTypeApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO Add([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<PermittedCarTypeDTO>();
                var info = new t_bd_permitted_car_type(){type_no=dto.type_no,type_name=dto.type_name,memo=dto.memo};
                var service = Instance<IPermittedCarTypeService>.Create;
                var dbInfo = service.FindByTypeNo(info.type_no);
                if (dbInfo != null) {
                    ret.success = false;
                    ret.message = string.Format("已存在此编码[{0}],请重新输入!",info.type_no);
                    return ret;
                }
                ret.success = service.AddPermittedCarType(info);
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
        public BaseResponseDTO Edit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<PermittedCarTypeDTO>();
                var info = new t_bd_permitted_car_type() { type_no = dto.type_no, type_name = dto.type_name, memo = dto.memo };
                var service = Instance<IPermittedCarTypeService>.Create;
                var dbInfo = service.FindByTypeNo(info.type_no);
                if (dbInfo == null)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在此编码[{0}],请刷新重试!", info.type_no);
                    return ret;
                }
                ret.success = service.EditPermittedCarType(info);
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
        public BaseResponseDTO Del([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data["deleted"].ToObject<List<PermittedCarTypeDTO>>();
                var listNos = dto.Select(e=>e.type_no).ToList();
                var service = Instance<IPermittedCarTypeService>.Create;
                ret.success = service.BatchDeletePermittedCarType(listNos);
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
        public GridResponseDTO<PermittedCarTypeDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<PermittedCarTypeDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IPermittedCarTypeService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new PermittedCarTypeDTO() { type_no = e.type_no, type_name = e.type_name, memo = e.memo }));
                ret.total = total;
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
        public BaseTResponseDTO<List<PermittedCarTypeDTO>> QueryAll()
        {
            var ret = new BaseTResponseDTO<List<PermittedCarTypeDTO>>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IPermittedCarTypeService>.Create;
                var lst = obj.GetAllPermittedCarType(sb, paramlst, ref total);
                ret.data.AddRange(lst.Select(e => new PermittedCarTypeDTO() { type_no = e.type_no, type_name = e.type_name }));
                return ret;

            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        /// <summary>
        /// 添加临时驾驶证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddTemporaryDriverLicense([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<TemporaryDriverLicenseDTO>();
                var service = Instance<IDriverLicenseService>.Create;
                //查询是否添加过过临时驾驶证
                var dbInfo = service.QueryTemporaryDrivingLicense(dto.name);
                if (!dbInfo)
                {
                    ret.success = false;
                    ret.message = string.Format("已添加过临时驾驶证!");
                    return ret;
                }
                ret.success = service.AddTemporaryDrivingLicense(dto);
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