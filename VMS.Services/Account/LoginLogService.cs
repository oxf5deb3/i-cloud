using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;

namespace VMS.Services
{
    public class LoginLogService : BaseReportService, ILoginLogService
    {
        public override string GetSqlString(IDictionary<string, dynamic> qcondition)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT id,region_no,ip,login_id,login_date");
            sql.Append(" FROM t_sys_login_log a");
            sql.Append(" where 1=1 ");
            if (qcondition["login_id"] != null && !string.IsNullOrEmpty(qcondition["login_id"]))
            {
                sql.Append(" and a.login_id like @login_id ");
            }
            return sql.ToString();
        }
        public override IList<SqlParam> GetParameters(IDictionary<string, dynamic> qcondition)
        {
            var lstParams = new List<SqlParam>();
            if (qcondition["login_id"] != null && !string.IsNullOrEmpty(qcondition["login_id"]))
            {
                lstParams.Add(new SqlParam("@login_id", "%" + qcondition["login_id"] + "%"));
            }
            return lstParams;
        }
    }
}
