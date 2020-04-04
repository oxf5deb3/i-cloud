using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;

namespace VMS.Services
{
    public class OperateLogService : BaseReportService, IOperateLogService
    {
        public override string GetSqlString(IDictionary<string, dynamic> qcondition)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT id,region_no,oper_desc,memo,oper_id,oper_date");
            sql.Append(" FROM t_sys_operate_log a");
            sql.Append(" where 1=1 ");
            if (qcondition["oper_id"] != null && !string.IsNullOrEmpty(qcondition["oper_id"]))
            {
                sql.Append(" and a.oper_id like @oper_id ");
            }
            return sql.ToString();
        }
        public override IList<SqlParam> GetParameters(IDictionary<string, dynamic> qcondition)
        {
            var lstParams = new List<SqlParam>();
            if (qcondition["oper_id"] != null && !string.IsNullOrEmpty(qcondition["oper_id"]))
            {
                lstParams.Add(new SqlParam("@oper_id", "%" + qcondition["oper_id"] + "%"));
            }
            return lstParams;
        }
    }
}
