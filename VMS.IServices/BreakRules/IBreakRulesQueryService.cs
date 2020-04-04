using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.Model;

namespace VMS.IServices
{
    public interface IBreakRulesQueryService : IService
    {
        bool AddBreakRules(t_bd_breakrules info, string oper_id);

        bool EditBreakRules(t_bd_breakrules info);

        bool BatchDeleteBreakRules(List<string> pkValues);

        t_bd_breakrules FindByTypeNo(string id);

        List<BreakRulesDTO> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, bool isAsc, string sortby,ref int count);
        List<t_bd_breakrules> GetAllBreakRules(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);
    }
}
