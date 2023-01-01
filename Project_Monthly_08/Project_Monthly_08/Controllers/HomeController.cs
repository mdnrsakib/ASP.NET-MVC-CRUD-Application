using Project_Monthly_08.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Monthly_08.Controllers
{
    public class HomeController : Controller
    {
        TravelTourDbContext db = new TravelTourDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.TravelAgents.ToList();
            return View();
        }
    }
}