using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Controllers;

namespace VMS.Areas.DetentionManagement.Controllers
{
    public class DetentionController : BaseController
    {
        //
        // GET: /DetentionManagement/Detention/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DetentionManagement/DriverLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DetentionManagement/DriverLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DetentionManagement/DriverLicense/Create
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
        // GET: /DetentionManagement/DriverLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DetentionManagement/DriverLicense/Edit/5
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
        // GET: /DetentionManagement/DriverLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DetentionManagement/DriverLicense/Delete/5
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