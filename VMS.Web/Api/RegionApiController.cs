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
using VMS.Api;
namespace VMS.Areas.BaseData.Controllers
{
    //区域
    public class RegionApiController : BaseApiController
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

                var dto = data.ToObject<RegionDTO>();
                var info = new t_bd_region() { region_no = dto.region_no, region_name = dto.region_name, memo = dto.memo };
                var service = Instance<IRegionService>.Create;
                var dbInfo = service.FindByRegionNo(info.region_no);
                if (dbInfo != null)
                {
                    ret.success = false;
                    ret.message = string.Format("已存在此编码[{0}],请重新输入!", info.region_no);
                    return ret;
                }
                ret.success = service.AddRegion(info);
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

                var dto = data.ToObject<RegionDTO>();
                var info = new t_bd_region() { region_no = dto.region_no, region_name = dto.region_name, memo = dto.memo };
                var service = Instance<IRegionService>.Create;
                var dbInfo = service.FindByRegionNo(info.region_no);
                if (dbInfo == null)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在此编码[{0}],请刷新重试!", info.region_no);
                    return ret;
                }
                ret.success = service.EditRegion(info);
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

                var dto = data["deleted"].ToObject<List<RegionDTO>>();
                var listNos = dto.Select(e => e.region_no).ToList();
                var service = Instance<IRegionService>.Create;
                ret.success = service.BatchDeleteRegion(listNos);
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
        public GridResponseDTO<RegionDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<RegionDTO>();
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
                var obj = Instance<IRegionService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new RegionDTO() { region_no = e.region_no, region_name = e.region_name, memo = e.memo }));
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
        public BaseTResponseDTO<List<RegionDTO>> QueryAll()
        {
            var ret = new BaseTResponseDTO<List<RegionDTO>>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IRegionService>.Create;
                var lst = obj.GetAllRegion(sb, paramlst, ref total);
                ret.data.AddRange(lst.Select(e => new RegionDTO() { region_no=e.region_no,region_name=e.region_name }));
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