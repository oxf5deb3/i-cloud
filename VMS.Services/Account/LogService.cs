using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;

namespace VMS.Services
{
    public class LogService : ServiceBase, ILogService
    {
        public bool WriteLoginLog(DTO.LoginLogDTO log)
        {
            var insertSql = new StringBuilder();
            insertSql.Append(" INSERT INTO [dbo].[t_sys_login_log]([region_no],[ip],[login_id],[login_date]) VALUES(@region_no,@ip,@login_id,@login_date) ");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@region_no",log.region_no),
               new SqlParam("@ip",log.ip),
               new SqlParam("@login_id",log.login_id),
               new SqlParam("@login_date",log.login_date),
            };
            return DbContext.ExecuteBySql(insertSql, paramlst) > 0;
        }

        public bool WriteOperateLog(DTO.OperateLogDTO log)
        {
            var insertSql = new StringBuilder();
            insertSql.Append(" INSERT INTO t_sys_operate_log(region_no,oper_desc ,memo,oper_id,oper_date)VALUES (@region_no,@oper_desc,@memo,@oper_id,@oper_date) ");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@region_no",log.region_no),
               new SqlParam("@oper_desc",log.oper_desc),
               new SqlParam("@memo",log.memo),
               new SqlParam("@oper_id",log.oper_id),
               new SqlParam("@oper_date",log.oper_date)};
            return DbContext.ExecuteBySql(insertSql, paramlst) > 0;
        }
    }
}
