using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using NAJB_BS_Final.ViewModels;
using System.Globalization;
using MoreLinq;

namespace NAJB_BS_Final.Models
{
    public class EmployerRepo
    {
        public IEnumerable<Job> GetEmployerJobs(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();

            var today = DateTime.Now;
            var jobs = db.Jobs.Where(j => j.Company.NAJB_user.email == email && (j.jobEndDate > today)).ToList();
            var oldJobs = db.Jobs.Where(j => j.jobEndDate < today).ToList();

            foreach (var oldjob in oldJobs)
            {
                oldjob.active = false;
                try
                {
                    db.SaveChanges();

                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            return jobs;
        }

        public EditUser GetEmployer(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var employer = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            EditUser user = new EditUser();

            user.Email = employer.email;
            user.FirstName = employer.firstname;
            user.HasPassword = (employer.AspNetUser.PasswordHash.Length > 0);
            user.Image = employer.userImage;
            user.LastName = employer.lastname;
            user.Resume = employer.resume;
            user.UserID = employer.ID;

            return user;
        }

        public void SaveNewCompany(Company newCompany)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            Company company = new Company();

            company.name = newCompany.name;
            company.address = newCompany.address;
            company.logo = newCompany.logo;
            company.NAJB_userID = db.NAJB_user.Where(u => u.email == HttpContext.Current.User.Identity.Name).Select(u => u.ID).FirstOrDefault();
            company.description = newCompany.description;
            company.credits = 0;
            company.phone = newCompany.phone;
            company.industry = newCompany.industry;
            company.website = newCompany.website;
            company.contact = newCompany.contact;

            db.Companies.Add(company);
            db.SaveChanges();
        }

        public void UpdateEmployer(EditUser employer)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == employer.Email).FirstOrDefault();

            najbUser.firstname = employer.FirstName;
            najbUser.lastname = employer.LastName;
            najbUser.email = employer.Email;

            db.SaveChanges();
        }

        public EditCompany GetCompany(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var newCompany = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault();

            EditCompany company = new EditCompany();

            company.CompanyName = newCompany.name;
            company.Address = newCompany.address;
            company.Image = newCompany.logo;
            company.CompanyDescription = newCompany.description;
            company.Phone = newCompany.phone;
            company.Industry = newCompany.industry;
            company.Website = newCompany.website;
            company.Contact = newCompany.contact;

            return company;
        }

        public void UpdateCompany(EditCompany company)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var thisCompany = db.Companies.Where(c => c.ID == company.CompanyID).FirstOrDefault();

            thisCompany.name = company.CompanyName;
            thisCompany.address = company.Address;
            thisCompany.website = company.Website;
            thisCompany.contact = company.Contact;
            thisCompany.phone = company.Phone;
            thisCompany.description = company.CompanyDescription;
            thisCompany.logo = company.CompanyImage;
            thisCompany.industry = company.Industry;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        public ViewCompany GetUserCompany(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            ViewCompany userCompany = new ViewCompany();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            userCompany.UserID = najbUser.ID;
            userCompany.FirstName = najbUser.firstname;
            userCompany.LastName = najbUser.lastname;
            userCompany.Email = najbUser.email;
            userCompany.UserImage = najbUser.userImage;
            userCompany.Users = db.NAJB_user.Where(u => u.AspNetUser.AspNetRoles.FirstOrDefault().Name == "Searcher").ToList();
            userCompany.Transactions = db.Transactions.Where(t => t.CompanyID == userCompany.CompanyID).ToList();
            Company company = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault();

            if (company != null)
            {
                userCompany.Company = company;
            }
            else
            {
                userCompany.CompanyName = null;
                userCompany.Jobs = null;
                return userCompany;
            }

            var today = DateTime.Now;
            var jobs = db.Jobs.Where(j => j.Company.name == company.name && (j.jobEndDate > today)).ToList();
            var oldJobs = db.Jobs.Where(j => j.jobEndDate < today).ToList();

            foreach (var oldjob in oldJobs)
            {
                oldjob.active = false;
                try
                {
                    db.SaveChanges();

                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }


            List<Jobs> jobsList = new List<Jobs>();
            foreach (var job in jobs)
            {
                jobsList.Add(new Jobs(job.ID, job.name, job.description, job.Company.name, job.location, job.reference, job.industry, job.jobType, job.experience, job.active, job.salaryMin, job.salaryMax));
            }

            userCompany.Jobs = jobsList;
            return userCompany;
        }

        //public bool SaveCompany(ViewCompany newCompany)
        //{
        //    notajobb_devEntities db = new notajobb_devEntities();
        //    Company company = new Company();

        //    company.name = newCompany.CompanyName;
        //    company.address = newCompany.Address;
        //    company.logo = newCompany.CompanyImage;
        //    company.NAJB_userID = db.NAJB_user.Where(u => u.email == HttpContext.Current.User.Identity.Name).Select(u => u.ID).FirstOrDefault();
        //    company.description = newCompany.CompanyDescription;
        //    company.credits = 0;
        //    company.phone = newCompany.Phone;
        //    company.industry = newCompany.Industry;
        //    company.website = newCompany.Website;
        //    company.contact = newCompany.Contact;

        //    db.Companies.Add(company);
        //    db.SaveChanges();

        //    var companyNames = db.Companies.ToList();
        //    var jobCompanyNames = db.JobCompanies.ToList();

        //    foreach (var companyName in companyNames)
        //    {
        //        foreach (var jobCompanyName in jobCompanyNames)
        //        {
        //            if (db.JobCompanies.Any(j => j.Companies == companyName.name))
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                JobCompany newJobCompanyName = new JobCompany();
        //                newJobCompanyName.Companies = companyName.name;

        //                db.JobCompanies.Add(newJobCompanyName);
        //                db.SaveChanges();
        //            }
        //        }
        //    }

        //    return true;
        //}

        //public bool UpdateCompanyUser(ViewCompany company)
        //{
        //    notajobb_devEntities db = new notajobb_devEntities();
        //    // change to company and user
        //    NAJB_user najbUser = db.NAJB_user.Where(u => u.email == company.Email).FirstOrDefault();

        //    najbUser.firstname = company.FirstName;
        //    najbUser.lastname = company.LastName;
        //    najbUser.email = company.Email;
        //    najbUser.userImage = company.Image;

        //    try
        //    {
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //            }
        //        }
        //    }

        //    return true;
        //}

        public bool SaveJob(AddJob newJob, string cke_JobDescription)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var salary = newJob.SalaryMin.Split(';');

            Job job = new Job();

            job.companyID = db.Companies.Where(c => c.NAJB_user.email == HttpContext.Current.User.Identity.Name).Select(u => u.ID).FirstOrDefault();
            job.description = newJob.JobDescription;
            job.location = newJob.Location;
            job.name = newJob.JobName;
            job.jobType = newJob.JobType;
            job.experience = newJob.Experience;
            job.industry = newJob.Industry;
            job.salaryMin = salary[0];
            job.salaryMax = salary[1];
            job.jobDate = newJob.StartDate;
            job.jobEndDate = job.jobDate.AddDays(60);
            job.active = true;
            job.applyTo = newJob.ApplyTo;
            job.views = 0;
            
            db.Jobs.Add(job);

            var company = db.Companies.Where(c => c.ID == job.companyID).FirstOrDefault();
            company.credits -= 100;

            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            //var jobNames = db.Jobs.Distinct().ToList();
            //var jobTitles = db.JobTitles.Distinct().ToList();

            //foreach (var jobName in jobNames)
            //{
            //    if (jobTitles.Count() > 0)
            //    {
            //        foreach (var jobTitle in jobTitles)
            //        {
            //            if (jobName.name.ToLower().Contains(jobTitle.Title.ToLower()))
            //            {

            //            }
            //            else
            //            {
            //                JobTitle newJobTitle = new JobTitle();
            //                newJobTitle.Title = newJob.JobName.Replace(",", "");

            //                db.JobTitles.Add(newJobTitle);
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JobTitle newJobTitle = new JobTitle();
            //        newJobTitle.Title = newJob.JobName;

            //        db.JobTitles.Add(newJobTitle);
            //        db.SaveChanges();
            //    }
            //}

            //var jobIndustries = db.Jobs.ToList();
            //var jobIndustriesNames = db.JobIndustries.ToList();

            //foreach (var jobIndustry in jobIndustries)
            //{
            //    foreach (var jobIndustriesName in jobIndustriesNames)
            //    {
            //        if (jobIndustry.industry.ToLower().Contains(jobIndustriesName.Industries.ToLower()))
            //        {

            //        }
            //        else
            //        {
            //            JobIndustry newJobIndustry = new JobIndustry();
            //            newJobIndustry.Industries = newJob.Industry.Replace(",", "");

            //            db.JobIndustries.Add(newJobIndustry);
            //            db.SaveChanges();
            //        }
            //    }
            //}

            //var jobCompanies = db.Jobs.ToList();
            //var jobCompanyNames = db.JobCompanies.ToList();

            //foreach (var jobCompany in jobCompanies)
            //{
            //    foreach (var jobCompanyName in jobCompanyNames)
            //    {
            //        if (jobCompany.name.ToLower().Contains(jobCompanyName.Companies.ToLower()))
            //        {

            //        }
            //        else
            //        {
            //            JobCompany newJobCompany = new JobCompany();
            //            newJobCompany.Companies = newJob.CompanyName.Replace(",", "");

            //            db.JobCompanies.Add(newJobCompany);
            //            db.SaveChanges();
            //        }
            //    }
            //}

            return true;
        }

        public AddJob GetUserCompanyDuplicate(string email, int id)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var oldJob = db.Jobs.Where(j => j.ID == id).FirstOrDefault();

            AddJob newJob = new AddJob();

            newJob.ApplyTo = oldJob.applyTo;
            newJob.Company = oldJob.Company;
            newJob.Experience = oldJob.experience;
            newJob.Industry = oldJob.industry;
            newJob.JobDescription = oldJob.description;
            newJob.JobName = oldJob.name;
            newJob.JobType = oldJob.jobType;
            newJob.Location = oldJob.location;
            newJob.ReferenceNumber = oldJob.reference;
            newJob.SalaryMax = oldJob.salaryMax;
            newJob.SalaryMin = oldJob.salaryMin;

            return newJob;
        }

       

        public ViewCompany GetArchive(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            ViewCompany userCompany = new ViewCompany();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            userCompany.UserID = najbUser.ID;
            userCompany.FirstName = najbUser.firstname;
            userCompany.LastName = najbUser.lastname;
            userCompany.Email = najbUser.email;
            userCompany.UserImage = najbUser.userImage;

            Company company = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault();

            if (company != null)
            {
                userCompany.CompanyID = company.ID;
                userCompany.CompanyName = company.name;
                userCompany.Address = company.address;
                userCompany.Credits = (int)company.credits;
                userCompany.Website = company.website;
                userCompany.Contact = company.contact;
                userCompany.CompanyDescription = company.description;
                userCompany.CompanyImage = company.logo;
            }
            else
            {
                userCompany.CompanyName = null;
                userCompany.Jobs = null;
                return userCompany;
            }

            var jobs = db.Jobs.Where(j => j.Company.name == company.name && j.active == false).ToList();

            List<Jobs> jobsList = new List<Jobs>();
            foreach (var job in jobs)
            {
                jobsList.Add(new Jobs(job.ID, job.name, job.description, job.Company.name, job.location, job.reference, job.industry, job.jobType, job.experience, job.active, job.salaryMin, job.salaryMax));
            }

            userCompany.Jobs = jobsList;

            var transactions = db.Transactions.Where(t => t.Company.name == company.name).ToList();

            //List<Transactions> transactionList = new List<Transactions>();
            //foreach (var transaction in transactions)
            //{
            //    transactionList.Add(new Transactions(transaction.CompanyID, transaction.PayPalID.ToString(), transaction.CompanyID, transaction.Credits, transaction.Dollars.ToString(), transaction.TransactionDate));
            //}

            userCompany.Transactions = transactions;

            return userCompany;
        }

        public bool AddCreditsTest(string credits, string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var company = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault();

            if (company.Transactions.Count() == 0)
            {
                company.credits += (2 * (Int32.Parse(credits)));
                credits = company.credits.ToString();
            }
            else
            {
                company.credits += Int32.Parse(credits);
            }


            db.SaveChanges();

            if (AddTransactionTest(credits, email))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SaveDuplicateJob(AddJob newJob)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            Job job = new Job();

            job.companyID = db.Companies.Where(c => c.NAJB_user.email == HttpContext.Current.User.Identity.Name).Select(u => u.ID).FirstOrDefault();
            job.description = newJob.JobDescription;
            job.location = newJob.Location;
            job.name = newJob.JobName;
            job.jobType = newJob.JobType;
            job.industry = newJob.Industry;
            job.salaryMin = newJob.SalaryMin;
            job.salaryMax = newJob.SalaryMax;
            job.jobDate = DateTime.Now;
            job.jobEndDate = job.jobDate.AddDays(60);
            job.active = true;

            db.Jobs.Add(job);

            var company = db.Companies.Where(c => c.ID == newJob.Company.ID).FirstOrDefault();
            company.credits -= 100;

            try
            {
                db.SaveChanges();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }

            //var jobNames = db.Jobs.Distinct().ToList();
            //var jobTitles = db.JobTitles.Distinct().ToList();

            //foreach (var jobName in jobNames)
            //{
            //    if (jobTitles.Count() > 0)
            //    {
            //        foreach (var jobTitle in jobTitles)
            //        {
            //            if (jobName.name.ToLower().Contains(jobTitle.Title.ToLower()))
            //            {

            //            }
            //            else
            //            {
            //                JobTitle newJobTitle = new JobTitle();
            //                newJobTitle.Title = newJob.JobName;

            //                db.JobTitles.Add(newJobTitle);
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JobTitle newJobTitle = new JobTitle();
            //        newJobTitle.Title = newJob.JobName;

            //        db.JobTitles.Add(newJobTitle);
            //        db.SaveChanges();
            //    }
            //}

            //var jobIndustries = db.Jobs.ToList();
            //var jobIndustriesNames = db.JobIndustries.ToList();

            //foreach (var jobIndustry in jobIndustries)
            //{
            //    foreach (var jobIndustriesName in jobIndustriesNames)
            //    {
            //        if (jobIndustry.industry.ToLower().Contains(jobIndustriesName.Industries.ToLower()))
            //        {

            //        }
            //        else
            //        {
            //            JobIndustry newJobIndustry = new JobIndustry();
            //            newJobIndustry.Industries = newJob.Industry;

            //            db.JobIndustries.Add(newJobIndustry);
            //            db.SaveChanges();
            //        }
            //    }
            //}

            //var jobCompanies = db.Jobs.ToList();
            //var jobCompanyNames = db.JobCompanies.ToList();

            //foreach (var jobCompany in jobCompanies)
            //{
            //    foreach (var jobCompanyName in jobCompanyNames)
            //    {
            //        if (jobCompany.name.ToLower().Contains(jobCompanyName.Companies.ToLower()))
            //        {

            //        }
            //        else
            //        {
            //            JobCompany newJobCompany = new JobCompany();
            //            newJobCompany.Companies = newJob.CompanyName;

            //            db.JobCompanies.Add(newJobCompany);
            //            db.SaveChanges();
            //        }
            //    }
            //}

            return true;
        }
        public bool AddTransactionTest(string credits, string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            Transaction transaction = new Transaction();

            transaction.PayPalID = "1234567890test";
            transaction.CompanyID = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault().ID;
            transaction.Credits = Int32.Parse(credits);
            if (credits == "100" || credits == "200")
            {
                transaction.Dollars = 99.00M;
            }
            if (credits == "500")
            {
                transaction.Dollars = 399.00M;
            }
            if (credits == "1000")
            {
                transaction.Dollars = 799.00M;
            }
            transaction.TransactionDate = DateTime.Now;

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return true;
        }

        //public Company GetCompanyByID(int id)
        //{
        //    notajobb_devEntities db = new notajobb_devEntities();
        //    var company = db.Companies.Where(c => c.ID == id).FirstOrDefault();

        //    return company;
        //}

        public List<EmployerMatch> GetEmployerMatches(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();

            var jobMatches = db.JobMatches.DistinctBy(jm => jm.jobID).Where(jm => jm.Company.NAJB_user.email == email).ToList();

            List<EmployerMatch> employerMatch = new List<EmployerMatch>();

            foreach (var jobMatch in jobMatches)
            {
                var allMatches = db.JobMatches.Where(jm => jm.jobID == jobMatch.jobID).ToList();
                var allUsers = db.NAJB_user.Where(u => u.AspNetUser.AspNetRoles.FirstOrDefault().Name == "Searcher").ToList();
                var users = new List<NAJB_user>();
                foreach (var user in allUsers)
                {
                    foreach (var thisMatch in allMatches)
                    {
                        if (user.ID == thisMatch.NAJB_userID)
                        {
                            users.Add(user);
                        }
                    }
                }
                EmployerMatch match = new EmployerMatch(jobMatch, users);
                employerMatch.Add(match);
            }

            return employerMatch;
        }
    }
}