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

namespace VMS.ESIApi
{
    /// <summary>
    /// 中队管理
    /// </summary>
    public class SquadronController: BaseApiController
    {
        /// <summary>
        /// 中队工作记录
        /// </summary>
        ///  <param name="page">第几页</param>
        ///  <param name="rows">页大小</param>
        ///  <param name="startTime">工作时间</param>
        ///  <param name="endTime">工作时间</param>
        /// <returns></returns>
        public GridResponseDTO<TrailerDTO> QueryTrailer([FromBody]TrailerQueryDTO trailerQueryDTO)
        {
            var ret = new GridResponseDTO<TrailerDTO>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageIndex = CommonHelper.GetInt(httpRequest.Form["page"], 1);
                var pageSize = CommonHelper.GetInt(httpRequest.Form["rows"], 10);

                if (httpRequest["trailerNo"] != null && !string.IsNullOrEmpty(httpRequest["trailerNo"]))
                {
                    sb.Append(" and trailer_no = @trailerNo");
                    paramlst.Add(new SqlParam("@trailerNo", Decimal.Parse(httpRequest["trailerNo"])));
                }
                if (trailerQueryDTO != null && !string.IsNullOrEmpty(trailerQueryDTO.startTime))
                {
                    sb.Append(" and trailer_date > @startTime");
                    paramlst.Add(new SqlParam("@startTime", trailerQueryDTO.startTime));
                }
                if (trailerQueryDTO != null && !string.IsNullOrEmpty(trailerQueryDTO.endTime))
                {
                    sb.Append(" and trailer_date < @endTime");
                    paramlst.Add(new SqlParam("@endTime", trailerQueryDTO.endTime));
                }

                string err = string.Empty;
                var obj = Instance<ITrailerService>.Create;
                var lst = obj.QueryTrailer(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new TrailerDTO()
                {
                    trailerNo = e.trailer_no,
                    trailerDate = e.trailer_date,
                    number = e.trailer_number,
                    totalMotorcycle = e.trailer_totalmotorcycle,
                    tricycle = e.trailer_tricycle,
                    batteryMotorcycle = e.trailer_batterymotorcycle,
                    totalVehicle = e.trailer_totalvehicle,
                    bigCar = e.trailer_bigcar,
                    smallCar = e.trailer_smallcar,
                    tractor = e.trailer_tractor,
                    totalTrailer = e.trailer_totaltrailer,
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
        /// 添加中队工作记录
        /// </summary>
        /// <param name="trailerDate">时间</param>
        /// <param name="number">人数</param>
        /// <param name="totalMotorcycle">摩托总数</param>
        /// <param name="tricycle">三轮摩托</param>
        /// <param name="batteryMotorcycle">电动摩托</param>
        /// <param name="totalVehicle">车辆总数</param>
        /// <param name="bigCar">大车</param>
        /// <param name="smallCar">小车</param>
        /// <param name="tractor">拖拉机</param>
        /// <param name="totalTrailer">拖车总数</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddTrailer([FromBody]TrailerDTO data)
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

                data.createId = "";
                var service = Instance<ITrailerService>.Create;
                bool res = service.AddTrailer(data);

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
        /// 修改中队工作记录
        /// </summary>
        /// <param name="data"></param>
        /// <param name="trailerNo"></param>
        /// <param name="number">人数</param>
        /// <param name="totalMotorcycle">摩托总数</param>
        /// <param name="tricycle">三轮摩托</param>
        /// <param name="batteryMotorcycle">电动摩托</param>
        /// <param name="totalVehicle">车辆总数</param>
        /// <param name="bigCar">大车</param>
        /// <param name="smallCar">小车</param>
        /// <param name="tractor">拖拉机</param>
        /// <param name="totalTrailer">拖车总数</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO upDate([FromBody]TrailerDTO data)
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


                var service = Instance<ITrailerService>.Create;
                ret.success = service.UpdateTrailer(data);
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
        /// 删除中队工作记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public BaseResponseDTO DeleteTrailer(string id)
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
                var service = Instance<ITrailerService>.Create;
                bool res = service.DeleteTrailer(id);

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

        /// <summary>
        /// 根据id查询单个中队工作记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseReportDTO QueryInfo([FromBody]TrailerQueryDTO trailerQueryDTO)
        {
            var ret = new BaseReportDTO();
            try
            {
                var service = Instance<ITrailerService>.Create;
                List<TrailerDTO> trailerDTO = new List<TrailerDTO>();
                trailerDTO = service.QueryInfo(trailerQueryDTO);
                if (trailerDTO == null || trailerDTO.Count == 0) { return ret; }
                ret.rows = trailerDTO;
                return ret;
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