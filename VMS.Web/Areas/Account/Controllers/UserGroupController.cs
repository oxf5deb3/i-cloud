using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.Account.Controllers
{
    public class UserGroupController : Controller
    {
        //
        // GET: /Account/UserGroup/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/UserGroup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Account/UserGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/UserGroup/Create
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
        // GET: /Account/UserGroup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Account/UserGroup/Edit/5
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
        // GET: /Account/UserGroup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Account/UserGroup/Delete/5
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
