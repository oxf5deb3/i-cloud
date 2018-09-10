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
    public class RegionService : ServiceBase, IRegionService
    {
        /// <summary>
        /// 添加准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool AddRegion(t_bd_region info)
        {
            var insertSql = new StringBuilder();
            insertSql.Append("insert into t_bd_region(region_no,region_name,memo) values(@region_no,@region_name,@memo)");
            List<SqlParam> paramlst = new List<SqlParam>();
            paramlst.Add(new SqlParam("@region_no", info.region_no));
            paramlst.Add(new SqlParam("@region_name", info.region_name));
            paramlst.Add(new SqlParam("@memo", info.memo));
            return DbContext.ExecuteBySql(insertSql, paramlst.ToArray()) > 0;
        }

        /// <summary>
        /// 修改准驾车型
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool EditRegion(t_bd_region info)
        {
            var updateSql = new StringBuilder();
            updateSql.Append("update t_bd_region set ");
            List<string> fields = new List<string>();
            if (!string.IsNullOrEmpty(info.region_name))
            {
                fields.Add(" region_name=@region_name");
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
            updateSql.Append(" where region_no=@region_no ");

            SqlParam[] paramlst = new SqlParam[] { 
              new SqlParam("@region_name",info.region_name),
              new SqlParam("@memo",info.memo),
              new SqlParam("@region_no",info.region_no)
            };

            return DbContext.ExecuteBySql(updateSql, paramlst) > 0;
        }
        /// <summary>
        /// 查找准驾车型
        /// </summary>
        /// <param name="region_no"></param>
        /// <returns></returns>
        public t_bd_region FindByRegionNo(string region_no)
        {
            var findSql = new StringBuilder();
            findSql.Append("select region_no,region_name,memo from t_bd_region where region_no=@region_no ");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@region_no",region_no)
            };
            var lst = DbContext.GetDataListBySQL<t_bd_region>(findSql, paramlst);
            if (lst.Count > 0)
            {
                return lst[0] as t_bd_region;
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
        public List<t_bd_region> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select region_no,region_name,memo from t_bd_region where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "region_no", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_bd_region>(dt) as List<t_bd_region>;
        }

        public bool BatchDeleteRegion(List<string> pkValues)
        {
            var tableName = "t_bd_region";
            var pkName = "region_no";
            return DbContext.BatchDeleteData(tableName, pkName, pkValues.ToArray()) > 0;
        }
    }
}
