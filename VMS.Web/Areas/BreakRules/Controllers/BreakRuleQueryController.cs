using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Controllers;

namespace VMS.Areas.BreakRules.Controllers
{
    public class BreakRuleQueryController : BaseController
    {
        // GET: BreakRules/BreakRuleQuery
        public ActionResult Index()
        {
            return View();
        }
    }
}