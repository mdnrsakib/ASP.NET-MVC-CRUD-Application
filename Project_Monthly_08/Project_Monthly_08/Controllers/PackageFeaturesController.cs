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
    public class PackageFeaturesController : Controller
    {
        TravelTourDbContext db = new TravelTourDbContext();
        // GET: Qualifications
        public ActionResult Index()
        {
            return View(db.PackageFeatures.Include(x=>x.TourPackage).ToList());
        }
        public ActionResult Create()
        {

            return View();
        }
        public PartialViewResult CreateFeature()
        {
            ViewBag.TourPackage = db.TourPackages.ToList();
            return PartialView("_CreateFeature");
        }
        [HttpPost]
        public PartialViewResult CreateFeature(PackageFeature f)
        {
            Thread.Sleep(3000);

            if (ModelState.IsValid)
            {
                db.PackageFeatures.Add(f);
                db.SaveChanges();
                return PartialView("_Success");
            }
            ViewBag.TourPackage = db.TourPackages.ToList();
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditFeature(int id)
        {
            ViewBag.TourPackage = db.TourPackages.ToList();
            var p = db.PackageFeatures.First(x => x.PackageFeatureId == id);
            return PartialView("_EditFeature", p);
        }
        [HttpPost]
        public PartialViewResult EditFeature(PackageFeature f)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            ViewBag.TourPackage = db.TourPackages.ToList();
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.PackageFeatures.First(x => x.PackageFeatureId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            PackageFeature t = new PackageFeature { PackageFeatureId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
