using System.Web.Mvc;

namespace VMS.Controllers
{
    public class LoginController : BaseController
    {
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
       
    }
}