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
    public class TravelAgentsController : Controller
    {
        TravelTourDbContext db = new TravelTourDbContext();
        // GET: Qualifications
        public ActionResult Index()
        {
            return View(db.TravelAgents.ToList());
        }
        public ActionResult Create()
        {

            return View();
        }
        public PartialViewResult CreateTravelAgent()
        {
            return PartialView("_CreateTravelAgent");
        }
        [HttpPost]
        public PartialViewResult CreateTravelAgent(TravelAgent q)
        {
            Thread.Sleep(4000);

            if (ModelState.IsValid)
            {
                db.TravelAgents.Add(q);
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
        public PartialViewResult EditTravelAgent(int id)
        {
            var p = db.TravelAgents.First(x => x.TravelAgentId == id);
            return PartialView("_EditTravelAgent", p);
        }
        [HttpPost]
        public PartialViewResult EditTravelAgent(TravelAgent t)
        {
            Thread.Sleep(2000);
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
            return View(db.TravelAgents.First(x => x.TravelAgentId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            TravelAgent t = new TravelAgent { TravelAgentId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}