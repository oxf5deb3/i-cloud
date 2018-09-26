using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Controllers;


namespace VMS.Areas.DriverLicenseManagement.Controllers
{
    public class DriverLicenseController : BaseController
    {
        //
        // GET: /DriverLicenseManagement/DriverLicense/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DriverLicenseManagement/DriverLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DriverLicenseManagement/DriverLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DriverLicenseManagement/DriverLicense/Create
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
        // GET: /DriverLicenseManagement/DriverLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DriverLicenseManagement/DriverLicense/Edit/5
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
        // GET: /DriverLicenseManagement/DriverLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DriverLicenseManagement/DriverLicense/Delete/5
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
