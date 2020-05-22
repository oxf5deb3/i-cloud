using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using VMS.ESIApi.Models;
using VMS.ESIApi.Utils;
using VMS.Utils;

namespace VMS.ESIApi
{
    [ESIAuthCheck]
    [ESIAopFilter]
    [ESIExceptionTrace]
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="type">1:正式驾驶证 2:正式行驶证 3:临时驾驶证 4:临时行驶证 5:消防事故 6:消防设备 7:交通事故</param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> Upload()
        {
            var retDTO = new Response<string>();
            try
            {
                var content = Request.Content;
                var tempUploadFiles = "/Upload/TempUploadFiles/";
                var newFileName = "";
                string filePath = "";
                string extname = "";
                string returnurl = "";
                var sp = new MultipartFormDataMemoryStreamProvider();
                Task<MultipartFormDataMemoryStreamProvider> task = null;
                Task.Factory
                    .StartNew(() =>
                    {
                        task = Request.Content.ReadAsMultipartAsync(sp);
                    },
                    CancellationToken.None,
                    TaskCreationOptions.LongRunning, // guarantees separate thread
                    TaskScheduler.Default)
                    .Wait();
                //var wait = Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp));
                //wait.Wait();
                //var result = wait.Result;
                task.Wait();
                var result = task.Result;
                Log4NetHelper.Info("1");
                var type = result.FormData.GetValues("type").FirstOrDefault();
                Log4NetHelper.Info("type:"+type);
                switch (type)
                {
                    case "1":
                        tempUploadFiles = StringContants.IMG_ZS_LICENSE_PATH;
                        break;
                    case "2":
                        tempUploadFiles = StringContants.IMG_ZS_LICENSE_PATH;
                        break;
                    case "3":
                        tempUploadFiles = StringContants.IMG_LS_LICENSE_PATH;
                        break;
                    case "4":
                        tempUploadFiles = StringContants.IMG_LS_LICENSE_PATH;
                        break;
                    case "5":
                        tempUploadFiles = StringContants.IMG_ACCIDENT_PATH;
                        break;
                    case "6":
                        tempUploadFiles = StringContants.IMG_EQUIPMENT_PATH;
                        break;
                    case "7":
                        tempUploadFiles = StringContants.IMG_TRAFFIC_ACCIDENT_PATH;
                        break;
                }
                Log4NetHelper.Info("tempUploadFiles:" + tempUploadFiles);
                foreach (var item in sp.Contents.Where((c,idx)=>result.IsStream(idx)))
                {
                    if (item.Headers.ContentDisposition.FileName != null)
                    {
                        var filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                        //FileInfo file = new FileInfo(filename);
                        string fileTypes = "gif,jpg,jpeg,png,bmp";
                        var ext = filename.Substring(filename.LastIndexOf('.')+1);
                        if (Array.IndexOf(fileTypes.Split(','), ext.ToLower()) == -1)
                        {
                            throw new ApplicationException("不支持上传文件类型");
                        }
                        //if (file.Length > 5242880)
                        //{
                        //    throw new ApplicationException("文件过大,不能超过5M");
                        //}
                        Log4NetHelper.Info("filename:" + filename);
                        extname = filename.Substring(filename.LastIndexOf('.'), (filename.Length - filename.LastIndexOf('.')));
                        newFileName = Guid.NewGuid().ToString().Substring(0, 6) + extname;


                        string newFilePath = DateTime.Now.ToString("yyyy-MM-dd") + "/";
                        if (!Directory.Exists(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath))
                        {
                            Directory.CreateDirectory(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath);
                        }
                        filePath = Path.Combine(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath, newFileName);
                        returnurl = Path.Combine(tempUploadFiles + newFilePath, newFileName);
                        var ms = item.ReadAsStreamAsync().Result;
                        Log4NetHelper.Info("Result:" + filename);
                        using (var br = new BinaryReader(ms))
                        {
                            //if (ms.Length > 1048576 * 5)
                            //{
                            //    //throw new UserFriendlyException(L("文件过大"));
                            //}
                            var data = br.ReadBytes((int)ms.Length);
                            File.WriteAllBytes(filePath, data);
                        }
                    }
                }

        return new Response<string> { success = true, data = Request.RequestUri.Scheme+"://"+ Request.RequestUri.Authority + returnurl };
            }
            catch (Exception ex)
            {
                Log4NetHelper.Info("上传失败，异常信息:" + ex.StackTrace);
                return new Response<string> { success = false, data = ex.Message, code = ESIApi.StatusCode.FAIL };
            }
        }

    }
}
