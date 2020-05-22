using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.DAL
{
    public class SqlSugarDbContext 
    {
        private static string _connStr = "";
        public static string ConnStr
        {
            get
            {
                if (string.IsNullOrEmpty(_connStr))
                {
                    _connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                }
                return _connStr;
            }
        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
        public SimpleClient<t_sys_user> t_sys_user { get { return new SimpleClient<t_sys_user>(Db); } }
        public SimpleClient<t_sys_setting> t_sys_setting { get { return new SimpleClient<t_sys_setting>(Db); } }
        public SimpleClient<t_sys_pwd_lostfind> t_sys_pwd_lostfind { get { return new SimpleClient<t_sys_pwd_lostfind>(Db); } }
        public SimpleClient<t_sys_news> t_sys_news { get { return new SimpleClient<t_sys_news>(Db); } }
        public SimpleClient<t_fire_equipment_register> t_fire_equipment_register { get { return new SimpleClient<t_fire_equipment_register>(Db); } }
        public SimpleClient<t_accident_records> t_accident_records { get { return new SimpleClient<t_accident_records>(Db); } }

        
        public SqlSugarDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConnStr,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

        }
       


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList<T>() where T : class, new()
        {
            return Db.GetSimpleClient<T>().GetList();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete<T>(dynamic id) where T : class, new()
        {
            return Db.GetSimpleClient<T>().Delete(id);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update<T>(T obj) where T : class, new()
        {
            return Db.GetSimpleClient<T>().Update(obj);
        }
        /// <summary>
        /// 分页 K:主表， U:用户DTO
        /// </summary>
        /// <param name="wheres"></param>
        /// <param name="orderby"></param>
        /// <param name="isAsc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public ISugarQueryable<U> GetPageList<K,U>(ISugarQueryable<K> q, List<Expression<Func<U, bool>>> wheres, Expression<Func<U, object>> orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count) where K:class,new() where U:class,new()
        {
            //var q = Db.Queryable<K>("master");
            var qq = q.Select<U>().MergeTable();
            foreach (Expression<Func<U, bool>> where in wheres)
            {
                qq.Where(where);
            }
            count = qq.Count();
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                var Skip = (pageIndex.Value - 1) * pageSize.Value;
                var Take = pageSize.Value;
                if (pageIndex * pageSize > count / 2)//页码大于一半用倒序
                {
                    if (isAsc)
                    {
                        qq.OrderBy(orderby, OrderByType.Desc);

                    }
                    else
                    {
                        qq.OrderBy(orderby);
                    }
                    var Mod = count % pageSize.Value;
                    var Page = (int)Math.Ceiling((Decimal)count / pageSize.Value);
                    if (pageIndex * pageSize >= count)
                    {
                        Skip = 0; Take = Mod == 0 ? pageSize.Value : Mod;
                    }
                    else
                    {
                        Skip = (Page - pageIndex.Value - 1) * pageSize.Value + Mod;
                    }
                }
                else
                {
                    if (isAsc)
                    {
                        qq.OrderBy(orderby, OrderByType.Asc);

                    }
                    else
                    {
                        qq.OrderBy(orderby, OrderByType.Desc);
                    }
                }
                qq.Skip(Skip).Take(Take);
            }
            return qq;
        }
    }
}
