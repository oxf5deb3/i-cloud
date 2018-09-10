using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;
using VMS.DTO;

namespace VMS.IServices
{
    public interface IRoleService:IService
    {
        //bool AddRegion(Region info);

       // bool EditRegion(Region info);
        List<t_sys_user> FindRoleUserById(string role_id);
        List<t_sys_group> FindRoleGroupById(string role_id);
        bool BatchDeleteRole(List<string> pkValues);

        //Region FindByRegionNo(string region_no);
        List<t_sys_role> GetAllRole(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);
        t_sys_role FindByRoleName(string role_name);

        bool AddRole(RoleUserGroupDTO role);
        bool EditRole(RoleUserGroupDTO role);
        List<t_sys_role> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);
    }
}
