using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.SystemLog.Controllers
{
    public class SysNewsController : Controller
    {
        // GET: SystemLog/SysNews
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditNews()
        {
            return View("EditNews");
        }
    }
}