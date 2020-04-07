using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VMS.DTO;
using VMS.ESIApi.Models;
using VMS.IServices;
using VMS.ServiceProvider;

namespace VMS.ESIApi
{
    /// <summary>
    /// 账号注册
    /// </summary>
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// 外部人员 注册
        /// json:{user_id:'',user_pwd:'',user_name:'',sex:'0',age:'',tel,'',email:''}
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public BaseESIReponseDTO Register([FromBody]JObject data)
        {
            var ret = new BaseESIReponseDTO();
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

    }
}