using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using System.IO;

namespace VMS.Api
{
    //事故
    public class TrafficAccidentApiController : BaseApiController
    {

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
                    dto.happen_date = CommonHelper.GetDateTime(httpRequest.Form["happenDate"]);
                    dto.happen_addr = httpRequest.Form["happenAddr"];
                    dto.first_party_man = httpRequest.Form["firstPartyMan"];
                    dto.first_party_car_no = httpRequest.Form["firstPartyCarNo"];
                    dto.second_party_car_no = httpRequest.Form["secondPartyCarNo"];
                    dto.first_party_addr = httpRequest.Form["firstPartyAddr"];
                    dto.second_party_man = httpRequest.Form["secondPartyMan"];
                    dto.second_party_addr = httpRequest.Form["secondPartyAddr"];
                    dto.accident_desc = httpRequest.Form["accidentDesc"];
                    dto.mediation_unit = httpRequest.Form["mediationUnit"];
                    dto.mediation_date = CommonHelper.GetDateTime(httpRequest.Form["mediationDate"]);
                    dto.draw_recorder = httpRequest.Form["drawRecorder"];
                    dto.accident_mediator = httpRequest.Form["accidentMediator"];
                    dto.bingPartyAddr = httpRequest.Form["bingPartyAddr"];
                    dto.bingPartyMan = httpRequest.Form["bingPartyMan"];
                    dto.dingPartyAddr = httpRequest.Form["dingPartyAddr"];
                    dto.dingPartyMan = httpRequest.Form["dingPartyMan"];
                    dto.duty = httpRequest.Form["duty"];

                    dto.oper_id = operInfo.user_id;
                    dto.img_url  = imgs.ToString();

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
                    happen_date = e.happen_date,
                    happen_addr = e.happen_addr,
                    first_party_man = e.first_party_man,
                    first_party_addr = e.first_party_addr,
                    second_party_man = e.second_party_man,
                    second_party_addr = e.second_party_addr,
                    accident_desc = e.accident_desc,
                    mediation_unit = e.mediation_unit,
                    mediation_date = e.mediation_date,
                    draw_recorder = e.draw_recorder,
                    accident_mediator = e.accident_mediator,
                    img_url = e.img_url,
                    oper_id = e.oper_id,
                    oper_date = e.oper_date,
                    modify_oper_id = e.modify_oper_id,
                    modify_date = e.modify_date,
                    bingPartyAddr=e.bingPartyAddr,
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyAccident([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modify_oper_id = operInfo.user_id;
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modify_oper_id = operInfo.user_id;
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
                    dto.modify_oper_id = operInfo.user_id;
                    dto.img_url = imgs.ToString();

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