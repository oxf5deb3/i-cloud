using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.DTO.Trailer;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class DetentionApiController : BaseApiController
    {

        /// <summary>
        /// 查询拖车记录
        /// </summary>
        /// <returns></returns>
        public GridResponseDTO<DetentionDTO> QueryDetention()
        {
            var ret = new GridResponseDTO<DetentionDTO>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageIndex = CommonHelper.GetInt(httpRequest.Form["page"], 1);
                var pageSize = CommonHelper.GetInt(httpRequest.Form["rows"], 10);

                string err = string.Empty;
                var obj = Instance<IDetentionService>.Create;
                var lst = obj.QueryDetention(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new DetentionDTO()
                {
                    detentionId = e.detention_id,
                    totalDetention = e.total_detention,
                    totalDetentionBoy = e.total_detention_boy,
                    totalDetentionGirl = e.total_detention_girl,
                    alreadyRelease = e.already_release,
                    nowDetentionTotal = e.now_detention_total,
                    nowDetentionBoy = e.now_detention_boy,
                    nowDetentionGirl = e.now_detention_girl,
                    trafficAccidentDetention = e.traffic_accident_detention,
                    notCooperateDetention = e.not_cooperate_detention,
                    devolvePoliceTotal = e.devolve_police_total,
                    devolvePoliceBoy = e.devolve_police_boy,
                    devolvePoliceGirl = e.devolve_police_girl,
                    devolveProcuratorateTotal = e.devolve_procuratorate_total,
                    devolveProcuratorateBoy=e.devolve_procuratorate_boy,
                    devolveProcuratorateGirl=e.devolve_procuratorate_girl,
                    remark = e.remark
                }));
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
        /// 添加拖车记录
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddDetention([FromBody]DetentionDTO data)
        {
            var ret = new BaseResponseDTO();

            try
            {
                if (data == null)
                {
                    ret.message = "添加失败";
                    ret.success = false;
                    return ret;
                }

                data.createId = operInfo.user_id;
                var service = Instance<IDetentionService>.Create;
                bool res = service.AddDetention(data);

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
        /// 修改拖车记录
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO upDate([FromBody]DetentionDTO data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                    return ret;
                }
                #endregion


                var service = Instance<IDetentionService>.Create;
                ret.success = service.UpdateDetention(data);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        [HttpGet]
        public BaseResponseDTO DeleteDetention(string id)
        {
            var ret = new BaseResponseDTO();
            try
            {
                if (id == null || id.Equals("null"))
                {
                    ret.message = "删除失败";
                    ret.success = false;
                    return ret;
                }
                var service = Instance<IDetentionService>.Create;
                bool res = service.DeleteDetention(id);

                if (res)
                {
                    return ret;
                }
                else
                {
                    ret.message = "删除失败";
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

    }
}