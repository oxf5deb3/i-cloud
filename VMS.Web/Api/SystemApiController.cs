using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using VMS.DTO;
using VMS.IServices;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class SystemApiController : BaseApiController
    {
        //加载菜单
        [HttpPost]
        public BaseTResponseDTO<List<string>> LoadMenu()
        {



            return null;

        }

        //登录
        [HttpPost]
        [AllowAnonymous]
        public BaseResponseDTO Login([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                    return ret;
                }
                if (data["user_id"] == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到账号!";
                    return ret;
                }
                if (data["user_pwd"] == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到密码!";
                    return ret;
                }
                #endregion
                var dto = data.ToObject<LoginDTO>();
                string userData = "";
                var service = Instance<IUserService>.Create;
                var user = service.FindByUserId(dto.user_id);
                if (user == null || !(user.user_pwd).Equals(dto.user_pwd))
                {
                    ret.success = false;
                    ret.message = "账号或密码错误,请重试!";
                    return ret;
                }
                var ip = HttpContext.Current.Request.UserHostAddress;
                dto.login_ip = ip;
                dto.user_pwd = "";
                userData = JsonConvert.SerializeObject(dto);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "login", DateTime.Now, DateTime.Now.AddMinutes(30), false, userData, FormsAuthentication.FormsCookiePath);
                string enyTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(ticket.Name, enyTicket);
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
                return ret;
            }
            catch (Exception ex)
            {
                ret.success = false;
                ret.message = ex.Message;
                return ret;
            }
        }

        //退出
        public void Logout()
        {

        }
    }
}