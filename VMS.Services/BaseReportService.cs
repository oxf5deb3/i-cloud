using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.Services
{
    public class BaseReportService : ServiceBase
    {
        //参数
        public virtual IList<SqlParam> GetParameters(Dictionary<string, string> qcondition)
        {
            return null;
        }
        //sql语句
        public virtual string GetSqlString(Dictionary<string, string> qcondition)
        {
            return null;
        }
        //排序
        public virtual string GetDefaultSortString() { return null; }
        public List<T> Query<T>(Dictionary<string, string> qcondition,ref string err) where T : class,new()
        {
            try
            {
                var sql = GetSqlString(qcondition);
                var sqlParams = GetParameters(qcondition);
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return null;
        }

    }
}
