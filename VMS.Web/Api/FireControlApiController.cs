using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.DTO.FireControl;
using Newtonsoft.Json.Linq;
using VMS.IServices;
using VMS.ServiceProvider;
using System.Text;
using VMS.Model;
using VMS.Utils;
using System.Text.RegularExpressions;
using System.IO;

namespace VMS.Api
{
    //消防接口
    public class FireControlApiController : BaseApiController
    {

        public const string IMG_ACCIDENT_PATH = "D:\\VMS\\image\\accident-imgs\\";
        //public const string IMG_EQUIPMENT_PATH = "D:\\VMS\\image\\equipment-imgs\\";

        //public const string IMG_ACCIDENT_PATH = "C:\\存储\\资料\\";
        public const string IMG_EQUIPMENT_PATH = "C:\\存储\\资料\\";

        public GridResponseDTO<FireAccidentDTO> ListAccident()
        {
            var ret = new GridResponseDTO<FireAccidentDTO>();
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

                if (httpRequest["address"] != null && !string.IsNullOrEmpty(httpRequest["address"]))
                {
                    sb.Append(" and happen_addr like @address");
                    paramlst.Add(new SqlParam("@address", "%" + httpRequest["address"] + "%"));
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
                if (httpRequest["cars"] != null && !string.IsNullOrEmpty(httpRequest["cars"]))
                {
                    sb.Append(" and out_police_cars like @cars");
                    paramlst.Add(new SqlParam("@cars", "%" + httpRequest["cars"] + "%"));
                }
                if (httpRequest["men"] != null && !string.IsNullOrEmpty(httpRequest["men"]))
                {
                    sb.Append(" and out_police_mans like @men");
                    paramlst.Add(new SqlParam("@men", "%" + httpRequest["men"] + "%"));
                }
                if (httpRequest["name"] != null && !string.IsNullOrEmpty(httpRequest["name"]))
                {
                    sb.Append(" and name = @name");
                    paramlst.Add(new SqlParam("@name", httpRequest["cars"]));
                }
                if (httpRequest["phone"] != null && !string.IsNullOrEmpty(httpRequest["phone"]))
                {
                    sb.Append(" and phone = @phone");
                    paramlst.Add(new SqlParam("@phone", httpRequest["phone"]));
                }
                string err = string.Empty;
                var obj = Instance<IFireControlService>.Create;
                var lst = obj.ListAccident(sb, paramlst, pageIndex, pageSize, ref total);
                ret.total = total;
                ret.rows.AddRange(lst.Select(e => new FireAccidentDTO()
                {
                    id = e.id,
                    address = e.happen_addr,
                    datetime = e.happen_date.ToString(),
                    desc = e.accident_desc,
                    cars = e.out_police_cars,
                    names = e.out_police_mans,
                    result = e.process_results,
                    imgs = e.img_url,
                    operId = e.oper_id,
                    operDate = e.oper_date.ToString(),
                    modifyOperId = e.modify_oper_id,
                    modifyDate = e.modify_date.ToString(),
                    name = e.name,
                    sex = e.sex,
                    age = e.age,
                    addr = e.addr,
                    folk = e.folk,
                    loss = e.loss,
                    casualties = e.casualties,
                    finance_loss = e.finance_loss,
                    phone = e.phone

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
                            tempFile.SaveAs(IMG_EQUIPMENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireEquipmentDTO();
                    dto.id = Decimal.Parse(httpRequest.Form["id"]);
                    dto.modifyOperId = operInfo.user_id;
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
                            tempFile.SaveAs(IMG_ACCIDENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireAccidentDTO();
                    dto.id = Decimal.Parse(httpRequest.Form["id"]);
                    dto.modifyOperId = operInfo.user_id;
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO addFireAccident()
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
                            tempFile.SaveAs(IMG_ACCIDENT_PATH + imgName);
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
                    dto.operId = operInfo.user_id;
                    dto.imgs = imgs.ToString();
                    dto.name = httpRequest.Form["name"];
                    dto.sex = httpRequest.Form["sex"];
                    dto.age = httpRequest.Form["datetime"];
                    dto.folk = httpRequest.Form["folk"];
                    dto.addr = httpRequest.Form["addr"];
                    dto.phone = httpRequest.Form["datetime"];
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyEquipmentImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<FireEquipmentDTO>();
                var service = Instance<IFireControlService>.Create;
                dto.modifyOperId = operInfo.user_id;
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
        public BaseResponseDTO ModifyImgs([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<FireAccidentDTO>();
                var service = Instance<IFireControlService>.Create;
                dto.modifyOperId = operInfo.user_id;
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
                dto.modifyOperId = operInfo.user_id;
                var service = Instance<IFireControlService>.Create;
                dto.modifyOperId = operInfo.user_id;
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO ModifyAccident([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var dto = data.ToObject<FireAccidentDTO>();
                var service = Instance<IFireControlService>.Create;
                dto.modifyOperId = operInfo.user_id;
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
                    path = IMG_ACCIDENT_PATH;
                }
                else
                {
                    path = IMG_EQUIPMENT_PATH;
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

        [System.Web.Mvc.HttpPost]
        public BaseResponseDTO addFireEquipment()
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
                            tempFile.SaveAs(IMG_EQUIPMENT_PATH + imgName);
                            imgs.Append(imgName).Append(",");
                        }
                    }
                    var dto = new FireEquipmentDTO();
                    dto.datetime = httpRequest.Form["datetime"];
                    dto.address = httpRequest.Form["address"];
                    dto.desc = httpRequest.Form["desc"];
                    dto.name = httpRequest.Form["name"];
                    dto.liable = httpRequest.Form["liable"];
                    dto.operId = operInfo.user_id;
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