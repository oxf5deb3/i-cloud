using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace VMS.ESIApi.Utils
{
    public class ESIAuthCheck: AuthorizeAttribute
    {
        public static DefaultMemoryCache _cache = new DefaultMemoryCache();
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
            var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;
            HttpRequestBase request = content.Request;
            string access_key = request.Form["access_key"];//获取请求参数对应的值
            string sign = request.Form["sign"];
            if (!string.IsNullOrEmpty(access_key) && !string.IsNullOrEmpty(sign))
            {
                //解密用户ticket,并校验用户名密码是否匹配 
                if (ValidateTicket(access_key, sign))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
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
            return false;
        }
    }
}