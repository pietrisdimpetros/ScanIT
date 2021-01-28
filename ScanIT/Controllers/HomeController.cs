using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScanIT.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult About_Will()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About_Petros()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult About_Timos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult PayMe()
        {
            return View();
        }
        public ActionResult CookMe()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult ReTurnMe()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult UnderConstruction()
        {
            ViewBag.Message = "This Page is Under Construction";

            return View();
        }
    }
}