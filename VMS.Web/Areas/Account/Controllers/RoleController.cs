using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.Account.Controllers
{
    public class RoleController : Controller
    {
        //
        // GET: /Account/Role/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Account/Role/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Role/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Account/Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
