using NAJB_BS_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAJB_BS_Final.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Searcher") && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Searcher", "Searcher");
            }
            else if (User.IsInRole("Employer") && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Employer", "Employer");
            }
            return View();
        }

        public ActionResult Searcher()
        {
            return View();
        }

        public ActionResult Employer()
        {
            return View();
        }

        [HandleError]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HandleError]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Job(int id)
        {
            SearcherRepo repo = new SearcherRepo();
            return View(repo.GetJob(id));
        }

        [HandleError]
        public ActionResult Company(int id)
        {
            SearcherRepo viewUserRepo = new SearcherRepo();
            return View(viewUserRepo.GetCompanyDetails(id));
        }
    }
}