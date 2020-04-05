using System.Web.Mvc;
using VMS.Models;

namespace VMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionAttribute());
        }
    }
}
