using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;

using VMS.DTO;
using VMS.IServices;
using VMS.ServiceProvider;

namespace VMS.Api
{
    public class DCShowApiController : ApiController
    {
        [HttpGet]
        public BaseTResponseDTO<DCShowDTO> GetDCInfo(string type,string did,string cid)
        {
            var ret = new BaseTResponseDTO<DCShowDTO>();
            try
            {
                var service = Instance<IDCShowService>.Create;
                var dic = new Dictionary<string, dynamic>();
                dic.Add("type", "0");
                dic.Add("did", did);
                dic.Add("cid", cid);
                var errMsg = string.Empty;
                ret.data = service.FindOne(dic, ref errMsg);
                return ret;
            }
            catch (Exception ex)
            {
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }

        [HttpPost]
        public BaseResponseDTO QrCodeMake([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                var service = Instance<IDCShowService>.Create;
                var dic = new Dictionary<string, dynamic>();
                var host = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath,"");
                var http = host + "/H5/dcshow.html";
                var did = data["did"] == null ? "0" : data["did"].ToObject<string>();
                var cid = data["cid"] == null ? "0" : data["cid"].ToObject<string>();
                dic.Add("type", 1);
                dic.Add("did", did);
                dic.Add("cid", cid);
                dic.Add("http", http);
                var errMsg = string.Empty;
                ret.message = service.QiCodeMake(dic, ref errMsg);
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
