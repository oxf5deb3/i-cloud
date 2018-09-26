using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Controllers;


namespace VMS.Areas.FireControlManagement.Controllers
{
    public class FireEquipmentQueryController : BaseController
    {
        //
        // GET: /FireControlManagement/FireEquipmentQuery/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FireControlManagement/FireEquipmentQuery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FireControlManagement/FireEquipmentQuery/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireEquipmentQuery/Create
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
        // GET: /FireControlManagement/FireEquipmentQuery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireEquipmentQuery/Edit/5
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
        // GET: /FireControlManagement/FireEquipmentQuery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FireControlManagement/FireEquipmentQuery/Delete/5
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
