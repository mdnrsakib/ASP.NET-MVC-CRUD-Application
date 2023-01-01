using Project_Monthly_08.Models;
using Project_Monthly_08.TouristsViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace Project_Monthly_08.Controllers
{
    [Authorize]
    public class TouristsController : Controller
    {
        // GET: Tourists
        TravelTourDbContext db = new TravelTourDbContext();
        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Tourists.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.TourPackage = db.TourPackages.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(InputModel t)
        {
            if (ModelState.IsValid)
            {
                var tourist = new Tourist
                {
                    TouristName = t.TouristName,
                    BookingDate = t.BookingDate,
                    TouristOccupation = t.TouristOccupation,
                    TourPackageId = t.TourPackageId
                };
                string ext = Path.GetExtension(t.TouristPicture.FileName);
                string f = Guid.NewGuid() + ext;
                t.TouristPicture.SaveAs(Server.MapPath("~/Assets/") + f);
                tourist.TouristPicture = f;
                db.Tourists.Add(tourist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourPackage = db.TourPackages.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var e = db.Tourists.First(x => x.TouristId == id);
            ViewBag.TourPackage = db.TourPackages.ToList();
            ViewBag.CurrentPic = e.TouristPicture;
            return View(new EditModel { TouristId = e.TouristId, TouristName = e.TouristName, BookingDate = e.BookingDate, TouristOccupation = e.TouristOccupation, TourPackageId=e.TourPackageId });
        }
        [HttpPost]
        public ActionResult Edit(EditModel e)
        {
            var t = db.Tourists.First(x => x.TouristId == e.TouristId);
            if (ModelState.IsValid)
            {
                t.TouristName = e.TouristName;
                t.BookingDate = e.BookingDate;
                t.TouristOccupation = e.TouristOccupation;
                if (e.TouristPicture != null)
                {
                    string ext = Path.GetExtension(e.TouristPicture.FileName);
                    string f = Guid.NewGuid() + ext;
                    e.TouristPicture.SaveAs(Server.MapPath("~/Assets/") + f);
                    t.TouristPicture = f;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TourPackage = db.TourPackages.ToList();
            ViewBag.CurrentPic = t.TouristPicture;
            return View(e);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Tourists.Include(x => x.TourPackage).First(x => x.TouristId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Tourist e = new Tourist { TouristId = id };
            db.Entry(e).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}