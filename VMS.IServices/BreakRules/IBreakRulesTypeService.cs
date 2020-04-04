using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.IServices
{
    public interface IBreakRulesTypeService : IService
    {
        bool AddBreakRulesType(t_bd_breakrules_type info,string oper_id);

        bool EditBreakRulesType(t_bd_breakrules_type info);

        bool BatchDeleteBreakRulesType(List<string> pkValues);

        t_bd_breakrules_type FindByTypeNo(string id);

        List<t_bd_breakrules_type> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);
        List<t_bd_breakrules_type> GetAllBreakRulesType(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);

    }
}
