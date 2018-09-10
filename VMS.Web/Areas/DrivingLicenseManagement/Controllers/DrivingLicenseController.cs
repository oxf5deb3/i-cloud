using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VMS.Areas.DrivingLicenseManagement.Controllers
{
    public class DrivingLicenseController : Controller
    {
        //
        // GET: /DrivingLicenseManagement/DrivingLicense/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DrivingLicenseManagement/DrivingLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DrivingLicenseManagement/DrivingLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/DrivingLicense/Create
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
        // GET: /DrivingLicenseManagement/DrivingLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/DrivingLicense/Edit/5
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
        // GET: /DrivingLicenseManagement/DrivingLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DrivingLicenseManagement/DrivingLicense/Delete/5
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
