using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.IServices
{
    public interface IRegionService : IService
    {
        bool AddRegion(t_bd_region info);

        bool EditRegion(t_bd_region info);

        bool BatchDeleteRegion(List<string> pkValues);

        t_bd_region FindByRegionNo(string region_no);

        List<t_bd_region> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count);
        List<t_bd_region> GetAllRegion(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count);
    }
}
