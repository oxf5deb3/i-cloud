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

namespace VMS.Api
{
    public class RoleApiController : BaseApiController
    {

        public BaseTResponseDTO<List<UserDTO>> FindRoleUserById([FromBody]JObject data)
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

                var role_id = data["role_id"].ToObject<string>();
                var service = Instance<IRoleService>.Create;
                var roleUsers = service.FindRoleUserById(role_id);
                roleUsers.ForEach(e =>
                {
                    ret.data.Add(new UserDTO() { id = e.id.ToString(), user_id = e.user_id, user_name = e.user_name });
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
        public BaseTResponseDTO<List<GroupDTO>> FindRoleGroupById([FromBody]JObject data)
        {
            var ret = new BaseTResponseDTO<List<GroupDTO>>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion

                var role_id = data["role_id"].ToObject<string>();
                var service = Instance<IRoleService>.Create;
                var roleGroups = service.FindRoleGroupById(role_id);
                roleGroups.ForEach(e =>
                {
                    ret.data.Add(new GroupDTO() { id = e.id, group_name=e.group_name });
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

                var dto = data.ToObject<RoleUserGroupDTO>();
                var service = Instance<IRoleService>.Create;
                var dbInfo = service.FindByRoleName(dto.role_name);
                if (dbInfo != null)
                {
                    ret.success = false;
                    ret.message = string.Format("已存在此角色名称[{0}],请重新输入!", dto.role_name);
                    return ret;
                }
                dto.create_id = "1001";
                dto.create_date = DateTime.Now;
                ret.success = service.AddRole(dto);
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

                var dto = data.ToObject<RoleUserGroupDTO>();
                var service = Instance<IRoleService>.Create;
                var dbInfo = service.FindByRoleName(dto.role_name);
                if (dbInfo == null)
                {
                    ret.success = false;
                    ret.message = string.Format("不存在存在此角色名称[{0}],请刷新重试!", dto.role_name);
                    return ret;
                }
                dto.create_id = "1001";
                dto.create_date = DateTime.Now;
                ret.success = service.EditRole(dto);
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

                var dto = data["deleted"].ToObject<List<RoleDTO>>();
                var listNos = dto.Select(e => e.id).ToList();
                var service = Instance<IRoleService>.Create;
                ret.success = service.BatchDeleteRole(listNos);
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
        public GridResponseDTO<RoleDTO> Query([FromBody]JObject data)
        {
            var ret = new GridResponseDTO<RoleDTO>();
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
                var obj = Instance<IRoleService>.Create;
                var lst = obj.GetPageList(sb, paramlst, pageIndex, pageSize, ref total);
                ret.rows.AddRange(lst.Select(e => new RoleDTO() {id=e.id.ToString(),role_name=e.role_name,status=e.status,memo=e.memo }));
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
        public GridResponseDTO<RoleDTO> QueryAll()
        {
            var ret = new GridResponseDTO<RoleDTO>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IRoleService>.Create;
                var lst = obj.GetAllRole(sb, paramlst, ref total);
                ret.rows.AddRange(lst.Select(e => new RoleDTO() { id = e.id.ToString(), role_name = e.role_name,  status = e.status }));
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
        public BaseTResponseDTO<List<TreeNodeDTO>> QueryTreeAll()
        {
            var ret = new BaseTResponseDTO<List<TreeNodeDTO>>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IRoleService>.Create;
                var lst = obj.GetAllRole(sb, paramlst, ref total);
                var parentNode = new TreeNodeDTO();
                parentNode.id = "-1";
                parentNode.text = "所有角色";
                parentNode.state = "open";
                parentNode.children = new List<TreeNodeDTO>();
                parentNode.children.AddRange(lst.Select(e => new TreeNodeDTO() { id = e.id.ToString(), text = e.role_name, state = "open" }));
                ret.data.Add(parentNode);
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
        public BaseTResponseDTO<List<ResDTO>> QueryAllRes()
        {
            var ret = new BaseTResponseDTO<List<ResDTO>>();
            try
            {
                #region 参数检验

                #endregion
                var sb = new StringBuilder();
                //sb.Append(" where res_type_id='0000'");
                var paramlst = new List<SqlParam>();
                int total = 0;
                var obj = Instance<IRoleService>.Create;
                var lst = obj.GetAllResource(sb, paramlst, ref total);
                ret.data.AddRange(lst.Select(e => new ResDTO() { id = e.id.ToString(), pid = e.pid ?? "", level = e.level, res_desc = e.res_desc, res_type_id = e.res_type_id, sort_code = e.sort_code, type_desc = e.type_desc, res_type_oper_id = e.res_type_oper_id }));
                return ret;

            }
            catch (Exception ex)
            {
                ret.message = ex.Message;
                ret.success = false;
                return ret;
            }
        }
        //SaveRoleRight
       [System.Web.Mvc.HttpPost]
        public BaseResponseDTO SaveRoleRight([FromBody]JObject data)
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
               
                var rows = data["rows"].ToObject<List<RoleRightDTO>>();
                var obj = Instance<IRoleService>.Create;
                ret.success = obj.SaveRoleRight(rows);
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
        public BaseTResponseDTO<List<RoleRightDTO>> QueryRoleRightByRoleId([FromBody]JObject data)
        {
          var ret = new BaseTResponseDTO<List<RoleRightDTO>>();
            try
            {
                #region 参数检验
                if (data == null)
                {
                    ret.success = false;
                    ret.message = "无法获取到请求参数!";
                }
                #endregion
                var role_id = data["role_id"].ToObject<string>();
                var obj = Instance<IRoleService>.Create;
                var lst = obj.GetRoleRightByRoleId(role_id);
                ret.data.AddRange(lst);
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