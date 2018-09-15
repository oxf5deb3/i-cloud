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
        public static LoginDTO get()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["login"];
            if (cookie == null)
            {
                return null;
            }
            else if (cookie.Value == null)
            {
                return null;
            }
            LoginDTO mode = null;
            try
            {
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
    }
}