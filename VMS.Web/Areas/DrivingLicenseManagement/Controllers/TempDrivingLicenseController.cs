using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.DrivingLicenseManagement.Controllers
{
    public class TempDrivingLicenseController : Controller
    {
        //
        // GET: /DrivingLicenseManagement/TempDrivingLicense/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DrivingLicenseManagement/TempDrivingLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DrivingLicenseManagement/TempDrivingLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/TempDrivingLicense/Create
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
        // GET: /DrivingLicenseManagement/TempDrivingLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/TempDrivingLicense/Edit/5
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
        // GET: /DrivingLicenseManagement/TempDrivingLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/TempDrivingLicense/Delete/5
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
