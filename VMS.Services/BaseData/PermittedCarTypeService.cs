using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class PermittedCarTypeService : ServiceBase, IPermittedCarTypeService
    {
        /// <summary>
        /// 添加准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddPermittedCarType(t_bd_permitted_car_type info)
        {
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_bd_permitted_car_type(type_no,type_name,memo) values(@type_no,@type_name,@memo)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@type_no", info.type_no));
            paramlst.Add(new SqlParam("@type_name", info.type_name));
            paramlst.Add(new SqlParam("@memo", info.memo));
            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        /// <summary>
        /// 修改准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool EditPermittedCarType(t_bd_permitted_car_type info)
        {
            var updateSql = new StringBuilder();
            updateSql.Append("update t_bd_permitted_car_type set ");
            List<string> fields = new List<string>();
            if (!string.IsNullOrEmpty(info.type_name))
            {
                fields.Add(" type_name=@type_name");
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
            updateSql.Append(" where type_no=@type_no ");

            SqlParam[] paramlst = new SqlParam[] { 
              new SqlParam("@type_name",info.type_name),
              new SqlParam("@memo",info.memo),
              new SqlParam("@type_no",info.type_no)
            };

            return DbContext.ExecuteBySql(updateSql, paramlst) > 0;
        }
        /// <summary>
        /// 查找准驾车型
        /// </summary>
        /// <param name="type_no"></param>
        /// <returns></returns>
        public t_bd_permitted_car_type FindByTypeNo(string type_no)
        {
            var findSql = new StringBuilder();
            findSql.Append("select type_no,type_name,memo from t_bd_permitted_car_type where type_no=@type_no ");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@type_no",type_no)
            };
            var lst = DbContext.GetDataListBySQL<t_bd_permitted_car_type>(findSql, paramlst);
            if (lst.Count > 0)
            {
                return lst[0] as t_bd_permitted_car_type;
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
        public List<t_bd_permitted_car_type> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select type_no,type_name,memo from t_bd_permitted_car_type where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "type_no", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_bd_permitted_car_type>(dt) as List<t_bd_permitted_car_type>;
        }

        public bool BatchDeletePermittedCarType(List<string> pkValues)
        {
            var tableName = "t_bd_permitted_car_type"; 
            var pkName = "type_no";
            return DbContext.BatchDeleteData(tableName, pkName, pkValues.ToArray())>0;
        }


        public List<t_bd_permitted_car_type> GetAllPermittedCarType(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select type_no,type_name from t_bd_permitted_car_type where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_bd_permitted_car_type>(sql, IList_param.ToArray()) as List<t_bd_permitted_car_type>;
        
        }
    }
}
