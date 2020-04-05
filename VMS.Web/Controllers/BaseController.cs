using System.Web.Mvc;
using VMS.DTO;
using VMS.Models;

namespace VMS.Controllers
{
    [VMSAuthorizeCore]
    public class BaseController : Controller
    {
        protected LoginDTO operInfo = GlobalVar.get();
	}
}