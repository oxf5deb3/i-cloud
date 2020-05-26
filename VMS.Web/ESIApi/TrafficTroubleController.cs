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
using VMS.ESIApi.Models;
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
        /// <param name="secondPartyAddr">乙方当事人地址</param>
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
        /// <param name="img_url">图片</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> AddAccident([FromBody]JObject data)
        {
            var ret = new Response<string>();

            try
            {
                var httpRequest = HttpContext.Current.Request;
                var host = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
                var dto = data.ToObject<ESITrafficAccidentDTO>();
                if (!string.IsNullOrEmpty(dto.imgUrl))
                {
                    TrafficAccidentDTO data1 = new TrafficAccidentDTO()
                    {
                        happen_addr = dto.happenAddr,
                        happen_date =CommonHelper.GetDateTime(dto.happenDate),
                        first_party_man = dto.firstPartyMan,
                        first_party_addr = dto.firstPartyAddr,
                        first_party_car_no = dto.firstPartyCarNo,
                        second_party_man = dto.secondPartyMan,
                        second_party_addr = dto.secondPartyAddr,
                        second_party_car_no = dto.secondPartyCarNo,
                        accident_desc = dto.accidentDesc,
                        mediation_unit = dto.mediationUnit,
                        mediation_date = CommonHelper.GetDateTime(dto.happenAddr),
                        draw_recorder = dto.drawRecorder,
                        accident_mediator = dto.accidentMediator,
                        oper_id = "1001",
                        oper_date = DateTime.Now,
                        img_url = dto.imgUrl.Replace(host,""),
                        duty = dto.duty,
                        dingPartyAddr = dto.dingPartyAddr,
                        dingPartyMan = dto.dingPartyMan,
                        bingPartyAddr = dto.bingPartyAddr,
                        bingPartyMan = dto.bingPartyMan
                    };
                    var service = Instance<ITrafficAccidentService>.Create;
                    bool res = service.AddTrafficAccident(data1);
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
        /// <param name="sort">排序字段</param>
        /// <param name="order">asc/desc</param>
        /// <param name="happenAddr">事故地点</param>
        /// <param name="timeBegin">string</param>
        /// <param name="timeEnd">string</param>
        /// <param name="firstPartyMan">甲方当事人</param>
        /// <param name="secondPartyMan">乙方当事人</param>
        /// <param name="accidentMediator">调解员</param>
        /// <param name="mediationUnit">调解单位</param>
        /// <returns></returns>
        public Response<List<ESITrafficAccidentDTO>> ListAccident([FromBody]JObject data)
        {
            var ret = new Response<List<ESITrafficAccidentDTO>>();
            try
            {
                var condition = data.ToDictionary();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var pageindex = CommonHelper.GetInt(condition["page"], 0);
                var pagesize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                var host = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
                var obj = Instance<ITrafficAccidentService>.Create;
                var err = "";
                List<TrafficAccidentDTO> lst = obj.QueryPage(condition, sort, order, pageindex, pagesize, ref total, ref err);
                ret.data=lst.Select(e => new ESITrafficAccidentDTO()
                {
                    id = e.id,
                    happenDate = CommonHelper.GetFormatDateTime(e.happen_date,"yyyy-MM-dd HH:mm:ss"),
                    happenAddr = e.happen_addr,
                    firstPartyMan = e.first_party_man,
                    firstPartyAddr = e.first_party_addr,
                    firstPartyCarNo = e.first_party_car_no,
                    secondPartyCarNo = e.second_party_car_no,
                    secondPartyMan = e.second_party_man,
                    secondPartyAddr = e.second_party_addr,
                    accidentDesc = e.accident_desc,
                    mediationUnit = e.mediation_unit,
                    mediationDate = CommonHelper.GetFormatDateTime(e.mediation_date, "yyyy-MM-dd HH:mm:ss"),
                    drawRecorder = e.draw_recorder,
                    accidentMediator = e.accident_mediator,
                    bingPartyAddr = e.bingPartyAddr,
                    bingPartyMan = e.bingPartyMan,
                    dingPartyAddr = e.dingPartyAddr,
                    dingPartyMan = e.dingPartyMan,
                    operId= e.oper_id,
                    operDate = CommonHelper.GetFormatDateTime(e.oper_date, "yyyy-MM-dd HH:mm:ss"),
                    oper_name =e.oper_name,
                    modifyOperId=e.modify_oper_id,
                    modifyDate= CommonHelper.GetFormatDateTime(e.modify_date,"yyyy-MM-dd HH:mm:ss"),
                    modify_oper_name =e.modify_oper_name,
                    duty = e.duty,
                    imgUrl = host+e.img_url
                }).ToList();
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
        public Response<string> DelAccident()
        {
            var ret = new Response<string>();
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
        public Response<string> ModifyAccident([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modify_oper_id = "";
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
        public Response<string> ModifyImgs([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<TrafficAccidentDTO>();
                var service = Instance<ITrafficAccidentService>.Create;
                dto.modify_oper_id = "";
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
        public Response<string> AppendAccidentImg()
        {
            var ret = new Response<string>();

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
                    dto.modify_oper_id = "";
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