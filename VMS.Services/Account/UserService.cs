using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public t_sys_user FindByUserId(string user_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select id,user_id,user_pwd,user_name,sex,age,tel,email,status from t_sys_user where user_id=@user_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@user_id",user_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_user>(findSql, paramlst) as List<t_sys_user>;
            if (lst.Count > 0)
            {
                lst[0].user_pwd = DESEncrypt.Decrypt(lst[0].user_pwd);
                return lst[0] as t_sys_user;
            }
            return null;
        }
        
        public List<t_sys_oper_role> FindOperRoleByUserId(string user_id)
        {
            var findSql = new StringBuilder();
            findSql.Append("select a.id,a.user_id,a.role_id,isnull(b.role_name,'') as role_name from t_sys_oper_role a left join t_sys_role b on a.role_id=b.id  where a.user_id=@user_id");
            SqlParam[] paramlst = new SqlParam[] { 
               new SqlParam("@user_id",user_id)
            };
            var lst = DbContext.GetDataListBySQL<t_sys_oper_role>(findSql, paramlst);
            return lst as List<t_sys_oper_role>;
        }

        public bool BatchDeleteUser(List<string> pkValues)
        {
            List<StringBuilder> sqls = new List<StringBuilder>();
            List<SqlParam[]> lstParams = new List<SqlParam[]>();
            var delUserSql = new StringBuilder();
            delUserSql.Append("delete from t_sys_user where user_id in (@user_id)");
            lstParams.Add(new SqlParam[] { new SqlParam("@user_id", string.Join("','", pkValues.ToArray())) });

            var delOperUserSql = new StringBuilder();
            delOperUserSql.Append("delete from t_sys_oper_role where user_id in(@oper_user)");
            lstParams.Add(new SqlParam[] { new SqlParam("@oper_user", string.Join("','", pkValues.ToArray())) });

            sqls.Add(delUserSql);
            sqls.Add(delOperUserSql);

            var count = DbContext.BatchExecuteBySql(sqls.ToArray(), lstParams.ToArray());
            return count >= 0;
        }
        public bool EditUser(UserRoleDTO user)
        {
            var allSql = new List<StringBuilder>();
            var lstParams = new List<SqlParam[]>();

            var updateSql = new StringBuilder();
            updateSql.Append("update t_sys_user set user_pwd=@user_pwd,user_name=@user_name,sex=@sex,age=@age,tel=@tel,email=@email,status=@status where id=@id ");
            SqlParam[] params0 = new SqlParam[]{
               new SqlParam("@id",user.id),
               new SqlParam("@user_pwd",DESEncrypt.Encrypt(user.user_pwd)),
               new SqlParam("@user_name",user.user_name),
               new SqlParam("@sex",user.sex),
               new SqlParam("@age",user.age),
               new SqlParam("@tel",user.tel),
               new SqlParam("@email",user.email),
               new SqlParam("@status",user.status)
            };
            allSql.Add(updateSql);
            lstParams.Add(params0);
            var delSql = new StringBuilder();
            delSql.Append("delete from t_sys_oper_role where user_id=@user_id ");
            SqlParam[] params1 = new SqlParam[]{
              new SqlParam("@user_id",user.user_id)
            };
            allSql.Add(delSql);
            lstParams.Add(params1);

            for(var i=0;i<user.roles.Count;i++){
                var sb = new StringBuilder();
                var userid = "@user_id_"+i;
                var roleid = "@role_id_"+i;
                sb.Append("insert into t_sys_oper_role(user_id,role_id) values(" + userid + "," + roleid + ")");
                allSql.Add(sb);
                lstParams.Add(new SqlParam[]{new SqlParam(userid,user.user_id),new SqlParam(roleid,user.roles[i].id)});
            }

           return DbContext.BatchExecuteBySql(allSql.ToArray(),lstParams.ToArray())>0;
        }
        public bool AddUser(UserRoleDTO user)
        {
            using (DbConnection connection = DbContext.GetDatabase().CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    decimal num1 = 0;
                    var insertRoleSql = new StringBuilder();
                    insertRoleSql.Append("insert into t_sys_user(user_id,user_pwd,user_name,sex,age,tel,email,status,last_login_time,create_date) values(@user_id,@user_pwd,@user_name,@sex,@age,@tel,@email,@status,null,getdate())");
                    user.user_pwd = DESEncrypt.Encrypt(user.user_pwd);
                    SqlParam[] param = new SqlParam[]{
                           new SqlParam("@user_id",user.user_id),
                           new SqlParam("@user_pwd",user.user_pwd),
                           new SqlParam("@user_name",user.user_name),
                           new SqlParam("@sex",user.sex),
                           new SqlParam("@age",user.age),
                           new SqlParam("@tel",user.tel),
                           new SqlParam("@email",user.email),
                           new SqlParam("@status",user.status)
                   };

                    DbCommand sqlStringCommand = connection.CreateCommand();
                    sqlStringCommand.CommandText = insertRoleSql.ToString();
                    sqlStringCommand.Transaction = transaction;
                    foreach (var p in param)
                    {
                        DbContext.GetDatabase().AddInParameter(sqlStringCommand, p.FieldName, p.DataType, p.FieldValue);
                    }
                    num1 = sqlStringCommand.ExecuteNonQuery();
                    List<SqlParam[]> lstParam = new List<SqlParam[]>();
                    List<StringBuilder> lstSql = new List<StringBuilder>();
                    var num = 0;
                    for (var i = 0; i < user.roles.Count; i++)
                    {
                        var insertRoleUserSql = new StringBuilder();
                        var roleid = "@role_id_" + i;
                        var userid = "@user_id_" + i;
                        insertRoleUserSql.Append("insert into t_sys_oper_role(role_id,user_id) values(" + roleid + "," + userid + ")");
                        lstParam.Add(new SqlParam[] { new SqlParam(roleid, user.roles[i].id), new SqlParam(userid, user.user_id) });
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
        public List<t_sys_user> GetAllUser(StringBuilder SqlWhere, IList<SqlParam> IList_param, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,user_id,user_pwd,user_name,sex,age,tel,email,status,last_login_time,create_date from t_sys_user where 1=1 ");
            sql.Append(SqlWhere);
            return DbContext.GetDataListBySQL<t_sys_user>(sql, IList_param.ToArray()) as List<t_sys_user>;
        }


        public List<t_sys_user> GetPageList(StringBuilder SqlWhere, IList<SqlParam> IList_param, int pageIndex, int pageSize, ref int count)
        {
            var sql = new StringBuilder();
            sql.Append("select id,user_id,user_pwd,user_name,sex,age,tel,email,status,last_login_time,create_date from t_sys_user where 1=1 ");
            sql.Append(SqlWhere);
            var dt = DbContext.GetPageList(sql.ToString(), IList_param.ToArray(), "id", "desc", pageIndex, pageSize, ref count);
            return DataTableHelper.DataTableToIList<t_sys_user>(dt) as List<t_sys_user>;
        }
        #region sqlsugar

        /// <summary>
        /// 是否存在此用户id
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="isInnerUser"></param>
        /// <returns></returns>
        public bool IsExist(string user_id, bool isInnerUser = true)
        {
            var has = SqlSugarDbContext.t_sys_user.IsAny(e => e.user_id == user_id && ((isInnerUser==false && e.user_type == "1") || (isInnerUser==true && e.user_type == "0")));
            return has;
        }

        /// <summary>
        /// 添加外部用户人员
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddOuterUser(UserRoleDTO user)
        {
            t_sys_user u = new t_sys_user();
            u.user_id = user.user_id;
            u.user_name = user.user_name;
            u.user_pwd = user.user_pwd;
            u.age = user.age;
            u.sex = user.sex;
            u.tel = user.tel;
            u.user_type = "1";
            u.email = user.email;
            return SqlSugarDbContext.t_sys_user.Insert(u);
        }
        #endregion
    }
}
