using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NAJB_BS_Final.Models;
using NAJB_BS_Final.ViewModels;

namespace NAJB_BS_Final.Controllers
{
    public class EmailController : Controller
    {
        public static string DOMAIN = "http://dev.notajobboard.com";

        // GET: GetMatches
        public ActionResult GetMatches()
        {
            SearcherRepo.GetJobMatches(); 
            return RedirectToAction("Index","Home");
        }
        
        // GET: SendEmail
        public ActionResult SendEmail()
        {
            SendJobMatches();
            return RedirectToAction("Index", "Home");
        }
        // GET: Email
        public void SendJobMatches()
        {      
            var to = "";
            

            notajobb_devEntities db = new notajobb_devEntities();
            var emails = db.JobMatches.Select(e => e.email).ToList();
            var distinct = emails.Distinct().ToList();
            //List<JobMatches> emails = SearcherRepo.GetEmailList();

            foreach (var email in distinct)
            {
                var jobMatches = "";
                List<JobMatch> emailList = db.JobMatches.Where(jm => jm.email == email).ToList();
                to = email;
                foreach (var job in emailList)
                {
                    string image = DOMAIN + "/Content/Images/" + job.logo;
                    string description = "";
                    if (job.description.Length > 20)
                    {
                        description = job.description.Substring(0, 20);
                    }
                    else
                    {
                        description = job.description;
                    }

                    string jobMatch = "<div style='box-sizing: border-box; border-top: 1px solid rgba(0,0,0,.14); border-bottom: 1px solid rgba(0,0,0,.14);'>" +
                        "<div style='padding: 10px;'><img src='" + image + "' alt='image logo' style ='display:inline-block; padding: 10px; margin:0px; max-height: 100px;" +
                        "max-width: 120px; border-radius: 50%; width: 64px; height: 64px;'/>" +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;'>Job Name: " + 
                        "<a href='" + DOMAIN + "/Searcher/Job?id=" + job.jobID.ToString() + "'> " + job.name + " </a></p> " + 
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;'>Company Name: " + job.companyName + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Industry: " + job.industry + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Location: " + job.location + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Job Type: " + job.jobType + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Description: " + description + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' >" +
                        "<a style = 'text-decoration: none;" +
                        "color: #fff; background-color: #26a69a; text-align: center; letter-spacing: .5px; background: 0 0;" + 
                        "border: none; border-radius: 2px; color: #000; position: relative; height: 36px; margin: 0; min-width: 64px;" + 
			            "padding: 0 16px; display: inline-block; font-size: 14px; font-weight: 500; text-transform: uppercase; letter-spacing: 0;" +
                        "overflow: hidden; transition: box - shadow .2s cubic-bezier(.4, 0, 1, 1),background - color .2s cubic-bezier(.4, 0, .2, 1)," +
                        "color .2s cubic-bezier(.4, 0, .2, 1); outline: none; cursor: pointer; text-decoration: none; text - align: center;" +
                        "line - height: 36px; vertical-align: middle; background: rgba(158, 158, 158, .2); box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14),"+
                        "0 3px 1px - 2px rgba(0, 0, 0, .2),0 1px 5px 0 rgba(0, 0, 0, .12); color: rgb(255, 255, 255); background-color: rgb(255, 82, 82); '" +
                        "href = '" + DOMAIN + "/Searcher/Job?id=" + job.jobID.ToString() + "' >View Job</a></p></div>";

                    jobMatches += jobMatch;
                }

                string body =   "<div style='box-shadow: 0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12);" +
                                "box-sizing: border-box;'><div style='padding:25px; color: black!important; background-color: white!important;'>" +
                                "<img src='" + DOMAIN + "/Content/images/NAJB_Logo_Web.png' style='height: 50px;'>" +
                                "<p class='lead'>The best way to connect job searchers with job hirers.</p></div>" +
	                            "<div style='padding:25px;'><p><strong>Hello " + to + "!  Here are your job matches for today:</strong></p>" +
		                        "<hr style='border-top-color: #ee6e73;'/></div><div style='padding: 20px'" + jobMatches + "</div></div>" +
                                "<div style='padding:10px;'><p style='font-size: 10px;'>Copyright NAJB Inc. 2016</p>"+
                                "<p style='font-size: 10px;'>All rights reserved.</p></div></div>";

                //SendAsync(to, body);

                using (var message = new MailMessage("info@notajobboard.com", to))
                {
                    message.Subject = "NAJB Matches - " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
                    message.Body = body;
                    message.IsBodyHtml = true;

                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = false,
                        Host = "smtp.sendgrid.net",
                        Port = 587,
                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential("jesse@loomo.ca", "snowboarding1")
                    })
                    try
                    {
                        client.Send(message);
                    }
                    catch (System.Exception ex)
                    {
                        var error = ex.Message;
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Share(string from, string email, int jobID)
        {
            SendShareEmail(from, email, jobID);
            return RedirectToAction("Job", new { Id = jobID });
        }

        public void SendShareEmail(string from, string email, int jobID)
        {
            notajobb_devEntities db = new notajobb_devEntities();

            var job = db.Jobs.Where(j => j.ID == jobID).FirstOrDefault();

            string jobMatch = "<div style='box-sizing: border-box; border-top: 1px solid rgba(0,0,0,.14); border-bottom: 1px solid rgba(0,0,0,.14);'>" +
                        "<div style='padding: 10px;'><img src='" + job.Company.logo + "' alt='image logo' style ='display:inline-block; padding: 10px; margin:0px; max-height: 100px;" +
                        "max-width: 120px; border-radius: 50%; width: 64px; height: 64px;'/>" +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;'>Job Name: " +
                        "<a href='" + DOMAIN + "/Searcher/Job?id=" + job.ID.ToString() + "'> " + job.name + " </a></p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;'>Company Name: " + job.Company.name + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Industry: " + job.industry + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Location: " + job.location + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Job Type: " + job.jobType + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' > Description: " + job.description + " </p> " +
                        "<br /><p style='display:inline-block; padding: 10px 10px 10px 0px; margin:0px;' >" +
                        "<a style = 'text-decoration: none;" +
                        "color: #fff; background-color: #26a69a; text-align: center; letter-spacing: .5px; background: 0 0;" +
                        "border: none; border-radius: 2px; color: #000; position: relative; height: 36px; margin: 0; min-width: 64px;" +
                        "padding: 0 16px; display: inline-block; font-size: 14px; font-weight: 500; text-transform: uppercase; letter-spacing: 0;" +
                        "overflow: hidden; transition: box - shadow .2s cubic-bezier(.4, 0, 1, 1),background - color .2s cubic-bezier(.4, 0, .2, 1)," +
                        "color .2s cubic-bezier(.4, 0, .2, 1); outline: none; cursor: pointer; text-decoration: none; text - align: center;" +
                        "line - height: 36px; vertical-align: middle; background: rgba(158, 158, 158, .2); box-shadow: 0 2px 2px 0 rgba(0, 0, 0, .14)," +
                        "0 3px 1px - 2px rgba(0, 0, 0, .2),0 1px 5px 0 rgba(0, 0, 0, .12); color: rgb(255, 255, 255); background-color: rgb(255, 82, 82); '" +
                        "href = '" + DOMAIN + "/Home/Job?id=" + job.ID.ToString() + "' >View Job</a></p></div>";

            string body = "<div style='box-shadow: 0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12);" +
                                "box-sizing: border-box;'><div style='padding:25px; color: black!important; background-color: white!important;'>" +
                                "<img src='" + DOMAIN + "/Content/images/NAJB_Logo_Web.png' style='height: 50px;'>" +
                                "<p class='lead'>The best way to connect job searchers with job hirers.</p><hr style='border-top-color: #ee6e73;'/></div>" +
                                "<div style='padding:0px 25px;'><p><strong>Hello " + email + "!  Your friend " + from + " thought you might like this Job Post:</strong></p>" +
                                "<hr style='border-top-color: #ee6e73;'/></div><div style='padding: 20px'" + jobMatch + "</div><hr style='border-top-color: #ee6e73;'/></div>" +
                                "<div style='padding:10px;'><p style='font-size: 10px;'>Copyright NAJB Inc. 2016</p>" +
                                "<p style='font-size: 10px;'>All rights reserved.</p></div></div>";

            using (var message = new MailMessage("share@notajobboard.com", email))
            {
                message.Subject = "NAJB Matches - " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
                message.Body = body;
                message.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = false,
                    Host = "smtp.sendgrid.net",
                    Port = 587,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("jesse@loomo.ca", "snowboarding1")
                })
                    try
                    {
                        client.Send(message);
                    }
                    catch (System.Exception ex)
                    {
                        var error = ex.Message;
                    }
            }
        }

        [HttpPost]
        public ActionResult SendApplication(string to, string from, string subject, string body, string email, string resume, int ID)
        {
            SendApplicationEmail(to, from, subject, body, email, resume, ID);
            return RedirectToAction("Job", "Searcher", new { Id = ID });
        }

        public void SendApplicationEmail(string to, string from, string subject, string body, string email, string resume, int jobID)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var PathToAttachment = "";
            if (resume == "Resume")
            {
                var resumeFile = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();
                string uriPath = "file:\\C:\\Users\\Jesse\\Documents\\Visual Studio 2015\\Projects\\NAJB_BS_Final\\NAJB_BS_Final\\Content\\Resumes\\" + resumeFile.resume;

                PathToAttachment = new Uri(uriPath).LocalPath;
            }
            else
            {
                PathToAttachment = null;
            }
            var job = db.Jobs.Where(j => j.ID == jobID).FirstOrDefault();

            string emailBody = "<div style='box-shadow: 0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12);" +
                                "box-sizing: border-box;'><div style='padding:25px; color: black!important; background-color: white!important;'>" +
                                "<img src='" + DOMAIN + "/Content/images/NAJB_Logo_Web.png' style='height: 50px;'>" +
                                "<p class='lead'>The best way to connect job searchers with job hirers.</p><hr style='border-top-color: #ee6e73;'/></div>" +
                                "<div style='padding:0px 25px;'><p><strong>Hello " + to + "!  " + from + " has submitted an " + subject + "</strong></p>" +
                                "<hr style='border-top-color: #ee6e73;'/></div><div style='padding: 20px'" + body + "</div><hr style='border-top-color: #ee6e73;'/></div>" +
                                "<div style='padding:10px;'><p style='font-size: 10px;'>Copyright NAJB Inc. 2016</p>" +
                                "<p style='font-size: 10px;'>All rights reserved.</p></div></div>";

            using (var message = new MailMessage("applications@notajobboard.com", to))
            {
                message.Subject = "NAJB Application";
                message.Body = emailBody;
                message.IsBodyHtml = true;
                if (PathToAttachment != null)
                {
                    message.Attachments.Add(new Attachment(PathToAttachment));
                }

                using (SmtpClient client = new SmtpClient
                {
                    EnableSsl = false,
                    Host = "smtp.sendgrid.net",
                    Port = 587,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("jesse@loomo.ca", "snowboarding1")
                })
                    try
                    {
                        client.Send(message);
                    }
                    catch (System.Exception ex)
                    {
                        var error = ex.Message;
                    }
            }

            var jobMatch = db.JobMatches.Where(jm => jm.email == email && jm.jobID == jobID).FirstOrDefault();

            jobMatch.applied = true;
            jobMatch.applyDate = DateTime.Now;

            db.SaveChangesAsync();
        }
        public Task SendAsync(IdentityMessage message)
        {
            // Credentials:
            var credentialUserName = "info@notajobboard.com";
            var sentFrom = "info@notajobboard.com";
            var pwd = "pn9Da7_8";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("notajobboard.com");

            client.Port = 25;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = false;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, message.Destination);

            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            // Send:
            return client.SendMailAsync(mail);
        }

        public Task SendAsync(string to, string body)
        {
            // Credentials:
            var credentialUserName = "jesse@loomo.ca";
            var sentFrom = "info@notajobboard.com";
            var pwd = "snowboarding1";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.sendgrid.net");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = false;
            client.Credentials = credentials;

            // Create the message:
            var mail =
                new System.Net.Mail.MailMessage(sentFrom, to);

            mail.Subject = "NAJB Matches - " + DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            mail.Body = body;
            mail.IsBodyHtml = true;

            // Send:
            return client.SendMailAsync(mail);
        }
    }
}