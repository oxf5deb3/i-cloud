using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.Model;

namespace VMS.IServices
{
    public interface IUserGroupService : IService
    {
        bool IsExist(string group_name);
        List<t_sys_group_role> FindGroupRoleByGroupId(string group_id);
        List<t_sys_user_group> FindGroupUserByGroupId(string group_id);

        bool BatchDeleteGroup(List<string> pkValues);

        bool AddGroup(GroupRoleDTO group);

        bool EditGroup(GroupRoleDTO group);


        List<t_sys_group> GetAllGroup(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);
        List<t_sys_group> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);
    
    }
}
