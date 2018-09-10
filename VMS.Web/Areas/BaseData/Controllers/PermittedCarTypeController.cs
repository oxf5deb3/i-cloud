using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Http;

using VMS.DTO;
using VMS.Models;

namespace VMS.Areas.BaseData.Controllers
{
    public class PermittedCarTypeController : Controller
    {
        //
        // GET: /BaseData/PermittedCarType/
        public ActionResult Index()
        {
            return View();
        }

      
	}
}