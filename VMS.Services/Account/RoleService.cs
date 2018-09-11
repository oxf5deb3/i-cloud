using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;
using VMS.DTO;
using System.Data.Common;
using System.Data;

namespace VMS.Services
{
    public class RoleService : ServiceBase, IRoleService
    {
        public List<t_sys_user> FindRoleUserById(string role_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select a.user_id,a.role_id,isnull(b.user_name,'') as user_name,b.id from t_sys_oper_role a left join t_sys_user b on a.user_id=b.user_id  where a.role_id=@role_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@role_id",role_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_user>(findSql, paramlst);
            return lst as List<t_sys_user>;
        }

        public List<t_sys_group> FindRoleGroupById(string role_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select a.group_id,a.role_id,isnull(b.group_name,'') as group_name,b.id from t_sys_group_role a left join t_sys_group b on a.group_id=b.id  where a.role_id=@role_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@role_id",role_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_group>(findSql, paramlst);
            return lst as List<t_sys_group>;
        }
        public bool BatchDeleteRole(List<string> pkValues)
        {
            List<StringBuilder> sqls = new List<StringBuilder>();
            List<SqlParam[]> lstParams = new List<SqlParam[]>();
            var delUserSql = new StringBuilder();
            delUserSql.Append("delete from t_sys_role where id in (@role_id)");
            lstParams.Add(new SqlParam[] { new SqlParam("@role_id", string.Join("','", pkValues.ToArray())) });

            var delOperUserSql = new StringBuilder();
            delOperUserSql.Append("delete from t_sys_oper_role where role_id in(@oper_role)");
            lstParams.Add(new SqlParam[] { new SqlParam("@oper_role", string.Join("','", pkValues.ToArray())) });

            var delRoleGroupSql = new StringBuilder();
            delRoleGroupSql.Append("delete from t_sys_group_role where role_id in(@group_role)");
            lstParams.Add(new SqlParam[] { new SqlParam("@group_role", string.Join("','", pkValues.ToArray())) });

            sqls.Add(delUserSql);
            sqls.Add(delOperUserSql);
            sqls.Add(delRoleGroupSql);

            var count = DbContext.BatchExecuteBySql(sqls.ToArray(), lstParams.ToArray());
            return count >= 0;
        }
        public List<t_sys_role> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,role_name,status,memo from t_sys_role where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_sys_role>(dt) as List<t_sys_role>;
        }

        public bool AddRole(RoleUserGroupDTO role)
        {
            using (DbConnection connection = DbContext.GetDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    decimal num1 = 0;
                    var insertRoleSql = new StringBuilder();
                    insertRoleSql.Append("Declare @ReturnValue int insert into t_sys_role(role_name,create_id,create_date,status,memo) values(@role_name,@create_id,@create_date,@status,@memo) Set @ReturnValue=SCOPE_IDENTITY() Select @ReturnValue");

                    SqlParam[] param = new SqlParam[]{
                           new SqlParam("@role_name",DbType.String,role.role_name),
                           new SqlParam("@create_id",DbType.String,role.create_id),
                           new SqlParam("@create_date",DbType.DateTime,role.create_date),
                           new SqlParam("@status",DbType.String,role.status),
                           new SqlParam("@memo",DbType.String,role.memo)
                   };

                    DbCommand sqlStringCommand = connection.CreateCommand();
                    sqlStringCommand.CommandText = insertRoleSql.ToString();
                    sqlStringCommand.Transaction = transaction;
                    foreach (var p in param) {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    object result = sqlStringCommand.ExecuteScalar();
                    Decimal.TryParse(result.ToString()??"", out num1);
                    List<SqlParam[]> lstParam = new List<SqlParam[]>();
                    List<StringBuilder> lstSql = new List<StringBuilder>();
                    var num = 0;
                    for (var i = 0; i < role.users.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var userid = "@user_id_" + i;
                        insertRoleUserSql.Append("insert into t_sys_oper_role(role_id,user_id) values(" + roleid + "," + userid + ")");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, result), new SqlParam(userid, role.users[i].user_id) });
                        lstSql.Add(insertRoleUserSql);
                        if (i % 20 == 0) {
                            var sql = new StringBuilder();
                            lstSql.ForEach(e => {
                                sql.Append(e.ToString());
                            });
                            sqlStringCommand.CommandText = sql.ToString();
                            foreach (var list in lstParam)
                            {
                                foreach (var p in list)
                                {
                                    DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                                }
                            }
                            num += sqlStringCommand.ExecuteNonQuery();
                            lstParam.Clear();
                            lstSql.Clear();
                            sqlStringCommand.Parameters.Clear();
                        }
                    }
                    if (lstParam.Count > 0) {
                        var sql = new StringBuilder();
                        lstSql.ForEach(e =>
                        {
                            sql.Append(e.ToString());
                        });
                        sqlStringCommand.CommandText = sql.ToString();
                        foreach (var list in lstParam)
                        {
                            foreach (var p in list)
                            {
                                DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                            }
                        }
                        num += sqlStringCommand.ExecuteNonQuery();
                        lstParam.Clear();
                        lstSql.Clear();
                        sqlStringCommand.Parameters.Clear();
                    }
                    //角色组
                    for (var i = 0; i < role.groups.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var groupid = "@group_id_" + i;
                        var createid = "@create_id" + i;
                        insertRoleUserSql.Append("insert into t_sys_group_role(role_id,group_id,create_id,create_date) values(" + roleid + "," + groupid + "," + createid + ",getdate())");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, result), new SqlParam(groupid, role.groups[i].id), new SqlParam(createid, role.create_id) });
                        lstSql.Add(insertRoleUserSql);
                        if (i % 20 == 0)
                        {
                            var sql = new StringBuilder();
                            lstSql.ForEach(e =>
                            {
                                sql.Append(e.ToString());
                            });
                            sqlStringCommand.CommandText = sql.ToString();
                            foreach (var list in lstParam)
                            {
                                foreach (var p in list)
                                {
                                    DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                                }
                            }
                            num += sqlStringCommand.ExecuteNonQuery();
                            lstParam.Clear();
                            lstSql.Clear();
                            sqlStringCommand.Parameters.Clear();
                        }
                    }
                    if (lstParam.Count > 0)
                    {
                        var sql = new StringBuilder();
                        lstSql.ForEach(e =>
                        {
                            sql.Append(e.ToString());
                        });
                        sqlStringCommand.CommandText = sql.ToString();
                        foreach (var list in lstParam)
                        {
                            foreach (var p in list)
                            {
                                DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                            }
                        }
                        num += sqlStringCommand.ExecuteNonQuery();
                        lstParam.Clear();
                        lstSql.Clear();
                        sqlStringCommand.Parameters.Clear();
                    }
                    transaction.Commit();
                    connection.Close();
                    return num>=0 && num1>0;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
        }

        public bool EditRole(RoleUserGroupDTO role)
        {
            using (DbConnection connection = DbContext.GetDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    decimal num1 = 0;
                    var insertRoleSql = new StringBuilder();
                    insertRoleSql.Append("update t_sys_role set role_name=@role_name,create_id=@create_id,status=@status,memo=@memo where id=@id");

                    SqlParam[] param = new SqlParam[]{
                           new SqlParam("@id",DbType.String,role.id),
                           new SqlParam("@role_name",DbType.String,role.role_name),
                           new SqlParam("@create_id",DbType.String,role.create_id),
                           new SqlParam("@status",DbType.String,role.status),
                           new SqlParam("@memo",DbType.String,role.memo)
                   };

                    DbCommand sqlStringCommand = connection.CreateCommand();
                    sqlStringCommand.CommandText = insertRoleSql.ToString();
                    sqlStringCommand.Transaction = transaction;
                    foreach (var p in param)
                    {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    num1 = sqlStringCommand.ExecuteNonQuery();


                    sqlStringCommand.CommandText = "delete from t_sys_oper_role where role_id=@oper_role_id ";
                    sqlStringCommand.Transaction = transaction;
                    SqlParam[] param1 = new SqlParam[]{
                           new SqlParam("@oper_role_id",DbType.String,role.id),
                   };
                    foreach (var p in param1)
                    {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    sqlStringCommand.ExecuteNonQuery();

                    sqlStringCommand.CommandText = "delete from t_sys_group_role where role_id=@group_role_id ";
                    sqlStringCommand.Transaction = transaction;
                    SqlParam[] param2 = new SqlParam[]{
                           new SqlParam("@group_role_id",DbType.String,role.id),
                   };
                    foreach (var p in param2)
                    {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    sqlStringCommand.ExecuteNonQuery();

                    List<SqlParam[]> lstParam = new List<SqlParam[]>();
                    List<StringBuilder> lstSql = new List<StringBuilder>();

                    var num = 0;
                    for (var i = 0; i < role.users.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var userid = "@user_id_" + i;
                        insertRoleUserSql.Append("insert into t_sys_oper_role(role_id,user_id) values(" + roleid + "," + userid + ")");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, role.id), new SqlParam(userid, role.users[i].user_id) });
                        lstSql.Add(insertRoleUserSql);
                        if (i % 20 == 0)
                        {
                            var sql = new StringBuilder();
                            lstSql.ForEach(e =>
                            {
                                sql.Append(e.ToString());
                            });
                            sqlStringCommand.CommandText = sql.ToString();
                            foreach (var list in lstParam)
                            {
                                foreach (var p in list)
                                {
                                    DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                                }
                            }
                            num += sqlStringCommand.ExecuteNonQuery();
                            lstParam.Clear();
                            lstSql.Clear();
                            sqlStringCommand.Parameters.Clear();
                        }
                    }
                    if (lstParam.Count > 0)
                    {
                        var sql = new StringBuilder();
                        lstSql.ForEach(e =>
                        {
                            sql.Append(e.ToString());
                        });
                        sqlStringCommand.CommandText = sql.ToString();
                        foreach (var list in lstParam)
                        {
                            foreach (var p in list)
                            {
                                DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                            }
                        }
                        num += sqlStringCommand.ExecuteNonQuery();
                        lstParam.Clear();
                        lstSql.Clear();
                        sqlStringCommand.Parameters.Clear();
                    }
                    //角色组
                    for (var i = 0; i < role.groups.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var groupid = "@group_id_" + i;
                        var createid = "@create_id" + i;
                        insertRoleUserSql.Append("insert into t_sys_group_role(role_id,group_id,create_id,create_date) values(" + roleid + "," + groupid + "," + createid + ",getdate())");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, role.id), new SqlParam(groupid, role.groups[i].id), new SqlParam(createid, role.create_id) });
                        lstSql.Add(insertRoleUserSql);
                        if (i % 20 == 0)
                        {
                            var sql = new StringBuilder();
                            lstSql.ForEach(e =>
                            {
                                sql.Append(e.ToString());
                            });
                            sqlStringCommand.CommandText = sql.ToString();
                            foreach (var list in lstParam)
                            {
                                foreach (var p in list)
                                {
                                    DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                                }
                            }
                            num += sqlStringCommand.ExecuteNonQuery();
                            lstParam.Clear();
                            lstSql.Clear();
                            sqlStringCommand.Parameters.Clear();
                        }
                    }
                    if (lstParam.Count > 0)
                    {
                        var sql = new StringBuilder();
                        lstSql.ForEach(e =>
                        {
                            sql.Append(e.ToString());
                        });
                        sqlStringCommand.CommandText = sql.ToString();
                        foreach (var list in lstParam)
                        {
                            foreach (var p in list)
                            {
                                DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                            }
                        }
                        num += sqlStringCommand.ExecuteNonQuery();
                        lstParam.Clear();
                        lstSql.Clear();
                        sqlStringCommand.Parameters.Clear();
                    }
                    transaction.Commit();
                    connection.Close();
                    return num >= 0 && num1 > 0;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw exception;
                }
            }
        }
        public t_sys_role FindByRoleName(string role_name)
        {
            var findSql = new StringBuilder();
            findSql.Append("select id,role_name from t_sys_role where role_name=@role_name");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@role_name",role_name)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_role>(findSql, paramlst);
            if (lst.Count > 0)
            {
                return lst[0] as t_sys_role;
            }
            return null;
        }
        public List<t_sys_role> GetAllRole(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,role_name,status from t_sys_role where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_sys_role>(sql, IList_param.ToArray()) as List<t_sys_role>;
        }

        public List<t_sys_resource> GetAllResource(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append(";with t as");
            sql.Append(" (select id,pid,level,res_desc,sort_code,res_type_id,res_type_oper_id from t_sys_resource where (pid is null or pid='')");
            sql.Append(" union all");
            sql.Append(" select r1.id,r1.pid,r1.level,r1.res_desc,r1.sort_code,r1.res_type_id,r1.res_type_oper_id from t_sys_resource");
            sql.Append(" r1 join t as r2 on r1.pid = r2.Id)");
            sql.Append(" select t.*,type.type_desc from t left join t_sys_resource_type type on t.res_type_id = type.id");
            sql.Append(SqlWhere);
            sql.Append(" order by t.Id,level");
            return DbContext.GetDataListBySQL<t_sys_resource>(sql, IList_param.ToArray()) as List<t_sys_resource>;
        }
    }
}
