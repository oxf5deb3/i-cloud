using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.IServices;
using VMS.Model;

namespace VMS.Services
{
    public class SystemService : ServiceBase, ISystemService
    {
        public List<t_sys_setting> QuerySetting(string key)
        {
            var set = SqlSugarDbContext.t_sys_setting.AsQueryable().Where(e => e.status == "0");
            if (!string.IsNullOrEmpty(key))
            {
                set = set.Where(e => e.sys_var_id == key);
            }
            return set.ToList();
        }

        public bool SaveSetting(List<t_sys_setting> settings)
        {
            try
            {
                if (settings.Count > 0)
                {
                    SqlSugarDbContext.t_sys_setting.InsertRange(settings);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
    }
}
