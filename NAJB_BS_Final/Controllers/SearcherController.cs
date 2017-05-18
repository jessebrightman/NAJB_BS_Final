using NAJB_BS_Final.Models;
using NAJB_BS_Final.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static NAJB_BS_Final.Controllers.ManageController;

namespace NAJB_BS_Final.Controllers
{
    public class SearcherController : Controller
    {
        // GET: Searcher
        notajobb_devEntities db = new notajobb_devEntities();

        public void getInfo()
        {
            var user = HttpContext.User.Identity.Name;
            ViewBag.Searches = db.JobSearches.Where(j => j.NAJB_user.email == user).Count();
            ViewBag.Matches = db.JobMatches.Where(j => j.NAJB_user.email == user && j.ignore == false).Count();
            ViewBag.Applications = db.JobMatches.Where(j => j.NAJB_user.email == user && j.applied == true).Count();
            ViewBag.Favourites = db.JobMatches.Where(j => j.NAJB_user.email == user && j.favourite == true && j.ignore == false).Count();

            ViewBag.FirstName = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().firstname;
            ViewBag.LastName = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().lastname;
            ViewBag.Email = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().email;

            ViewBag.Image = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().userImage;
            ViewBag.Resume = db.NAJB_user.Where(u => u.email == user).FirstOrDefault().resume;
            ViewBag.Companies = db.Companies.Take(5).ToList();
        }
        [Authorize(Roles = "Searcher")]

        public ActionResult About()
        {
            getInfo();
            return View();
        }

        [Authorize(Roles = "Searcher")]

        public ActionResult Searcher(string successMessage, string errorMessage)
        {
            var user = HttpContext.User.Identity.Name;
            var searches = db.JobSearches.Where(js => js.NAJB_user.email == user).ToList();
            getInfo();
            ViewBag.SuccessMessage = successMessage;
            ViewBag.ErrorMessage = errorMessage;
            return View(searches);
        }


        [Authorize(Roles = "Searcher")]
        [HttpGet]
        public ActionResult EditSearcher(ManageMessageId? message)
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            getInfo();
            ViewBag.StatusMessage =
               message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
               : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
               : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
               : message == ManageMessageId.Error ? "An error has occurred."
               : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
               : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
               : "";
            return View(repo.GetUser(user));
        }


        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult EditSearcher(EditUser user, string email, string myUploadedImg)
        {
            SearcherRepo repo = new SearcherRepo();
            repo.UpdateUser(user, myUploadedImg);
            return RedirectToAction("Searcher", "Searcher");
        }

        [Authorize(Roles = "Searcher")]
        [HttpGet]
        public ActionResult AddSearch(string successMessage)
        {
            getInfo();
            ViewBag.SuccessMessage = successMessage;
            return View();
        }

        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult AddSearch(AddSearch user)
        {
            var thisUser = HttpContext.User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (!IsAnyNullOrEmpty(user))
                {
                    @ViewBag.ErrorMessage = "You must fill out at least 3 fields to submit a Job Search.";                 
                    getInfo();
                    return View();
                }
                else
                {
                    SearcherRepo repo = new SearcherRepo();
                    repo.SaveJobSearch(user, thisUser);
                    string successMessage = "You have successfully added a Job Search!";
                    return RedirectToAction("AddSearch", "Searcher", new { successMessage });
                }
            }

            else
            {
                @ViewBag.ErrorMessage = "You must fill out at least 3 fields to submit a Job Search.";
                getInfo();
                return View();
            }
        }      

        bool IsAnyNullOrEmpty(AddSearch myObject)
        {
            var count = 0;

            if (myObject.JobName == null)
            {
                count++;
            }
            if (myObject.Industry == null)
            {
                count++;
            }
            if (myObject.CompanyName == null)
            {
                count++;
            }
            if (myObject.SalaryMin == null)
            {
                count++;
            }
            if (myObject.Location == null)
            {
                count++;
            }
            if (myObject.JobType == null)
            {
                count++;
            }
            if (count < 3)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [Authorize(Roles = "Searcher")]
        [HttpGet]
        public ActionResult EditSearch(int id)
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            getInfo();
            return View(repo.GetJobSearch(id));
        }

        [Authorize(Roles = "Searcher")]
        [HttpPost]
        public ActionResult EditSearch(JobSearch jobSearch)
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            repo.UpdateJobSearch(jobSearch);
            getInfo();
            var message = "You have successfully updated your Job Search!";
            return RedirectToAction("Searcher", "Searcher", message);
        }

        public ActionResult Companies()
        {
            var companies = db.Companies.ToList();
            getInfo();
            return View(companies);
        }
        [HttpGet]
        public ActionResult UploadResume()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadResume(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/Resumes"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                getInfo();
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                getInfo();
                return View();
            }
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Matches()
        {
            var user = HttpContext.User.Identity.Name;
            var jobMatch = db.JobMatches.Where(j => j.email == user && j.ignore == false);
            getInfo();
            return View(jobMatch);
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Favourites()
        {
            var user = HttpContext.User.Identity.Name;
            var favourites = db.JobMatches.Where(j => j.email == user && (j.favourite == true && j.ignore == false) || (j.applied == true && j.ignore == false));
            getInfo();
            return View(favourites);
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult DeleteJobSearch(int id)
        {
            var record = db.JobSearches.Where(j => j.ID == id).FirstOrDefault();
            if (record != null)
            {
                db.JobSearches.Remove(record);
                db.SaveChanges();
            }
            string successMessage = "Job Search Removed";
            return RedirectToAction("Searcher", new { successMessage });
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult ActivateJobSearch(int id)
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            repo.ActivateJobSearch(id);
            return RedirectToAction("Searcher");
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult DeactivateJobSearch(int id)
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            repo.DeactivateJobSearch(id);
            return RedirectToAction("Searcher");
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Deactivate()
        {
            var user = HttpContext.User.Identity.Name;
            SearcherRepo repo = new SearcherRepo();
            repo.DeactivateUserSearches(user);
            return RedirectToAction("Searcher");
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Job(int id)
        {
            SearcherRepo repo = new SearcherRepo();
            var user = HttpContext.User.Identity.Name;
            var ID = id;
            if (User.Identity.IsAuthenticated)
            {
                getInfo();
                var job = db.Jobs.Where(j => j.ID == ID).FirstOrDefault();
                job.views += 1;
                db.SaveChanges();
                return View(repo.GetJob(ID, user));
            }
            else
            {
                return RedirectToAction("Job", "Home", new { id = ID });
            }
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Company(int id)
        {
            var user = HttpContext.User.Identity.Name;
            var company = db.Companies.Where(c => c.ID == id).FirstOrDefault();
            if (User.Identity.IsAuthenticated)
            {
                getInfo();
                return View(company);
            }
            else
            {
                return RedirectToAction("Company", "Home", new { id = id });
            }
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Favourite(int id)
        {
            SearcherRepo repo = new SearcherRepo();
            var user = HttpContext.User.Identity.Name;
            var ID = id;
            repo.SaveFavourite(ID, user);
            return RedirectToAction("Job", new { id = ID });
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult UnFavourite(int id)
        {
            SearcherRepo repo = new SearcherRepo();
            var user = HttpContext.User.Identity.Name;
            var ID = id;
            repo.UnSaveFavourite(ID, user);
            return RedirectToAction("Favourites", "Searcher");
        }

        [Authorize(Roles = "Searcher")]
        public ActionResult Ignore(int id)
        {
            SearcherRepo repo = new SearcherRepo();
            var user = HttpContext.User.Identity.Name;
            var ID = id;
            repo.SaveIgnore(ID, user);
            return RedirectToAction("Job", new { id = ID });
        }

        [Authorize(Roles = "Searcher")]
        public async Task<ActionResult> Share(int id, string shareEmail)
        {
            SearcherRepo repo = new SearcherRepo();
            var user = HttpContext.User.Identity.Name;
            var ID = id;
            await repo.SendAsync(id, shareEmail);
            return RedirectToAction("Job", new { id = ID });
        }
    }
}