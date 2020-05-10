﻿using System;
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

        public bool SaveRoleRight(List<RoleRightDTO> data)
        {
            var sql = new StringBuilder();
            var lstParams = new List<SqlParam>();
            sql.Append("delete from t_sys_role_right where role_id=@role_id;");
            lstParams.Add(new SqlParam("@role_id", data[0].role_id));
            for(var i=0;i<data.Count;i++) {
                var roleid = "@role_id_" + i;
                var resid = "@res_id_" + i;
                var grantid = "@grant_id_" + i;
                sql.Append(" insert into t_sys_role_right(role_id,res_id,grant_id) values("+roleid+","+resid+","+grantid+")");
                lstParams.Add(new SqlParam(roleid,data[i].role_id));
                lstParams.Add(new SqlParam(resid, data[i].res_id));
                lstParams.Add(new SqlParam(grantid, data[i].grant_id));
            }
            return DbContext.ExecuteBySql(sql, lstParams.ToArray()) > 0;
        }

        public List<RoleRightDTO> GetRoleRightByRoleId(string role_id)
        {
            var sql = new StringBuilder();
            sql.Append("select role_id,res_id,grant_id from t_sys_role_right where role_id=@role_id");
            var lstParams = new List<SqlParam>();
            lstParams.Add(new SqlParam("@role_id",role_id));
            return DbContext.GetDataListBySQL<RoleRightDTO>(sql, lstParams.ToArray()) as List<RoleRightDTO>;
        }


        public List<RightMenuDTO> LoadMenu(string oper_id)
        {
            var sql = new StringBuilder();
            sql.Append(" select * from ( ");
            sql.Append(" select d.id,d.pid,d.level,d.res_uri,d.res_img,d.res_desc,d.sort_code ");
            sql.Append(" from t_sys_user a");
            sql.Append(" left join t_sys_oper_role b on a.user_id=b.user_id");
            sql.Append(" left join t_sys_role_right c on b.role_id=c.role_id");
            sql.Append(" left join t_sys_resource d on c.res_id = d.id");
            sql.Append(" left join t_sys_resource_type e on d.res_type_id=e.id");
            sql.Append(" where a.user_id=@oper_id and e.type_name='Menu'");
            sql.Append(" union ");
            sql.Append(" select i.id,i.pid,i.level,i.res_uri,i.res_img,i.res_desc,i.sort_code from t_sys_user_group f ");
            sql.Append(" left join t_sys_group_role g on f.group_id=g.group_id ");
            sql.Append(" left join t_sys_role_right h on g.role_id=h.role_id ");
            sql.Append(" left join t_sys_resource i on h.res_id = i.id ");
            sql.Append(" left join t_sys_resource_type j on i.res_type_id=j.id ");
            sql.Append(" where f.user_id=@oper_id and j.type_name='Menu' ");
            sql.Append(" union ");
            sql.Append(" select '9009' as id,'9000' as pid,'1' as level,'../SystemLog/SystemSetting/Index' as res_uri,'' as res_img,'系统设置' as res_desc,99 as sort_code");
            sql.Append(" where '1001' = @oper_id ");
            sql.Append(" ) as t order by id,level,sort_code ");
            var lstParams = new List<SqlParam>();
            lstParams.Add(new SqlParam("@oper_id", oper_id));
            return DbContext.GetDataListBySQL<RightMenuDTO>(sql, lstParams.ToArray()) as List<RightMenuDTO>;
        }
    }
}
