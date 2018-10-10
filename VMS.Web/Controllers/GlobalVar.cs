using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using VMS.DTO;

namespace VMS.Controllers
{
    public class GlobalVar
    {
        public static string LOGIN_KEY = "login";
        public static LoginDTO get()
        {
            LoginDTO mode = null;
            var loginSession = HttpContext.Current.Session[LOGIN_KEY] as string;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[LOGIN_KEY];
            if (loginSession == null)
            {
                if (cookie == null)
                {
                    return null;
                }
                else if (cookie.Value == null)
                {
                    return null;
                }
                try
                {
                    HttpContext.Current.Session.Add(LOGIN_KEY, cookie.Value);
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(cookie.Value);
                    mode = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginDTO>(authTicket.UserData);
                    //经销商模拟登陆
                    return mode;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                if (cookie == null || cookie.Value == null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(loginSession);
                    var userData = authTicket.UserData;
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, GlobalVar.LOGIN_KEY, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData, FormsAuthentication.FormsCookiePath);
                    string enyTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie newCookie = new HttpCookie(LOGIN_KEY, enyTicket);
                    HttpContext.Current.Response.Cookies.Add(newCookie);
                }
                FormsAuthenticationTicket authTicket1 = FormsAuthentication.Decrypt(loginSession);
                mode = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginDTO>(authTicket1.UserData);
                //经销商模拟登陆
                return mode;
            }
        }
    }
}