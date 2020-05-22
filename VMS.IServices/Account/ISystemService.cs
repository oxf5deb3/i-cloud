using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.DTO.Account;
using VMS.Model;

namespace VMS.IServices
{
    public interface ISystemService : IService
    {
        List<t_sys_setting> QuerySetting(string key);
        bool SaveSetting(List<t_sys_setting> addsettings, List<t_sys_setting> upsettings);
        bool AddNews(string title, string content,string imgurl,string operId);
        t_sys_news GetNewsById(string key);
        bool DelNewsById(string key);
        bool UpdateNewsById(string key, string title, string content,string imgurl, string operId);
        List<SysNewsDTO> QueryPageNews(IDictionary<string, dynamic> conditions, string orderby, bool isAsc, int? pageIndex, int? pageSize, ref int count, ref string err);

        string GetContentById(decimal id);
        List<t_sys_news> GetTop10News();

    }
}
