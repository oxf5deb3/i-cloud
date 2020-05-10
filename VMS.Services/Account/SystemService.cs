using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Account;
using VMS.IServices;
using VMS.Model;
using VMS.Utils;

namespace VMS.Services
{
    public class SystemService : ServiceBase, ISystemService
    {
        public bool AddNews(string title, string content, string operId)
        {
            t_sys_news news = new t_sys_news();
            news.title = title;
            news.content = content;
            news.create_id = operId;
            news.create_date = DateTime.Now;
            return SqlSugarDbContext.t_sys_news.Insert(news);
        }
        public List<t_sys_news> GetTop10News()
        {
            return SqlSugarDbContext.t_sys_news.AsQueryable().OrderBy(e => e.id, OrderByType.Desc).Take(10).ToList();
        }
        public t_sys_news GetNewsById(string key)
        {
            decimal id = -1;
            Decimal.TryParse(key, out id);
            return SqlSugarDbContext.t_sys_news.AsQueryable().First(e => e.id == id);
        }
        public string GetContentById(decimal id)
        {
            var c = SqlSugarDbContext.t_sys_news.AsQueryable().First(e => e.id == id);
            if (c == null)
            {
                return "";
            }
            return c.content;
        }
        public bool DelNewsById(string key)
        {
            decimal id = -1;
            Decimal.TryParse(key, out id);
            return SqlSugarDbContext.t_sys_news.DeleteById(id);
        }
        public bool UpdateNewsById(string key, string title, string content, string operId)
        {
            decimal id = -1;
            Decimal.TryParse(key, out id);
            if (id <= 0) return false;
            t_sys_news news = new t_sys_news();
            news.id = id;
            news.title = title;
            news.content = content;
            news.create_id = operId;
            news.create_date = DateTime.Now;
            return SqlSugarDbContext.t_sys_news.AsUpdateable(news).ExecuteCommand()>0;
        }
        public List<t_sys_setting> QuerySetting(string key)
        {
            var set = SqlSugarDbContext.t_sys_setting.AsQueryable().Where(e => e.status == "0");
            if (!string.IsNullOrEmpty(key))
            {
                set = set.Where(e => e.sys_var_id == key);
            }
            var lst = set.ToList();
            lst.ForEach(e =>
            {
                if (e.sys_var_id == "email_pwd")
                {
                    e.sys_var_val = DESEncrypt.Decrypt(e.sys_var_val);
                }
            });
            return lst;
        }

        public bool SaveSetting(List<t_sys_setting> addsettings, List<t_sys_setting> upsettings)
        {
            try
            {
                if (upsettings != null && upsettings.Count > 0)
                {
                    upsettings.ForEach(e => {
                        e.modify_date = DateTime.Now;
                        if(e.sys_var_id== "email_pwd")
                        {
                            e.sys_var_val = DESEncrypt.Encrypt(e.sys_var_val);
                        }
                    });
                    SqlSugarDbContext.t_sys_setting.AsUpdateable(upsettings).UpdateColumns(e=>new {e.sys_var_val,e.modify_date}).ExecuteCommand();
                }
                if (addsettings != null && addsettings.Count > 0)
                {
                    addsettings.ForEach(e => {
                        e.status = "0";
                        e.create_date = DateTime.Now;
                        if (e.sys_var_id == "email_pwd")
                        {
                            e.sys_var_val = DESEncrypt.Encrypt(e.sys_var_val);
                        }
                    });
                    SqlSugarDbContext.t_sys_setting.InsertRange(addsettings);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SysNewsDTO> QueryPageNews(IDictionary<string, dynamic> conditions, string orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count, ref string err)
        {
            List<Expression<Func<SysNewsDTO, bool>>> wheres = new List<Expression<Func<SysNewsDTO, bool>>>();
            wheres.AddRange(CreateWhere(conditions));

            Expression<Func<SysNewsDTO, object>> orderbys = CreateOrderby(orderby);

            var q = SqlSugarDbContext.Db.Queryable<t_sys_news,t_sys_user>((r1, r2) => new object[] { JoinType.Left, r1.create_id == r2.user_id && r2.user_type == "0" });
   
            var lst = SqlSugarDbContext.GetPageList<t_sys_news, SysNewsDTO>(q, wheres, orderbys, isAsc, pageIndex, pageSize, ref count);

            var dtos = Convert2DTO(lst);

            return dtos;

        }
        public virtual List<Expression<Func<SysNewsDTO, bool>>> CreateWhere(IDictionary<string, dynamic> conditions)
        {
            var where = new List<Expression<Func<SysNewsDTO, bool>>>();
            var title = conditions["title"] != null ? (string)conditions["title"] : "";
            if (!string.IsNullOrEmpty(title))
            {
                where.Add(e => e.title.Contains(title));
            }
            return where;
        }
        public virtual Expression<Func<SysNewsDTO, object>> CreateOrderby(string orderby)
        {
            Expression<Func<SysNewsDTO, object>> by = null;
            switch (orderby)
            {
                case "id": by = o => o.id; break;
                case "title": by = o => o.title; break;
                case "create_date": by = o => new { o.create_date }; break;
                default: by = o => o.id; break;
            }
            return by;
        }
        public virtual List<SysNewsDTO> Convert2DTO(ISugarQueryable<SysNewsDTO> q)
        {
            var dtos = q.ToList();
            return dtos;
        }

       
    }
}
