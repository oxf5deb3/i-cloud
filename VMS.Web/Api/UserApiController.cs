using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;
using VMS.Utils;

namespace VMS.Api
{
    public class UserApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        public BaseTResponseDTO<UserDTO> FindUserById([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<UserDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var user_id = data["user_id"].ToObject<string>();
                var service = Instance<IUserService>.Create;
                var user = service.FindByUserId(user_id);
                ret.data = new UserDTO() { id = user.id.ToString(), user_id = user.user_id, user_pwd = user.user_pwd, user_name = user.user_name, sex = user.sex, age = user.age, tel = user.tel, email = user.email, status = user.status };
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
        public BaseTResponseDTO<List<RoleDTO>> FindOperRoleById([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<List<RoleDTO>>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var user_id = data["user_id"].ToObject<string>();
                var service = Instance<IUserService>.Create;
                var userRoles = service.FindOperRoleByUserId(user_id);
                userRoles.ForEach(e =>
                {
                    ret.data.Add(new RoleDTO() { id = e.role_id, role_name = e.role_name });
                });
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
        public BaseResponseDTO Edit([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var isExist = service.IsExist(dto.user_id);
                if (!isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在此账号[{0}],请刷新重试!", dto.user_id);
                    return ret;
                }
                ret.success = service.EditUser(dto);
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
        public BaseResponseDTO Add([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var isExist = service.IsExist(dto.user_id);
                if (isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("已存在此账号[{0}],请重新输入!", dto.user_id);
                    return ret;
                }
                dto.create_date = DateTime.Now;
                ret.success = service.AddUser(dto);
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
        public BaseResponseDTO Del([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data["deleted"].ToObject<List<UserDTO>>();
                var listNos = dto.Select(e => e.user_id).ToList();
                var service = Instance<IUserService>.Create;
                ret.success = service.BatchDeleteUser(listNos);
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
        public GridResponseDTO<UserDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<UserDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var condition = data.ToDictionary();
                condition.Add(new KeyValuePair<string, dynamic>("user_type", "0"));
                var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                string err = string.Empty;
                var obj = Instance<IUserService>.Create;
                List<t_sys_user> lst = obj.GetUserPageList(condition, sort, order, pageIndex, pageSize, ref total, ref err);
                //var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new UserDTO() { id = e.id.ToString(), user_id = e.user_id, user_pwd = e.user_pwd, user_name = e.user_name, sex = e.sex, age = e.age, tel = e.tel, email = e.email, status = e.status, last_login_time = e.last_login_time, create_date = e.create_date }));
                ret.total = total;
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
        [System.Web.Mvc.HttpGet]
        public GridResponseDTO<RoleUserGroupDTO> QueryAll()
        {
            var ret = new GridResponseDTO<RoleUserGroupDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IUserService>.Create;
                var lst = obj.GetAllUser(sb, paramlst, ref total);
                ret.rows.AddRange(lst.Select(e => new RoleUserGroupDTO() { id = e.id, user_id = e.user_id, user_name = e.user_name, status = e.status }));
                ret.total = total;
                return ret;

            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        #region 外部人员
        /// <summary>
        /// 外部人员查询
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public GridResponseDTO<UserDTO> OuterQuery([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<UserDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var condition = data.ToDictionary();
                condition.Add(new KeyValuePair<string, dynamic>("user_type", "1"));
                var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sort = condition.ContainsKey("sort") ? CommonHelper.GetString(condition["sort"]) : "";
                var order = condition.ContainsKey("order") ? (CommonHelper.GetString(condition["order"]) == "asc" ? true : false) : true;
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                string err = string.Empty;
                var obj = Instance<IUserService>.Create;
                List<t_sys_user> lst = obj.GetUserPageList(condition, sort, order, pageIndex, pageSize, ref total, ref err);
                //var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new UserDTO() { id = e.id.ToString(), user_id = e.user_id, user_pwd = e.user_pwd, user_name = e.user_name, sex = e.sex, age = e.age, tel = e.tel, email = e.email, status = e.status, last_login_time = e.last_login_time, create_date = e.create_date }));
                ret.total = total;
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
        public BaseTResponseDTO<UserDTO> FindOuterUserById([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<UserDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var user_id = data["user_id"].ToObject<string>();
                var service = Instance<IUserService>.Create;
                var user = service.FindByUserId(user_id,"1");
                ret.data = new UserDTO() { id = user.id.ToString(), user_id = user.user_id, user_pwd = user.user_pwd, user_name = user.user_name, sex = user.sex, age = user.age, tel = user.tel, email = user.email, status = user.status };
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
        public BaseResponseDTO EditOuterUser([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data.ToObject<UserRoleDTO>();
                var service = Instance<IUserService>.Create;
                var isExist = service.IsExist(dto.user_id,false);
                if (!isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在此账号[{0}],请刷新重试!", dto.user_id);
                    return ret;
                }
                ret.success = service.EditOuterUser(dto);
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
        public BaseResponseDTO DelOuter([FromBody]JObject data)
        {
            var ret = new BaseResponseDTO();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var dto = data["deleted"].ToObject<List<UserDTO>>();
                var listNos = dto.Select(e => e.id).ToList();
                var service = Instance<IUserService>.Create;
                ret.success = service.BatchDeleteOuterUser(listNos);
                return ret;
            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        #endregion
    }
}