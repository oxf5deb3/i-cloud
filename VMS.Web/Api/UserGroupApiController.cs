using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.ServiceProvider;

namespace VMS.Api
{
    public class UserGroupApiController : BaseApiController
    {
        [System.Web.Mvc.HttpPost]
        public BaseTResponseDTO<List<RoleDTO>> FindGroupRoleById([FromBody]JObject data)
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

                var group_id = data["group_id"].ToObject<string>();
                var service = Instance<IUserGroupService>.Create;
                var userRoles = service.FindGroupRoleByGroupId(group_id);
                userRoles.ForEach(e =>
                {
                    ret.data.Add(new RoleDTO() { id = e.group_id.ToString(), role_name = e.role_name });
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
        public BaseTResponseDTO<List<UserDTO>> FindGroupUserById([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<List<UserDTO>>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var group_id = data["group_id"].ToObject<string>();
                var service = Instance<IUserGroupService>.Create;
                var userGroups = service.FindGroupUserByGroupId(group_id);
                userGroups.ForEach(e =>
                {
                    ret.data.Add(new UserDTO() {user_id=e.user_id,user_name=e.user_name });
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

                var dto = data.ToObject<GroupRoleDTO>();
                var service = Instance<IUserGroupService>.Create;
                var isExist = service.IsExist(dto.group_name);
                if (!isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在此用户组[{0}],请刷新重试!", dto.group_name);
                    return ret;
                }
                ret.success = service.EditGroup(dto);
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

                var dto = data.ToObject<GroupRoleDTO>();
                var service = Instance<IUserGroupService>.Create;
                var isExist = service.IsExist(dto.group_name);
                if (isExist)
                {
                    ret.success = false;
                    ret.message = string.Format("已存在此用户组[{0}],请重新输入!", dto.group_name);
                    return ret;
                }
                dto.create_id = "1001";
                ret.success = service.AddGroup(dto);
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

                var dto = data["deleted"].ToObject<List<UserGroupDTO>>();
                var listNos = dto.Select(e => e.id.ToString()).ToList();
                var service = Instance<IUserGroupService>.Create;
                ret.success = service.BatchDeleteGroup(listNos);
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
        public GridResponseDTO<UserGroupDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<UserGroupDTO>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var pageIndex = data["page"] == null ? 1 : data["page"].ToObject<int>();
                var pageSize = data["rows"] == null ? 20 : data["rows"].ToObject<int>();
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IUserGroupService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new UserGroupDTO() { id = e.id, group_name = e.group_name, create_id = e.create_id, create_date = e.create_date, status = e.status, memo = e.memo }));
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
        public GridResponseDTO<UserGroupDTO> QueryAllGroup()
        {
            var ret = new GridResponseDTO<UserGroupDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IUserGroupService>.Create;
                var lst = obj.GetAllGroup(sb, paramlst, ref total);
                ret.rows.AddRange(lst.Select(e => new UserGroupDTO() { id = e.id, group_name = e.group_name, status = e.status, memo = e.memo }));
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

    }
}