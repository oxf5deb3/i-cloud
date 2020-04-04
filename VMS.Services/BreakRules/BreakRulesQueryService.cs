using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class BreakRulesQueryService : ServiceBase, IBreakRulesQueryService
    {
        public bool AddBreakRules(t_bd_breakrules info, string oper_id)
        {
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_bd_breakrules(name,sex,age,phone,id_card,car_no,driver_no,driving_no,carframe_no,breakrule_type_id,breakrule_addr,breakrule_date,memo,memo1,memo2,oper_id,oper_date)");
            insertSql.Append(" values(@name,@sex,@age,@phone,@id_card,@car_no,@driver_no,@driving_no,@carframe_no,@breakrule_type_id,@breakrule_addr,@breakrule_date,@memo,@memo1,@memo2,@oper_id,getdate())");
            List <SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@name", info.name));
            paramlst.Add(new SqlParam("@sex", info.sex));
            paramlst.Add(new SqlParam("@age", info.age));
            paramlst.Add(new SqlParam("@phone", info.phone));
            paramlst.Add(new SqlParam("@id_card", info.id_card));
            paramlst.Add(new SqlParam("@car_no", info.car_no));
            paramlst.Add(new SqlParam("@driver_no", info.driver_no));
            paramlst.Add(new SqlParam("@driving_no", info.driving_no));
            paramlst.Add(new SqlParam("@carframe_no", info.carframe_no));
            paramlst.Add(new SqlParam("@breakrule_type_id", info.breakrule_type_id));
            paramlst.Add(new SqlParam("@breakrule_addr", info.breakrule_addr));
            paramlst.Add(new SqlParam("@breakrule_date", info.breakrule_date));
            paramlst.Add(new SqlParam("@memo", info.memo));
            paramlst.Add(new SqlParam("@memo1", info.memo1));
            paramlst.Add(new SqlParam("@memo2", info.memo2));
            paramlst.Add(new SqlParam("@oper_id", info.oper_id));
            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        public bool BatchDeleteBreakRules(List<string> pkValues)
        {

            var tableName = "t_bd_breakrules";
            var pkName = "id";
            return DbContext.BatchDeleteData(tableName, pkName, pkValues.ToArray()) > 0;
        }

        public bool EditBreakRules(t_bd_breakrules info)
        {
            var updateSql = new StringBuilder();
            updateSql.Append("delete from t_bd_breakrules where id=@id ");
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_bd_breakrules(name,sex,age,phone,id_card,car_no,driver_no,driving_no,carframe_no,breakrule_type_id,breakrule_addr,breakrule_date,memo,memo1,memo2,oper_id,oper_date)");
            insertSql.Append(" values(@name,@sex,@age,@phone,@id_card,@car_no,@driver_no,@driving_no,@carframe_no,@breakrule_type_id,@breakrule_addr,@breakrule_date,@memo,@memo1,@memo2,@oper_id,getdate())");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@id", info.id));
            paramlst.Add(new SqlParam("@name", info.name));
            paramlst.Add(new SqlParam("@sex", info.sex));
            paramlst.Add(new SqlParam("@age", info.age));
            paramlst.Add(new SqlParam("@phone", info.phone));
            paramlst.Add(new SqlParam("@id_card", info.id_card));
            paramlst.Add(new SqlParam("@car_no", info.car_no));
            paramlst.Add(new SqlParam("@driver_no", info.driver_no));
            paramlst.Add(new SqlParam("@driving_no", info.driving_no));
            paramlst.Add(new SqlParam("@carframe_no", info.carframe_no));
            paramlst.Add(new SqlParam("@breakrule_type_id", info.breakrule_type_id));
            paramlst.Add(new SqlParam("@breakrule_addr", info.breakrule_addr));
            paramlst.Add(new SqlParam("@breakrule_date", info.breakrule_date));
            paramlst.Add(new SqlParam("@memo", info.memo));
            paramlst.Add(new SqlParam("@memo1", info.memo1));
            paramlst.Add(new SqlParam("@memo2", info.memo2));
            paramlst.Add(new SqlParam("@oper_id", info.oper_id));
           return DbContext.ExecuteBySql(updateSql.AppendLine(insertSql.ToString()),paramlst.ToArray())>0;
        }

        public t_bd_breakrules FindByTypeNo(string id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select id,name,sex,age,phone,id_card,car_no,driver_no,driving_no,carframe_no,breakrule_type_id,breakrule_addr,breakrule_date,memo,memo1,memo2,oper_id,oper_date" +
                " from t_bd_breakrules where id=@id ");
            SqlParam[] paramlst = new SqlParam[] {
               new SqlParam("@id",id)
            };
            var lst = DbContext.GetDataListBySQL<t_bd_breakrules>(findSql, paramlst);
            if (lst.Count > 0)
            {
                return lst[0] as t_bd_breakrules;
            }
            return null;
        }

        public List<t_bd_breakrules> GetAllBreakRules(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,name,sex,age,phone,id_card,car_no,driver_no,driving_no,carframe_no,breakrule_type_id,breakrule_addr,breakrule_date,memo,memo1,memo2,oper_id,oper_date where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_bd_breakrules>(sql, IList_param.ToArray()) as List<t_bd_breakrules>;

        }

        public List<BreakRulesDTO> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize,bool isAsc,string sortby, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select r.id,r.name,sex,age,phone, id_card, car_no, driver_no, driving_no, carframe_no, breakrule_type_id, breakrule_type_name = t.name," +
                "breakrule_addr, breakrule_date, r.memo, memo1, memo2, r.oper_id, r.oper_date"+
                " from t_bd_breakrules r" +
                " left join t_bd_breakrules_type t on r.breakrule_type_id = t.id " +
                " where 1=1 ");
            sql.Append(SqlWhere);
            var sort = "asc";
            if (!isAsc)
            {
                sort = "desc";
            }
            if (string.IsNullOrEmpty(sortby))
            {
                sortby = "id";
            }
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), sortby, sort, pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<BreakRulesDTO>(dt) as List<BreakRulesDTO>;
        }
    }
}
