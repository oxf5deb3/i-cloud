using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Controllers
{

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewData.Model = operInfo;
            return View();
        }
       
    }
}