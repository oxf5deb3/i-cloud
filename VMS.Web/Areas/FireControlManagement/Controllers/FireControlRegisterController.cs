using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.FireControlManagement.Controllers
{
    public class FireControlRegisterController : Controller
    {
        //
        // GET: /FireControlManagement/FireControlRegister/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FireControlManagement/FireControlRegister/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FireControlManagement/FireControlRegister/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireControlRegister/Create
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
        // GET: /FireControlManagement/FireControlRegister/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireControlRegister/Edit/5
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
        // GET: /FireControlManagement/FireControlRegister/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireControlRegister/Delete/5
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
