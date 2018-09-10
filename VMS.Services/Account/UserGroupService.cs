using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class UserGroupService : ServiceBase, IUserGroupService
    {
        public bool IsExist(string group_name)
        {
            var tableName = "t_sys_group";
            var pkName = "group_name";
            var pkVal = group_name;
            return DbContext.IsExist(tableName, pkName, pkVal) > 0;
        }

        public List<Model.t_sys_group_role> FindGroupRoleByGroupId(string group_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select a.id,a.group_id,a.role_id,isnull(b.role_name,'') as role_name from t_sys_group_role a left join t_sys_role b on a.role_id=b.id  where a.group_id=@group_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@group_id",group_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_group_role>(findSql, paramlst);
            return lst as List<t_sys_group_role>;
        }

        public List<Model.t_sys_user_group> FindGroupUserByGroupId(string group_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select a.id,a.group_id,a.user_id,isnull(b.user_name,'') as user_name from t_sys_user_group a left join t_sys_user b on a.user_id=b.user_id  where a.group_id=@group_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@group_id",group_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_user_group>(findSql, paramlst);
            return lst as List<t_sys_user_group>;
        }

        public bool BatchDeleteGroup(List<string> pkValues)
        {
            List<StringBuilder> sqls = new List<StringBuilder>();
            List<SqlParam[]> lstParams = new List<SqlParam[]>();
            var delUserSql = new StringBuilder();
            delUserSql.Append("delete from t_sys_group where id in (@group_id)");
            lstParams.Add(new SqlParam[] { new SqlParam("@group_id", string.Join("','", pkValues.ToArray())) });

            var delOperUserSql = new StringBuilder();
            delOperUserSql.Append("delete from t_sys_group_role where group_id in(@group_role)");
            lstParams.Add(new SqlParam[] { new SqlParam("@group_role", string.Join("','", pkValues.ToArray())) });
            
            var delGroupUserSql = new StringBuilder();
            delGroupUserSql.Append("delete from t_sys_user_group where group_id in(@group_role)");
            lstParams.Add(new SqlParam[] { new SqlParam("@group_role", string.Join("','", pkValues.ToArray())) });

            sqls.Add(delUserSql);
            sqls.Add(delOperUserSql);
            sqls.Add(delGroupUserSql);

            var count = DbContext.BatchExecuteBySql(sqls.ToArray(), lstParams.ToArray());
            return count >= 0;
        }

        public bool AddGroup(DTO.GroupRoleDTO group)
        {
            using (DbConnection connection = DbContext.GetDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    decimal num1 = 0;
                    var insertRoleSql = new StringBuilder();
                    insertRoleSql.Append("Declare @ReturnValue int insert into t_sys_group(group_name,create_id,create_date,status,memo) values(@group_name,@create_id,getdate(),@status,@memo)  Set @ReturnValue=SCOPE_IDENTITY() Select @ReturnValue");
                    SqlParam[] param = new SqlParam[]{
                           new SqlParam("@group_name",group.group_name),
                           new SqlParam("@create_id",group.create_id),
                           new SqlParam("@status",group.status),
                           new SqlParam("@memo",group.memo),
                   };

                    DbCommand sqlStringCommand = connection.CreateCommand();
                    sqlStringCommand.CommandText = insertRoleSql.ToString();
                    sqlStringCommand.Transaction = transaction;
                    foreach (var p in param)
                    {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    object result = sqlStringCommand.ExecuteScalar();
                    Decimal.TryParse(result.ToString() ?? "", out num1);
                    List<SqlParam[]> lstParam = new List<SqlParam[]>();
                    List<StringBuilder> lstSql = new List<StringBuilder>();
                    var num = 0;
                    for (var i = 0; i < group.roles.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var groupid = "@group_id_" + i;
                        var createid = "@create_id_" + i;
                        insertRoleUserSql.Append("insert into t_sys_group_role(role_id,group_id,create_id,create_date) values(" + roleid + "," + groupid + "," + createid + ",getdate())");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, group.roles[i].id), new SqlParam(groupid, num1), new SqlParam(createid, group.create_id) });
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

                    //组用户
                    for (var i = 0; i < group.users.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var userid = "@user_id_" + i;
                        var groupid = "@group_id_" + i;
                        var createid = "@create_id_" + i;
                        insertRoleUserSql.Append("insert into t_sys_user_group(user_id,group_id,create_id,create_date) values(" + userid + "," + groupid + "," + createid + ",getdate())");
                        lstParam.Add(new SqlParam[] { new SqlParam(userid, group.users[i].user_id), new SqlParam(groupid, num1), new SqlParam(createid, group.create_id) });
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

        public bool EditGroup(DTO.GroupRoleDTO group)
        {
            var allSql = new List<StringBuilder>();
            var lstParams = new List<SqlParam[]>();

            var updateSql = new StringBuilder();
            updateSql.Append("update t_sys_group set group_name=@group_name,memo=@memo,status=@status where id=@id ");
            SqlParam[] params0 = new SqlParam[]{
               new SqlParam("@id",group.id),
               new SqlParam("@group_name",group.group_name),
               new SqlParam("@memo",group.memo),
               new SqlParam("@status",group.status),
            };
            allSql.Add(updateSql);
            lstParams.Add(params0);
            var delSql = new StringBuilder();
            delSql.Append("delete from t_sys_group_role where group_id=@group_id ");
            SqlParam[] params1 = new SqlParam[]{
              new SqlParam("@group_id",group.id)
            };
            allSql.Add(delSql);
            lstParams.Add(params1);

            var delGroupUserSql = new StringBuilder();
            delGroupUserSql.Append("delete from t_sys_user_group where group_id=@group_id ");
            SqlParam[] params2 = new SqlParam[]{
              new SqlParam("@group_id",group.id)
            };
            allSql.Add(delGroupUserSql);
            lstParams.Add(params2);

            for (var i = 0; i < group.roles.Count; i++)
            {
                var sb = new StringBuilder();
                var groupid = "@group_id_" + i;
                var roleid = "@role_id_" + i;
                var createid = "@create_id_" + i;
                sb.Append("insert into t_sys_group_role(group_id,role_id,create_id,create_date) values(" + groupid + "," + roleid + ","+createid+",getdate())");
                allSql.Add(sb);
                lstParams.Add(new SqlParam[] { new SqlParam(groupid, group.id), new SqlParam(roleid, group.roles[i].id), new SqlParam(createid, group.create_id) });
            }
            for (var i = 0; i < group.users.Count; i++)
            {
                var sb = new StringBuilder();
                var groupid = "@group_id_" + i;
                var userid = "@user_id_" + i;
                var createid = "@create_id_" + i;
                sb.Append("insert into t_sys_user_group(user_id,group_id,create_id,create_date) values(" + userid + "," + groupid + "," + createid + ",getdate())");
                allSql.Add(sb);
                lstParams.Add(new SqlParam[] { new SqlParam(groupid, group.id), new SqlParam(userid, group.users[i].user_id), new SqlParam(createid, group.create_id) });
            }

            return DbContext.BatchExecuteBySql(allSql.ToArray(), lstParams.ToArray()) > 0;
        }

        public List<Model.t_sys_group> GetAllGroup(StringBuilder SqlWhere, IList<Model.SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,group_name,create_id,create_date,status,memo from t_sys_group where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_sys_group>(sql, IList_param.ToArray()) as List<t_sys_group>;
        }

        public List<Model.t_sys_group> GetPageList(StringBuilder SqlWhere, IList<Model.SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,group_name,create_id,create_date,status,memo from t_sys_group where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_sys_group>(dt) as List<t_sys_group>;
        }
    }
}
