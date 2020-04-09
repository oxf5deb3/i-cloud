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
        /// <returns></returns>
        [HttpPost]
        public GridResponseDTO<FireEquipmentDTO> ListEquipment()
        {
            var ret = new GridResponseDTO<FireEquipmentDTO>();
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

                if (httpRequest["name"] != null && !string.IsNullOrEmpty(httpRequest["name"]))
                {
                    sb.Append(" and eq_name like @eq_name");
                    paramlst.Add(new SqlParam("@eq_name", "%" + httpRequest["name"] + "%"));
                }
                if (httpRequest["address"] != null && !string.IsNullOrEmpty(httpRequest["address"]))
                {
                    sb.Append(" and install_addr like @install_addr");
                    paramlst.Add(new SqlParam("@install_addr", "%" + httpRequest["address"] + "%"));
                }
                if (httpRequest["timeBegin"] != null && !string.IsNullOrEmpty(httpRequest["timeBegin"]))
                {
                    sb.Append(" and install_date >  @timeBegin");
                    paramlst.Add(new SqlParam("@timeBegin", httpRequest["timeBegin"]));
                }
                if (httpRequest["timeEnd"] != null && !string.IsNullOrEmpty(httpRequest["timeEnd"]))
                {
                    sb.Append(" and install_date < @timeEnd");
                    paramlst.Add(new SqlParam("@timeEnd", httpRequest["timeEnd"]));
                }
                if (httpRequest["liable"] != null && !string.IsNullOrEmpty(httpRequest["liable"]))
                {
                    sb.Append(" and person_liable like @person_liable");
                    paramlst.Add(new SqlParam("@person_liable", "%" + httpRequest["liable"] + "%"));
                }

                string err = string.Empty;
                var obj = Instance<IFireControlService>.Create;
                var lst = obj.ListEquipment(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new FireEquipmentDTO()
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
        /// 附加图片
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AppendEquipmentImg()
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
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddFireAccident()
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
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO DelEquipment()
        {
            var ret = new BaseResponseDTO();
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
        /// 删除消防事故
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO DelAccident()
        {
            var ret = new BaseResponseDTO();
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
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyEquipmentImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
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
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
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
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyEquipment()
        {
            var ret = new BaseResponseDTO();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                var dto = new FireEquipmentDTO();
                dto.id = Decimal.Parse(httpRequest.Form["id"]);
                dto.datetime = httpRequest.Form["datetime"];
                dto.address = httpRequest.Form["address"];
                dto.desc = httpRequest.Form["desc"];
                dto.name = httpRequest.Form["name"];
                dto.liable = httpRequest.Form["liable"];
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
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyAccident([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
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
        /// 添加消防设备
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO AddFireEquipment()
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