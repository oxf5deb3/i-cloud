using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;

namespace VMS.IServices
{
    public interface ILoginLogService : IService
    {
        List<LoginLogDTO> Query<LoginLogDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err);

    }
}
