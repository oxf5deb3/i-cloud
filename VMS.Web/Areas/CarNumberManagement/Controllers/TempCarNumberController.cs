using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.CarNumberManagement.Controllers
{
    public class TempCarNumberController : Controller
    {
        // GET: CarNumberManagement/TempCarNumber
        public ActionResult Index()
        {
            return View();
        }
    }
}