using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Model;

namespace VMS.IServices
{
    public interface ISystemService : IService
    {
        List<t_sys_setting> QuerySetting(string key);
        bool SaveSetting(List<t_sys_setting> settings);
    }
}
