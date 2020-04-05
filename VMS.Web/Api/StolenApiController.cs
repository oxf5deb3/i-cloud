using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.DTO.Stolen;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class StolenApiController : BaseApiController
    {
        /// <summary>
        /// 查询拖车记录
        /// </summary>
        /// <returns></returns>
        public GridResponseDTO<StolenDTO> QueryStolen()
        {
            var ret = new GridResponseDTO<StolenDTO>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageIndex = CommonHelper.GetInt(httpRequest.Form["page"], 1);
                var pageSize = CommonHelper.GetInt(httpRequest.Form["rows"], 10);

                string err = string.Empty;
                var obj = Instance<IStolenService>.Create;
                var lst = obj.QueryStolen(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new StolenDTO()
                {
                    stolenId = e.stolen_id,
                    acceptingCases = e.accepting_cases,
                    totalStolenCarBig = e.total_stolencar_big,
                    totalStolenCarSmall = e.total_stolencar_small,
                    totalStolenCarMotorcycle = e.total_stolencar_motorcycle,
                    recoverBigCar = e.recover_bigcar,
                    recoverSmallCar = e.recover_smallcar,
                    recoverMotorcyle = e.recover_motorcycle,
                    undetected = e.undetected,
                    remark = e.remark,
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
        public BaseResponseDTO AddStolen([FromBody]StolenDTO data)
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
                var service = Instance<IStolenService>.Create;
                bool res = service.AddStolen(data);

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
        public BaseResponseDTO upDate([FromBody]StolenDTO data)
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


                var service = Instance<IStolenService>.Create;
                ret.success = service.UpdateStolen(data);
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
        public BaseResponseDTO DeleteStolen(string id)
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
                var service = Instance<IStolenService>.Create;
                bool res = service.DeleteStolen(id);

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