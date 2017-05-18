using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.IO;

namespace NAJB_BS_Final.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        notajobb_devEntities db = new notajobb_devEntities();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AutoCompleteGetJobName()
        {
            using (db)
            {
                var result = (from j in db.JobTitles
                              select new { j.Title }).Distinct().ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AutoCompleteGetJobNameQuery(string term)
        {
            var result = (from j in db.JobTitles
                          where j.Title.ToLower().Contains(term.ToLower())
                          select new { j.Title }).Distinct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteGetCompanyName()
        {
            using (db)
            {
                var result = (from j in db.JobCompanies
                              select new { j.Companies }).Distinct().ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AutoCompleteGetCompanyNameQuery(string term)
        {
            var result = (from r in db.JobCompanies
                          where r.Companies.ToLower().Contains(term.ToLower())
                          select new { r.Companies }).Distinct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteGetJobIndustry()
        {
            using (db)
            {
                var result = (from j in db.JobIndustries
                              select new { j.Industries }).Distinct().ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AutoCompleteGetJobIndustryQuery(string term)
        {

            var result = (from j in db.JobIndustries
                          where j.Industries.ToLower().Contains(term.ToLower())
                          select new { j.Industries }).Distinct();
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Ignore(string id)
        {
            var user = HttpContext.User.Identity.Name;
            var result = db.JobMatches.Where(j => j.jobID.ToString() == id && j.NAJB_user.email == user).FirstOrDefault();
            if (result != null && result.ignore == false) {
                result.ignore = true;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if(result != null && result.ignore == true) {
                result.ignore = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Favourite(string id)
        {
            var user = HttpContext.User.Identity.Name;
            var result = db.JobMatches.Where(j => j.jobID.ToString() == id && j.NAJB_user.email == user).FirstOrDefault();
            if (result != null && result.favourite == false)
            {
                result.favourite = true;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (result != null && result.favourite == true)
            {
                result.favourite = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Active(string id)
        {
            var user = HttpContext.User.Identity.Name;
            var result = db.JobSearches.Where(j => j.ID.ToString() == id && j.NAJB_user.email == user).FirstOrDefault();
            if (result != null && result.active == false)
            {
                result.active = true;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (result != null && result.active == true)
            {
                result.active = false;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadImage()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Content/Images/MVC_") + _imgname + _ext;
                    _imgname = "MVC_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize

                    var user = HttpContext.User.Identity.Name;
                    var userImage = db.NAJB_user.Where(u => u.email == user).FirstOrDefault();
                    userImage.userImage = Convert.ToString(_imgname);
                    db.SaveChanges();
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CompanyUploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Content/Images/MVC_") + _imgname + _ext;
                    _imgname = "MVC_" + _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize

                    var user = HttpContext.User.Identity.Name;
                    var companyImage = db.Companies.Where(c => c.NAJB_user.email == user).FirstOrDefault();
                    companyImage.logo = Convert.ToString(_imgname);
                    db.SaveChanges();
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            var user = HttpContext.User.Identity.Name;
            var userResume = db.NAJB_user.Where(u => u.email == user).FirstOrDefault();

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;
                        string _imgname = Guid.NewGuid().ToString();
                        var _ext = Path.GetExtension(file.FileName);
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        var path = userResume.firstname + ' ' + userResume.lastname + " Resume#" + userResume.ID + _ext;
                        fname = Path.Combine(Server.MapPath("~/Content/Resumes/"), path);
                        file.SaveAs(fname);
                        
                        userResume.resume = path;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}