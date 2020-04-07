using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.Model;

namespace VMS.IServices
{
    public interface IUserService : IService
    {
        bool IsExist(string user_id,bool isInnerUser=true);
        List<t_sys_oper_role> FindOperRoleByUserId(string user_id);
        bool BatchDeleteUser(List<string> pkValues);
        t_sys_user FindByUserId(string user_id);
        bool AddUser(UserRoleDTO user);
        bool AddOuterUser(UserRoleDTO user);
        bool EditUser(UserRoleDTO user);
        List<t_sys_user> GetAllUser(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);
        List<t_sys_user> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);
    
    }
}
