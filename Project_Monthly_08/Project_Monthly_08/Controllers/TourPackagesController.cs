using Project_Monthly_08.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Project_Monthly_08.Controllers
{
    [Authorize]
    public class TourPackagesController : Controller
    {
        TravelTourDbContext db = new TravelTourDbContext();
        // GET: Batches
        public ActionResult Index()
        {
            return View(db.TourPackages.Include(b => b.PackageFeatures).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateTourPackage()
        {
            return PartialView("_CreateTourPackage");
        }
        [HttpPost]
        public PartialViewResult CreateTourPackage(TourPackage t)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.TourPackages.Add(t);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditTourPackage(int id)
        {
            var b = db.TourPackages.First(x => x.TourPackageId == id);
            return PartialView("_EditTourPackage", b);
        }
        [HttpPost]
        public PartialViewResult EditTourPackage(TourPackage t)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.TourPackages.First(x => x.TourPackageId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int TourPackageId)
        {
            var b = new TourPackage { TourPackageId = TourPackageId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}