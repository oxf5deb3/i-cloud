using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.ESIApi.Models;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.ESIApi
{
    /// <summary>
    /// 账号注册
    /// </summary>
    
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// 外部人员 注册
        /// </summary>
        /// <param name="user_id">string 账号</param>
        /// <param name="user_pwd">string 密码</param>
        /// <param name="email">string 邮箱</param>
        /// <returns></returns>
        [AllowAnonymous]
        public Response<string> Register([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.code = ESIApi.StatusCode.PARAM_NULL;
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var isExist = service.IsExist(dto.user_id,false);
                if (isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("已存在此账号[{0}],请重新输入!", dto.user_id);
                    return ret;
                }
                dto.create_date = DateTime.Now;
                ret.success = service.AddOuterUser(dto);
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user_id">账号</param>
        /// <param name="user_pwd">密码</param>
        /// <param name="user_type">用户类型</param>
        /// <returns></returns>
        [AllowAnonymous]
        public Response<ESIUserLoginDTO> Login([FromBody]JObject data)
        {
            var ret = new Response<ESIUserLoginDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.code = ESIApi.StatusCode.PARAM_NULL;
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<LoginDTO>();
                var service = Instance<IUserService>.Create;
                var user = service.FindByUserId(dto.user_id, dto.user_type);
                if (user == null || !(user.user_pwd).Equals(dto.user_pwd))
                {
                    ret.success = false;
                    ret.message = string.Format("账号或密码错误,请重试!");
                    return ret;
                }
                var rights = service.GetAppMenuRights(dto.user_id);
                var guid = Guid.NewGuid().ToString().Replace("-", "");
                var userdto = new ESIUserLoginDTO()
                {
                    user_id = user.user_id,
                    user_name = user.user_name,
                    token = guid,
                    create_date = user.create_date,
                    user_type = user.user_type,
                    email = user.email,
                    menu_right = rights
                };
                ret.data = userdto;
                Utils.CacheHelper.SlideInsert(guid,userdto,TimeSpan.FromHours(12));
                var ip = HttpContext.Current.Request.UserHostAddress;
                try
                {
                    var logservice = Instance<ILogService>.Create;
                    logservice.WriteLoginLog(new LoginLogDTO() { region_no = "", ip = ip, login_id = dto.user_id, login_date = DateTime.Now });
                }
                catch (Exception ex)
                {
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.CATCH_EXCEPTION;
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="user_id">账号</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public Response<string> ForgetPwd([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<UserRoleDTO>(); 
                var service = Instance<IUserService>.Create;
                var user_id = dto.user_id;
                var email = dto.email;
                var httpaddr = HttpContext.Current.Request.Url.Authority;
                var success = service.AddLostPwdRecord(user_id, email, httpaddr);
                ret.success = success;
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.FAIL;
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="user_pwd">新密码</param>
        /// <returns></returns>
        public Response<string> ResetPwd([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var user_pwd = dto.user_pwd;
                var guid = data["guid"] != null ? data["guid"].ToObject<string>() : "";
                var err = "";
                var success = service.ResetPwd(user_pwd, guid, ref err);
                ret.success = success;
                if (!success && !string.IsNullOrEmpty(err))
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = err;
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.FAIL;
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="user_id">账号</param>
        /// <param name="old_pwd">旧密码</param>
        /// <param name="new_pwd">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public Response<string> ModifyPwd([FromBody]JObject data)
        {
            var ret = new Response<string>();
            try
            {
                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var user_id = data["user_id"] != null ? data["user_id"].ToObject<string>() : "";
                var old_pwd = data["old_pwd"] != null ? data["old_pwd"].ToObject<string>() : "";
                var new_pwd = data["new_pwd"] != null ? data["new_pwd"].ToObject<string>() : "";
                var err = "";
                var success = service.ModifyPwd(user_id, old_pwd,new_pwd, ref err);
                ret.success = success;
                if (!success && !string.IsNullOrEmpty(err))
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = err;
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.FAIL;
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <param name="user_id">账号</param>
        /// <param name="user_type">用户类型</param>
        /// <returns></returns>
        [HttpPost]
        public Response<ESIUserProfileDTO> FindUserById([FromBody]JObject data)
        {
            var ret = new Response<ESIUserProfileDTO>();
            try
            {
                var service = Instance<IUserService>.Create;
                var user_id = data["user_id"] != null ? data["user_id"].ToObject<string>() : "";
                var user_type = data["user_type"] != null ? data["user_type"].ToObject<string>() : "1";
                var err = "";
                var user = service.FindByUserId(user_id, user_type);
                if (user != null)
                {
                    var dtto = new ESIUserProfileDTO();
                    dtto.user_id = user.user_id;
                    dtto.user_name = user.user_name;
                    dtto.user_type = user.user_type;
                    dtto.create_date = user.create_date;
                    dtto.last_login_time = user.last_login_time;
                    dtto.tel = user.tel;
                    dtto.sex = user.sex;
                    dtto.age = user.age;
                    dtto.email = user.email;

                    var sys = Instance<ISystemService>.Create;
                    var tel = sys.QuerySetting("help_tel");
                    if(tel!=null && tel.Count > 0)
                    {
                        dtto.helper_tel = tel[0].sys_var_val;
                    }
                    ret.data = dtto;
                }
                else
                {
                    ret.code = ESIApi.StatusCode.FAIL;
                    ret.message = "无法找到该用户";
                }
                return ret;
            }
            catch (Exception ex)
            {
                ret.code = ESIApi.StatusCode.FAIL;
                ret.message = "无法找到该用户,错误信息:"+ex.Message;
                ret.success = false;
                return ret;
            }
        }
    }
}