using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using NAJB_BS_Final.Models;
using NAJB_BS_Final.ViewModels;
using static NAJB_BS_Final.Controllers.ManageController;

namespace NAJB_BS_Final.Controllers
{
    public class EmployerController : Controller
    {
        // GET: Employer

        public void getInfo()
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var user = HttpContext.User.Identity.Name;
            ViewBag.Postings = db.Jobs.Where(j => j.Company.NAJB_user.email == user && j.active == true).Count();
            ViewBag.JobMatches = db.JobMatches.Where(jm => jm.Company.NAJB_user.email == user).Count();
            ViewBag.Applications = db.JobMatches.Where(jm => jm.Company.NAJB_user.email == user && jm.applied == true).Count();

            ViewBag.FirstName = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().firstname;
            ViewBag.LastName = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().lastname;
            ViewBag.Email = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().email;
            ViewBag.Image = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().userImage;

            ViewBag.Company = db.Companies.Where(c => c.NAJB_user.email == user).FirstOrDefault();
           
            return;
        }

        [Authorize(Roles = "Employer")]
        public ActionResult About()
        {
            getInfo();
            return View();
        }

        [Authorize(Roles = "Employer")]
        public ActionResult AddCredits()
        {
            getInfo();
            return View();
        }


        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult AddJob()
        {
            getInfo();
            return View();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public ActionResult AddJob(AddJob job, string email, string FCKeditor1)
        {
            var user = HttpContext.User.Identity.Name;
            notajobb_devEntities db = new notajobb_devEntities();
            var company = db.Companies.Where(c => c.NAJB_user.email == user).FirstOrDefault();
            EmployerRepo repo = new EmployerRepo();

            if (company.credits >= 100)
            {
                repo.SaveJob(job, FCKeditor1);
                string successMessage = "You have successfully added a Job!";
                return RedirectToAction("Employer", "Employer", new { successMessage });
            }
            else
            {
                @ViewBag.ErrorMessage = "Please add credits in order to post a Job.";
                getInfo();
                return View();
            }
        }

        [Authorize(Roles = "Employer")]
        public ActionResult Employer(string successMessage)
        {
            var user = HttpContext.User.Identity.Name;
            notajobb_devEntities db = new notajobb_devEntities();
            var company = db.Companies.Where(c => c.NAJB_user.email == user).FirstOrDefault();
            EmployerRepo repo = new EmployerRepo();
            var userCompany = repo.GetEmployerJobs(user);

            if (company.name == null)
            {
                return RedirectToAction("AddCompany");
            }
            getInfo();
            @ViewBag.SuccessMessage = successMessage;
            return View(userCompany);
        }

        [Authorize(Roles = "Employer")]
        public ActionResult Archive()
        {
            var user = HttpContext.User.Identity.Name;
            EmployerRepo repo = new EmployerRepo();
            getInfo();
            return View(repo.GetArchive(user));
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult EditEmployer(ManageMessageId? message)
        {
            var user = HttpContext.User.Identity.Name;
            EmployerRepo repo = new EmployerRepo();
            getInfo();
            ViewBag.StatusMessage =
               message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
               : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
               : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
               : message == ManageMessageId.Error ? "An error has occurred."
               : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
               : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
               : "";
            return View(repo.GetEmployer(user));
        }


        [Authorize(Roles = "Employer")]
        [HttpPost]
        public ActionResult EditEmployer(EditUser company, string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var user = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();
            EmployerRepo repo = new EmployerRepo();
            company.UserID = user.ID;
            repo.UpdateEmployer(company);
            return RedirectToAction("Employer");
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult Company()
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var user = HttpContext.User.Identity.Name;
            var company = db.Companies.Where(c => c.NAJB_user.email == user).FirstOrDefault();
            getInfo();
            return View(company);
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult Job(string id)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var job = db.Jobs.Where(j => j.ID.ToString() == id).FirstOrDefault();
            getInfo();
            return View(job);
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult Matches()
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var user = HttpContext.User.Identity.Name;
            var jobMatches = db.JobMatches.Where(j => j.Company.NAJB_user.email == user).ToList();
            EmployerRepo repo = new EmployerRepo();       
            getInfo();
            return View(repo.GetEmployerMatches(user));
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult EditCompany()
        {
            var user = HttpContext.User.Identity.Name;
            EmployerRepo repo = new EmployerRepo();
            getInfo();
            return View(repo.GetCompany(user));
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public ActionResult EditCompany(EditCompany company, string email)
        {
            EmployerRepo repo = new EmployerRepo();
            repo.UpdateCompany(company);
            return RedirectToAction("Company", "Employer");
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult AddCompany()
        {
            getInfo();
            return View();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public ActionResult AddCompany(Company newCompany)
        {
            EmployerRepo repo = new EmployerRepo();
            repo.SaveNewCompany(newCompany);
            return RedirectToAction("Employer", "Employer");
        }

        [Authorize(Roles = "Employer")]
        public ActionResult ConfirmCredits(string credits)
        {
            var user = HttpContext.User.Identity.Name.ToString();
            EmployerRepo repo = new EmployerRepo();
            repo.AddCreditsTest(credits, user);
            return RedirectToAction("Employer");
        }

        [Authorize(Roles = "Employer")]
        public ActionResult DeleteJob(int id)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var record = db.Jobs.Where(j => j.ID == id).FirstOrDefault();
            if (record != null)
            {
                db.Jobs.Remove(record);
                db.SaveChanges();
            }
            return RedirectToAction("Employer");
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public ActionResult DuplicateJob(int id)
        {
            var user = HttpContext.User.Identity.Name;
            EmployerRepo repo = new EmployerRepo();
            var userCompany = repo.GetUserCompanyDuplicate(user, id);
            getInfo();
            return View(userCompany);
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public ActionResult DuplicateJob(AddJob job, string email)
        {
            var user = HttpContext.User.Identity.Name;
            var company = job.Company;
            EmployerRepo repo = new EmployerRepo();

            if (company.credits >= 100)
            {
                repo.SaveDuplicateJob(job);

                return RedirectToAction("Employer");
            }
            else
            {
                @ViewBag.ErrorMessage = "Please add credits in order to post a Job.";
                getInfo();
                return View(repo.GetUserCompany(user));
            }
        }
    }
}