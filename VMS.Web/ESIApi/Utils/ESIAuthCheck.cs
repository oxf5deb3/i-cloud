using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using VMS.ESIApi.Models;

namespace VMS.ESIApi.Utils
{
    public class ESIAuthCheck : AuthorizeAttribute
    {
       // public static DefaultMemoryCache _cache = new DefaultMemoryCache();
        /// <summary>
        /// 主要作用:
        /// </summary>
        /// <param name="actionContext"></param>
        //接口限流
        //重复调用
        //请求数据采用json+加密签名

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            //SortedDictionary
            //url获取token 
            //var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;
            //HttpRequestBase request = content.Request;
            //string access_key = request.Form["access_key"];//获取请求参数对应的值
            //string sign = request.Form["sign"];
            //if (!string.IsNullOrEmpty(access_key) && !string.IsNullOrEmpty(sign))
            //var json = "";
            //using (Stream st = request.InputStream)
            //{
            //    StreamReader sr = new StreamReader(st, Encoding.UTF8);
            //    json = sr.ReadToEnd();
            //}
            //var token = "";
            //if (!string.IsNullOrEmpty(json))
            //{
            //    var jobj = JObject.Parse(json);
            //    if(jobj!=null && jobj["token"] != null)
            //    {
            //        token = jobj["token"].ToString();
            //    }
            //}
           

            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                var isjson = false;
                try
                {
                    isjson = ("application/json").Equals(actionContext.Request.Content.Headers.ContentType.MediaType.ToLower());
                }
                catch
                {
                }
                
                if (isjson)
                {
                    var requestContent = actionContext.Request.Content.ReadAsStringAsync();
                    requestContent.Wait();
                    var json = "";
                    json = requestContent.Result;
                    VMS.Utils.Log4NetHelper.Info("接受到json串:" + json);
                    var token = "";
                    if (!string.IsNullOrEmpty(json))
                    {
                        try
                        {
                            var jobj = JObject.Parse(json);
                            if (jobj != null && jobj["token"] != null)
                            {
                                token = jobj["token"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            VMS.Utils.Log4NetHelper.Info("无法将参数解析为json,异常信息:" + ex.StackTrace);
                        }

                    }
                    if (!string.IsNullOrEmpty(token))
                    {
                        //解密用户ticket,并校验用户名密码是否匹配 
                        //if (ValidateTicket(access_key, sign))
                        if (ValidateTicket(token, ""))
                        {
                            base.IsAuthorized(actionContext);
                        }
                        else
                        {
                            VMS.Utils.Log4NetHelper.Info("授权失败");
                            HandleUnauthorizedRequest(actionContext);
                        }
                    }
                    else
                    {
                        //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                        HandleUnauthorizedRequest(actionContext);
                    }
                }
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Forbidden;
            var content = Models.DefaultResponse.Fail<string>(StatusCode.UNAUTHORIZED);
            content.data = "";
            var json = JObject.FromObject(content).ToString();
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        //校验sign（数据库数据匹配） 
        private bool ValidateTicket(string key, string sign)
        {
            //var result = sp_portuserbll.GetAccess_secret(key);
            //if (!string.IsNullOrEmpty(result))
            //{
            //    var mysing = Encryption.DataEncryption(key, result);//sign验证
            //    if (mysing.Equals(sign))
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            var entry = CacheHelper.Get(key);
            VMS.Utils.Log4NetHelper.Info("查询key:"+key);
            if (entry != null)
            {
                VMS.Utils.Log4NetHelper.Info("查询value:" + JObject.FromObject(entry).ToString());
            }
            return true;
        }
    }
}