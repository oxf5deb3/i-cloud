using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
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
        public t_sys_user FindByUserId(string user_id, string user_type = "0")
        {
            var user = SqlSugarDbContext.t_sys_user.GetSingle(e => e.user_id == user_id.Trim() && e.user_type == user_type);
            if (user != null)
            {
                user.user_pwd = DESEncrypt.Decrypt(user.user_pwd);
            }
            return user;
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

            for (var i = 0; i < user.roles.Count; i++)
            {
                var sb = new StringBuilder();
                var userid = "@user_id_" + i;
                var roleid = "@role_id_" + i;
                sb.Append("insert into t_sys_oper_role(user_id,role_id) values(" + userid + "," + roleid + ")");
                allSql.Add(sb);
                lstParams.Add(new SqlParam[] { new SqlParam(userid, user.user_id), new SqlParam(roleid, user.roles[i].id) });
            }

            return DbContext.BatchExecuteBySql(allSql.ToArray(), lstParams.ToArray()) > 0;
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
            var has = SqlSugarDbContext.t_sys_user.IsAny(e => e.user_id == user_id && ((isInnerUser == false && e.user_type == "1") || (isInnerUser == true && e.user_type == "0")));
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
        public bool EditOuterUser(UserRoleDTO user)
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
            return DbContext.BatchExecuteBySql(allSql.ToArray(), lstParams.ToArray()) > 0;
        }
        public List<t_sys_user> GetUserPageList(IDictionary<string, dynamic> conditions, string orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count, ref string err)
        {
            List<Expression<Func<t_sys_user, bool>>> wheres = new List<Expression<Func<t_sys_user, bool>>>();
            wheres.AddRange(UserCreateWhere(conditions));

            Expression<Func<t_sys_user, object>> orderbys = UserCreateOrderby(orderby);

            var q = SqlSugarDbContext.Db.Queryable<t_sys_user>();

            var lst = SqlSugarDbContext.GetPageList<t_sys_user, t_sys_user>(q, wheres, orderbys, isAsc, pageIndex, pageSize, ref count);

            var dtos = UserConvert2DTO(lst);

            return dtos;
        }
        public virtual List<Expression<Func<t_sys_user, bool>>> UserCreateWhere(IDictionary<string, dynamic> conditions)
        {
            var where = new List<Expression<Func<t_sys_user, bool>>>();
            var user_type = conditions["user_type"] != null ? (string)conditions["user_type"] : "";
            if (!string.IsNullOrEmpty(user_type))
            {
                where.Add(e => e.user_type == user_type);
            }
            return where;
        }
        public virtual Expression<Func<t_sys_user, object>> UserCreateOrderby(string orderby)
        {
            Expression<Func<t_sys_user, object>> by = null;
            switch (orderby)
            {
                case "user_id": by = o => o.user_id; break;
                case "user_name": by = o => o.user_name; break;
                case "sex": by = o => o.sex; break;
                case "age": by = o => o.age; break;
                case "tel": by = o => o.tel; break;
                case "email": by = o => o.email; break;
                case "status": by = o => o.status; break;
                case "last_login_time": by = o => new { o.last_login_time }; break;
                case "create_date": by = o => new { o.create_date }; break;
                default: by = o => o.id; break;
            }
            return by;
        }
        public virtual List<t_sys_user> UserConvert2DTO(ISugarQueryable<t_sys_user> q)
        {
            var dtos = q.ToList();
            return dtos;
        }

        //忘记密码
        public bool AddLostPwdRecord(string user_id, string email, string httpaddr)
        {
            var r = new t_sys_pwd_lostfind();
            r.user_id = user_id.Trim();
            if (string.IsNullOrEmpty(email))
            {
                var u = SqlSugarDbContext.t_sys_user.AsQueryable().Where(e => e.user_type == "1" && e.user_id == r.user_id).First();
                if (u != null && !string.IsNullOrEmpty(u.email))
                {
                    r.email = u.email;
                }
            }
            else
            {
                r.email = email;
            }
            r.guid = Guid.NewGuid().ToString().Replace("-", "");
            r.create_date = DateTime.Now;
            r.valid_date = r.create_date.AddHours(24);
            r.status = "0";

            var set = SqlSugarDbContext.t_sys_setting.AsQueryable().Where(e => e.sys_var_id == "email_server_addr" || e.sys_var_id == "email_account" || e.sys_var_id == "email_pwd").ToList();
            if (set.Count > 2)
            {
                var server = set.First(e => e.sys_var_id == "email_server_addr").sys_var_val;
                var account = set.First(e => e.sys_var_id == "email_account").sys_var_val;
                var pwd = set.First(e => e.sys_var_id == "email_pwd").sys_var_val;
                //var admin = SqlSugarDbContext.t_sys_user.AsQueryable().First(e => e.user_id == "1001");
                //r.email = "oxf5deb3@163.com";
                var subject = "佤邦司法委车管所-密码重置";
                var fromName = "系统管理员";
                var from = account;
                var body = "密码找回请点击此链接进行密码重置，地址: <a href=\"" + httpaddr+"/H5/pwdReset.html?id=" + r.guid + "\">密码重置</a>";

                var success = Utils.EmailHelper.Send(from, fromName, r.email, server, account, Utils.DESEncrypt.Decrypt(pwd), subject, body);
                if (success)
                {
                    SqlSugarDbContext.t_sys_pwd_lostfind.Insert(r);
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool ResetPwd(string newPwd,string guid ,ref string err)
        {
           var findOne = SqlSugarDbContext.t_sys_pwd_lostfind.AsQueryable().First(e => e.guid == guid &&e.status=="0" && e.valid_date >= DateTime.Now);
            if (findOne != null)
            {
                var userId = findOne.user_id;
                var user = SqlSugarDbContext.t_sys_user.AsQueryable().First(e => e.user_id == userId && e.user_type == "1" && e.status == "0");
                if (user != null)
                {
                    user.user_pwd = DESEncrypt.Encrypt(newPwd);
                    var count = SqlSugarDbContext.t_sys_user.AsUpdateable(user).ExecuteCommand();
                    if (count > 0)
                    {
                        findOne.status = "1";
                        findOne.modify_date = DateTime.Now;
                        var c = SqlSugarDbContext.t_sys_pwd_lostfind.AsUpdateable(findOne).ExecuteCommand();
                        if (c > 0)
                            return true;
                        else
                        {
                            err = "密码重置失败";
                            return false;
                        }
                    }
                    else
                    {
                        err = "密码重置失败";
                        return false;
                    }
                }
                else
                {
                    err = "系统无法查询到此用户！";
                    return false;
                }
            }
            else
            {
                err = "未找到有效的重置记录，请尝试重新发起忘记密码动作";
                return false;
            }
        }
        public bool BatchDeleteOuterUser(List<string> pkValues)
        {
            List<StringBuilder> sqls = new List<StringBuilder>();
            List<SqlParam[]> lstParams = new List<SqlParam[]>();
            var delUserSql = new StringBuilder();
            delUserSql.Append("delete from t_sys_user where id in (@user_id)");
            lstParams.Add(new SqlParam[] { new SqlParam("@user_id", string.Join("','", pkValues.ToArray())) });
            sqls.Add(delUserSql);
            var count = DbContext.BatchExecuteBySql(sqls.ToArray(), lstParams.ToArray());
            return count >= 0;
        }

       
        #endregion
    }
}
