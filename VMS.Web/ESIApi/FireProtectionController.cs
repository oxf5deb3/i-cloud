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
using VMS.DTO.FireControl;
using VMS.ESIApi.Models;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.ESIApi
{
    /// <summary>
    /// 消防管理
    /// </summary>
    public class FireProtectionController : BaseApiController
    {
        /// <summary>
        /// 消防列表(分页)
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="rows">页大小</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">asc/desc</param>
        /// <param name="id">查询条件-编号</param>
        /// <param name="name">查询条件-设备信息</param>
        /// <param name="address">查询条件-安装地址</param>
        /// <param name="timeBegin">查询条件-安装时间</param>
        /// <param name="timeEnd">查询条件-安装时间</param>
        /// <param name="liable">查询条件-负责人</param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<FireEquipmentDTO>> ListEquipment([FromBody]JObject data)
        {
            var ret = new Response<List<FireEquipmentDTO>>();
            try
            {
                var condition = data.ToDictionary();
                int total = 0;
                var pageIndex = CommonHelper.GetInt(condition["page"], 0);
                var pageSize = CommonHelper.GetInt(condition["rows"]);
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;

                string err = string.Empty;
                var obj = Instance<IFireControlService>.Create;
                List<t_fire_equipment_register> lst = obj.ListEquipmentNew(condition, sort, order, pageIndex, pageSize, ref total, ref err);
                ret.data=lst.Select(e => new FireEquipmentDTO()
                {
                    id = e.id,
                    name = e.eq_name,
                    address = e.install_addr,
                    datetime = e.install_date.ToString(),
                    desc = e.usage_desc,
                    liable = e.person_liable,
                    imgs = e.img_url,
                    operId = e.oper_id,
                    operDate = e.oper_date.ToString(),
                    modifyOperId = e.modify_oper_id,
                    modifyDate = e.modify_date.ToString()
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
        /// 附加图片
        /// </summary>
        ///<param name="id"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> AppendEquipmentImg()
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
                            tempFile.SaveAs(StringContants.IMG_EQUIPMENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireEquipmentDTO();
                    dto.id = Decimal.Parse(httpRequest.Form["id"]);
                    dto.modifyOperId = "";
                    dto.imgs = imgs.ToString();

                    var service = Instance<IFireControlService>.Create;

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

        /// <summary>
        /// 添加消防事故
        /// </summary>
        /// <param name="datetime">火灾时间</param>
        /// <param name="address">火灾地点</param>
        /// <param name="desc">火灾原因</param>
        /// <param name="cars">出警车辆</param>
        /// <param name="names">出警人员</param>
        /// <param name="result">处理结果</param>
        /// <param name="name">当事人姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="age">年龄</param>
        /// <param name="folk">名族</param>
        /// <param name="addr">地址</param>
        /// <param name="phone">电话</param>
        /// <param name="loss">火灾损失</param>
        /// <param name="finance_loss">财物损失</param>
        /// <param name="casualties">人员伤亡</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> AddFireAccident()
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
                            tempFile.SaveAs(StringContants.IMG_ACCIDENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireAccidentDTO();
                    dto.datetime = httpRequest.Form["datetime"];
                    dto.address = httpRequest.Form["address"];
                    dto.desc = httpRequest.Form["desc"];
                    dto.cars = httpRequest.Form["cars"];
                    dto.names = httpRequest.Form["names"];
                    dto.result = httpRequest.Form["result"];
                    dto.operId = "";
                    dto.imgs = imgs.ToString();
                    dto.name = httpRequest.Form["name"];
                    dto.sex = httpRequest.Form["sex"];
                    dto.age = httpRequest.Form["age"];
                    dto.folk = httpRequest.Form["folk"];
                    dto.addr = httpRequest.Form["addr"];
                    dto.phone = httpRequest.Form["phone"];
                    dto.loss = httpRequest.Form["loss"];
                    dto.finance_loss = httpRequest.Form["finance_loss"];
                    dto.casualties = httpRequest.Form["casualties"];


                    var service = Instance<IFireControlService>.Create;

                    bool res = service.AddFireAccident(dto);

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
        /// 删除消防设备
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> DelEquipment()
        {
            var ret = new Response<string>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var ids = CommonHelper.GetString(httpRequest.Form["ids"]);
                var service = Instance<IFireControlService>.Create;
                bool res = service.DelEquipment(ids);

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
        /// 删除消防事故 表单
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
                var service = Instance<IFireControlService>.Create;
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
        /// 修改消防设备图片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <param name="count"></param>
        /// <param name="address"></param>
        /// <param name="datetime"></param>
        /// <param name="desc"></param>
        /// <param name="liable"></param>
        /// <param name="imgs"></param>
        /// <param name="operId"></param>
        /// <param name="operDate"></param>
        /// <param name="modifyOperId"></param>
        /// <param name="modifyDate"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> ModifyEquipmentImgs([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<FireEquipmentDTO>();
                var service = Instance<IFireControlService>.Create;
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
        /// 修改消防事故图片
        /// </summary>
        /// <param name="imgs">图片</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> ModifyImgs([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<FireAccidentDTO>();
                var service = Instance<IFireControlService>.Create;
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
        /// 修改消防设备
        /// </summary>
        /// <param name="id"></param>
        /// <param name="datetime">安装时间</param>
        /// <param name="address">安装地址</param>
        /// <param name="desc">用途描述</param>
        /// <param name="name">设备信息</param>
        /// <param name="liable">负责人</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> ModifyEquipment([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var condition = data.ToDictionary();

                var dto = new FireEquipmentDTO();
                dto.id = CommonHelper.GetInt(condition["id"], -1);
                dto.datetime = CommonHelper.GetDateTime(condition["datetime"]);
                dto.address = CommonHelper.GetString(condition["address"]);
                dto.desc = CommonHelper.GetString(condition["desc"]);
                dto.name = CommonHelper.GetString(condition["name"]);
                dto.liable = CommonHelper.GetString(condition["liable"]);
                dto.modifyOperId = "";
                var service = Instance<IFireControlService>.Create;
                dto.modifyOperId = "";
                bool res = service.ModifyEquipment(dto);

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
        /// 修改消防事故
        /// </summary>
        ///  <param name="id"></param>
        ///  <param name="address">火灾地点</param>
        ///  <param name="datetime">火灾时间</param>
        ///  <param name="desc">火灾原因</param>
        ///  <param name="cars">出警车辆</param>
        ///  <param name="names">出警人员</param>
        ///  <param name="result">处理结果</param>
        ///  <param name="operId"></param>
        ///  <param name="operDate"></param>
        ///  <param name="modifyOperId"></param>
        ///  <param name="modifyDate"></param>
        ///  <param name="name">当事人姓名</param>
        ///  <param name="sex">性别</param>
        ///  <param name="age">年龄</param>
        ///  <param name="folk">名族</param>
        ///  <param name="addr">地址</param>
        ///  <param name="phone">电话</param>
        ///  <param name="loss">火灾损失</param>
        ///  <param name="finance_loss">财物损失</param>
        ///  <param name="casualties">伤亡</param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> ModifyAccident([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<FireAccidentDTO>();
                var service = Instance<IFireControlService>.Create;
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
                var type = httpRequest.QueryString["type"];
                var imgName = httpRequest.QueryString["imgName"];
                var path = "";
                if ("accident".Equals(type))
                {
                    path = StringContants.IMG_ACCIDENT_PATH;
                }
                else
                {
                    path = StringContants.IMG_EQUIPMENT_PATH;
                }

                var arr = Regex.Split(imgName, ".", RegexOptions.IgnoreCase);
                httpResponse.ContentType = "png".Equals(arr[1], StringComparison.OrdinalIgnoreCase) ? "image/png" : "image/jpeg";
                System.IO.FileStream fs = File.Open(path + imgName, FileMode.Open, FileAccess.Read, FileShare.Read);
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
        /// 添加消防设备 表单提交
        /// </summary>
        /// <param name="datetime">安装时间</param>
        /// <param name="address">安装地址</param>
        /// <param name="desc">用途描述</param>
        /// <param name="name">设备信息</param>
        /// <param name="liable">负责人</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public Response<string> AddFireEquipment()
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
                            tempFile.SaveAs(StringContants.IMG_EQUIPMENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireEquipmentDTO();
                    dto.datetime = httpRequest.Form["datetime"];
                    dto.address = httpRequest.Form["address"];
                    dto.desc = httpRequest.Form["desc"];
                    dto.name = httpRequest.Form["name"];
                    dto.liable = httpRequest.Form["liable"];
                    dto.operId = "";
                    dto.imgs = imgs.ToString();

                    var service = Instance<IFireControlService>.Create;

                    bool res = service.AddFireEquipment(dto);

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