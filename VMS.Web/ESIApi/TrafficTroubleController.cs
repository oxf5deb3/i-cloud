using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.DTO.TrafficAccident;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.ESIApi
{
    /// <summary>
    /// 交通事故管理
    /// </summary>
    public class TrafficTroubleController: BaseApiController
    {
        /// <summary>
        /// 添加事故信息 表单
        /// </summary>
        /// <param name="happenDate">事故时间</param>
        /// <param name="happenAddr">事故地点</param>
        /// <param name="firstPartyMan">甲方当事人</param>
        /// <param name="firstPartyAddr">甲方地址</param>
        /// <param name="firstPartyCarNo">甲方车牌号</param>
        /// <param name="secondPartyCarNo">乙方车牌号</param>
        /// <param name="secondPartyMan">乙方当事人</param>
        /// <param name="accidentDesc">事故经过</param>
        /// <param name="mediationUnit">调解单位</param>
        /// <param name="mediationDate">调解日期</param>
        /// <param name="drawRecorder">绘图记录</param>
        /// <param name="accidentMediator">调解员</param>
        /// <param name="bingPartyAddr">丙方地址</param>
        /// <param name="bingPartyMan">丙方当事人</param>
        /// <param name="dingPartyAddr">丁方地址</param>
        /// <param name="dingPartyMan">丁方当事人</param>
        /// <param name="duty">责任认定</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddAccident()
        {
            var ret = new BaseResponseDTO();

            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpFileCollection filelist = httpRequest.Files;
                if (filelist != null && filelist.Count > 0)
                {
                    Random random = new Random();
                    StringBuilder imgs = new StringBuilder();
                    HttpPostedFile tempFile = null;
                    for (int i = 0; i < filelist.Count; i++)
                    {
                        tempFile = filelist[i];
                        if (tempFile.ContentLength > 0)
                        {
                            string fileName = System.IO.Path.GetFileName(tempFile.FileName); //获取到名称
                            string fileExtension = System.IO.Path.GetExtension(fileName);// 扩展名
                            string imgName = DateTime.Now.ToFileTimeUtc().ToString() + random.Next(1000, 10000) + fileExtension;
                            tempFile.SaveAs(StringContants.IMG_TRAFFIC_ACCIDENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }

                    var dto = new TrafficAccidentDTO();
                    dto.happenDate = httpRequest.Form["happenDate"];
                    dto.happenAddr = httpRequest.Form["happenAddr"];
                    dto.firstPartyMan = httpRequest.Form["firstPartyMan"];
                    dto.firstPartyAddr = httpRequest.Form["firstPartyAddr"];
                    dto.secondPartyMan = httpRequest.Form["secondPartyMan"];
                    dto.secondPartyAddr = httpRequest.Form["secondPartyAddr"];
                    dto.accidentDesc = httpRequest.Form["accidentDesc"];
                    dto.mediationUnit = httpRequest.Form["mediationUnit"];
                    dto.mediationDate = httpRequest.Form["mediationDate"];
                    dto.drawRecorder = httpRequest.Form["drawRecorder"];
                    dto.accidentMediator = httpRequest.Form["accidentMediator"];
                    dto.bingPartyAddr = httpRequest.Form["bingPartyAddr"];
                    dto.bingPartyMan = httpRequest.Form["bingPartyMan"];
                    dto.dingPartyAddr = httpRequest.Form["dingPartyAddr"];
                    dto.dingPartyMan = httpRequest.Form["dingPartyMan"];
                    dto.duty = httpRequest.Form["duty"];

                    dto.operId = "";
                    dto.imgUrl = imgs.ToString();

                    var service = Instance<ITrafficAccidentService>.Create;

                    bool res = service.AddTrafficAccident(dto);

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
                else
                {
                    ret.message = "缺少图片";
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
        /// 查询事故信息(分页)
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="rows">页大小</param>
        /// <param name="id">string</param>
        /// <param name="happenAddr">事故地点</param>
        /// <param name="timeBegin">string</param>
        /// <param name="timeEnd">string</param>
        /// <param name="firstPartyMan">甲方当事人</param>
        /// <param name="secondPartyMan">乙方当事人</param>
        /// <param name="accidentMediator">调解员</param>
        /// <param name="mediationUnit">调解单位</param>
        /// <returns></returns>
        public GridResponseDTO<TrafficAccidentDTO> ListAccident()
        {
            var ret = new GridResponseDTO<TrafficAccidentDTO>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageIndex = CommonHelper.GetInt(httpRequest.Form["page"], 1);
                var pageSize = CommonHelper.GetInt(httpRequest.Form["rows"], 10);

                if (httpRequest["id"] != null && !string.IsNullOrEmpty(httpRequest["id"]))
                {
                    sb.Append(" and id = @id");
                    paramlst.Add(new SqlParam("@id", Decimal.Parse(httpRequest["id"])));
                }

                if (httpRequest["happenAddr"] != null && !string.IsNullOrEmpty(httpRequest["happenAddr"]))
                {
                    sb.Append(" and happen_addr like @address");
                    paramlst.Add(new SqlParam("@address", "%" + httpRequest["happenAddr"] + "%"));
                }

                if (httpRequest["timeBegin"] != null && !string.IsNullOrEmpty(httpRequest["timeBegin"]))
                {
                    sb.Append(" and happen_date >  @timeBegin");
                    paramlst.Add(new SqlParam("@timeBegin", httpRequest["timeBegin"]));
                }

                if (httpRequest["timeEnd"] != null && !string.IsNullOrEmpty(httpRequest["timeEnd"]))
                {
                    sb.Append(" and happen_date < @timeEnd");
                    paramlst.Add(new SqlParam("@timeEnd", httpRequest["timeEnd"]));
                }

                if (httpRequest["firstPartyMan"] != null && !string.IsNullOrEmpty(httpRequest["firstPartyMan"]))
                {
                    sb.Append(" and first_party_man = @firstPartyMan");
                    paramlst.Add(new SqlParam("@firstPartyMan", httpRequest["firstPartyMan"]));
                }

                if (httpRequest["secondPartyMan"] != null && !string.IsNullOrEmpty(httpRequest["secondPartyMan"]))
                {
                    sb.Append(" and second_party_man = @secondPartyMan");
                    paramlst.Add(new SqlParam("@secondPartyMan", httpRequest["secondPartyMan"]));
                }

                if (httpRequest["accidentMediator"] != null && !string.IsNullOrEmpty(httpRequest["accidentMediator"]))
                {
                    sb.Append(" and accident_mediator = @accidentMediator");
                    paramlst.Add(new SqlParam("@accidentMediator", httpRequest["accidentMediator"]));
                }

                if (httpRequest["mediationUnit"] != null && !string.IsNullOrEmpty(httpRequest["mediationUnit"]))
                {
                    sb.Append(" and mediation_unit = @mediationUnit");
                    paramlst.Add(new SqlParam("@mediationUnit", "%" + httpRequest["mediationUnit"] + "%"));
                }

                string err = string.Empty;
                var obj = Instance<ITrafficAccidentService>.Create;
                var lst = obj.ListAccident(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new TrafficAccidentDTO()
                {
                    id = e.id,
                    happenDate = e.happen_date.ToString(),
                    happenAddr = e.happen_addr,
                    firstPartyMan = e.first_party_man,
                    firstPartyAddr = e.first_party_addr,
                    secondPartyMan = e.second_party_man,
                    secondPartyAddr = e.second_party_addr,
                    accidentDesc = e.accident_desc,
                    mediationUnit = e.mediation_unit,
                    mediationDate = e.mediation_date.ToString(),
                    drawRecorder = e.draw_recorder,
                    accidentMediator = e.accident_mediator,
                    imgUrl = e.img_url,
                    operId = e.oper_id,
                    operDate = e.oper_date.ToString(),
                    modifyOperId = e.modify_oper_id,
                    modifyDate = e.modify_date.ToString(),
                    bingPartyAddr = e.bingPartyAddr,
                    bingPartyMan = e.bingPartyMan,
                    dingPartyMan = e.dingPartyMan,
                    dingPartyAddr = e.dingPartyAddr,
                    duty = e.duty
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
        /// 删除事故信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO DelAccident()
        {
            var ret = new BaseResponseDTO();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var ids = CommonHelper.GetString(httpRequest.Form["ids"]);
                var service = Instance<ITrafficAccidentService>.Create;
                bool res = service.DelAccident(ids);

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
        /// 修改事故信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="happenDate">事故时间</param>
        /// <param name="happenAddr">事故地点</param>
        /// <param name="firstPartyMan">甲方当事人</param>
        /// <param name="firstPartyAddr">甲方地址</param>
        /// <param name="secondPartyMan">乙方当事人</param>
        /// <param name="accidentDesc">事故经过</param>
        /// <param name="mediationUnit">调解单位</param>
        /// <param name="mediationDate">调解日期</param>
        /// <param name="drawRecorder">绘图记录</param>
        /// <param name="accidentMediator">调解员</param>
        /// <param name="bingPartyAddr">丙方地址</param>
        /// <param name="bingPartyMan">丙方当事人</param>
        /// <param name="dingPartyAddr">丁方地址</param>
        /// <param name="dingPartyMan">丁方当事人</param>
        /// <param name="duty">责任认定</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyAccident([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modifyOperId = "";
                bool res = service.ModifyAccident(dto);

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

        public void getReadImg()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var httpResponse = HttpContext.Current.Response;
                var imgName = httpRequest.QueryString["imgName"];
                var arr = Regex.Split(imgName, ".", RegexOptions.IgnoreCase);
                httpResponse.ContentType = "png".Equals(arr[1], StringComparison.OrdinalIgnoreCase) ? "image/png" : "image/jpeg";
                System.IO.FileStream fs = File.Open(StringContants.IMG_TRAFFIC_ACCIDENT_PATH + imgName, FileMode.Open, FileAccess.Read, FileShare.Read);
                const int byteLength = 8192;
                byte[] bytes = new byte[byteLength];
                while (fs.Read(bytes, 0, byteLength) != 0)
                {
                    httpResponse.BinaryWrite(bytes);
                }
                fs.Close();
                httpResponse.Flush();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modifyOperId = "";
                bool res = service.ModifyImgs(dto);

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
        /// <summary>
        /// 附加图片 表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AppendAccidentImg()
        {
            var ret = new BaseResponseDTO();

            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpFileCollection filelist = httpRequest.Files;
                if (filelist != null && filelist.Count > 0)
                {
                    Random random = new Random();
                    StringBuilder imgs = new StringBuilder();
                    HttpPostedFile tempFile = null;
                    for (int i = 0; i < filelist.Count; i++)
                    {
                        tempFile = filelist[i];
                        if (tempFile.ContentLength > 0)
                        {
                            string fileName = System.IO.Path.GetFileName(tempFile.FileName); //获取到名称
                            string fileExtension = System.IO.Path.GetExtension(fileName);// 扩展名
                            string imgName = DateTime.Now.ToFileTimeUtc().ToString() + random.Next(1000, 10000) + fileExtension;
                            tempFile.SaveAs(StringContants.IMG_TRAFFIC_ACCIDENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new TrafficAccidentDTO();
                    dto.id = Decimal.Parse(httpRequest.Form["id"]);
                    dto.modifyOperId = "";
                    dto.imgUrl = imgs.ToString();

                    var service = Instance<ITrafficAccidentService>.Create;

                    bool res = service.AppendImgs(dto);

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
                else
                {
                    ret.message = "缺少图片";
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