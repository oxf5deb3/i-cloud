using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Controllers;


namespace VMS.Areas.Statistical.Controllers
{
    public class DrivingLicenseController : BaseController
    {
        //
        // GET: /RegisterCenter/DriverLicenseRegister/
        public ActionResult Index()
        {
            return View();
        }
	}
}