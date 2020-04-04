using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class BreakRulesTypeService : ServiceBase, IBreakRulesTypeService
    {
        /// <summary>
        /// 添加准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddBreakRulesType(t_bd_breakrules_type info,string oper_id)
        {
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_bd_breakrules_type(name,punish_desc,memo,oper_id) values(@name,@punish_desc,@memo,@oper_id)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@name", info.name));
            paramlst.Add(new SqlParam("@punish_desc", info.punish_desc));
            paramlst.Add(new SqlParam("@memo", info.memo));
            paramlst.Add(new SqlParam("@oper_id", oper_id));
            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        /// <summary>
        /// 修改准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool EditBreakRulesType(t_bd_breakrules_type info)
        {
            var updateSql = new StringBuilder();
            updateSql.Append("update t_bd_breakrules_type set ");
            List<string> fields = new List<string>();
            if (!string.IsNullOrEmpty(info.name))
            {
                fields.Add(" name=@name");
            }
            if (!string.IsNullOrEmpty(info.punish_desc))
            {
                fields.Add(" @punish_desc=@punish_desc");
            }
            if (!string.IsNullOrEmpty(info.memo))
            {
                fields.Add(" memo=@memo ");
            }
            if (fields.Count > 0)
            {
                updateSql.Append(string.Join(",", fields));
            }
            else
            {
                updateSql.Append(" 1=1");
            }
            updateSql.Append(" where id=@id ");

            SqlParam[] paramlst = new SqlParam[] {
              new SqlParam("@name",info.name),
              new SqlParam("@punish_desc",info.punish_desc),
              new SqlParam("@memo",info.memo),
              new SqlParam("@id",info.id)
            };

            return DbContext.ExecuteBySql(updateSql, paramlst) > 0;
        }
        /// <summary>
        /// 查找准驾车型
        /// </summary>
        /// <param name="type_no"></param>
        /// <returns></returns>
        public t_bd_breakrules_type FindByTypeNo(string id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select id,name,punish_desc,memo,oper_id,oper_date from t_bd_breakrules_type where id=@id ");
            SqlParam[] paramlst = new SqlParam[] {
               new SqlParam("@id",id)
            };
            var lst = DbContext.GetDataListBySQL<t_bd_breakrules_type>(findSql, paramlst);
            if (lst.Count > 0)
            {
                return lst[0] as t_bd_breakrules_type;
            }
            return null;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="SqlWhere"></param>
        /// <param name="IList_param"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<t_bd_breakrules_type> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,name,punish_desc,memo,oper_id,oper_date from t_bd_breakrules_type where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_bd_breakrules_type>(dt) as List<t_bd_breakrules_type>;
        }

        public bool BatchDeleteBreakRulesType(List<string> pkValues)
        {
            var tableName = "t_bd_breakrules_type";
            var pkName = "id";
            return DbContext.BatchDeleteData(tableName, pkName, pkValues.ToArray()) > 0;
        }


        public List<t_bd_breakrules_type> GetAllBreakRulesType(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,name,punish_desc,memo,oper_id,oper_date from t_bd_breakrules_type where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_bd_breakrules_type>(sql, IList_param.ToArray()) as List<t_bd_breakrules_type>;

        }


    }
}
