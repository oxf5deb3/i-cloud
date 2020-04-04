using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class BaseReportService : ServiceBase
    {
        //参数
        public virtual IList<SqlParam> GetParameters(IDictionary<string, dynamic> qcondition)
        {
            return new List<SqlParam>();
        }
        //sql语句
        public virtual string GetSqlString(IDictionary<string, dynamic> qcondition)
        {
            return "";
        }
        //排序
        public virtual string GetSortString(IDictionary<string, dynamic> qcondition) { return ""; }
        public virtual List<BaseReportDTO> Query<BaseReportDTO>(IDictionary<string, dynamic> qcondition, bool loadAll, int pagesize, int pageindex, bool isasc, string orderby, ref int total, ref string err)
        {
            var list = new List<BaseReportDTO>();
            total = 0;
            try
            {
                var sql = GetSqlString(qcondition);
                var sqlParams = GetParameters(qcondition);
                var sort = GetSortString(qcondition);
                var start = 0;
                var end = 0;
                if (!loadAll)
                {
                    start = ((pageindex) == 0 ? 1 : pageindex - 1) * (pagesize) + 1;
                    end = pagesize * pageindex;
                }
                var sumSql = string.Format("SET ARITHABORT ON ;WITH t AS({0}) SELECT  COUNT(*) AS total FROM t SET ARITHABORT OFF", sql);
                var pageSql = string.Format("SET ARITHABORT ON;WITH t AS({0}),ret AS(select ROW_NUMBER() over(order by {1} {2}) as rn,t.* from t) SELECT  * FROM ret WHERE 1=1 {3} {4} SET ARITHABORT OFF", sql, orderby + " " + (isasc ? "asc" : "desc"), string.IsNullOrEmpty(sort) ? "" : sort, loadAll ? "" : " and rn>=" + start, loadAll ? "" : " and rn<=" + end);
                using (DbConnection connection = DbContext.GetDatabase().CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        DbCommand sqlStringCommand = connection.CreateCommand();
                        sqlStringCommand.CommandText = pageSql;
                        sqlStringCommand.Transaction = transaction;
                        foreach (var p in sqlParams)
                        {
                            DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                        }
                        using (IDataReader dataReader = sqlStringCommand.ExecuteReader())
                        {
                            list = ReaderToIListHelper.ReaderToList<BaseReportDTO>(dataReader);
                        }
                        sqlStringCommand.CommandText = sumSql;
                        foreach (var p in sqlParams)
                        {
                            DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                        }
                        var result = sqlStringCommand.ExecuteScalar();
                        transaction.Commit();
                        connection.Close();
                        total = (int)result;
                        return list;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        err = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return list;
        }



    }
}
