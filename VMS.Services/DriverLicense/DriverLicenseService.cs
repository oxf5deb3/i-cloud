using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.DTO;
using VMS.Model;

namespace VMS.Services
{
    public class DriverLicenseService : BaseReportService, IDriverLicenseService
    {
        public override string GetSqlString(Dictionary<string, string> qcondition)
        {
            return "";
        }
        public override IList<SqlParam> GetParameters(Dictionary<string, string> qcondition)
        {
            return new List<SqlParam>();
        }
        public override List<DriverLicenseDTO> Query<DriverLicenseDTO>(Dictionary<string, string> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err)
        {
            return base.Query<DriverLicenseDTO>(qcondition, loadAll, pagesize, pageindex, isasc, orderby, ref  total, ref err);
        }
    }
}
