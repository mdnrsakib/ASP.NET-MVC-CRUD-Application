using Project_Monthly_08.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project_Monthly_08.Controllers
{
    [Authorize]
    public class AgentTourPackageController : Controller
    {
        private readonly TravelTourDbContext db = new TravelTourDbContext();
        // GET: TestEntries
        public ActionResult Index()
        {
            var data = db.TravelAgents
                .Include(x => x.AgentTourPackages.Select(y => y.TourPackage))
                .ToList();

            return View(data);
        }
        public ActionResult CreateAgentTourPackage()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateAgentTourPackage(TravelAgent c, int[] TourPackageId)
            {
                if (ModelState.IsValid)
                {
                    foreach (var i in TourPackageId)
                    {
                        c.AgentTourPackages.Add(new AgentTourPackage { TourPackageId = i });
                    }
                    db.TravelAgents.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(c);
            }
            public PartialViewResult CreatePackageEntry()
            {
                ViewBag.TourPackages = db.TourPackages.ToList();
                return PartialView("_PackageEntry");
            }
        }
    }
