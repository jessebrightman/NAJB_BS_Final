using NAJB_BS_Final.Controllers;
using NAJB_BS_Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NAJB_BS_Final.Models
{
    public class SearcherRepo
    {
        notajobb_devEntities db = new notajobb_devEntities();

        public IEnumerable<JobSearch> GetJobSearches(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var jobSearches = db.JobSearches.Where(js => js.NAJB_user.email == email).ToList();
            return jobSearches;
        }
        public bool SaveMyUser(RegisterViewModel newUser)
        {
            NAJB_user thisUser = new NAJB_user();

            thisUser.email = newUser.Email;
            thisUser.firstname = newUser.FirstName;
            thisUser.lastname = newUser.LastName;
            thisUser.userImage = null;
            thisUser.active = true;
            thisUser.IdentityID = db.AspNetUsers
                             .Where(u => u.UserName == newUser.Email).Select(u => u.Id).FirstOrDefault().ToString();

            AspNetUser user = db.AspNetUsers
                             .Where(u => u.UserName == newUser.Email).FirstOrDefault();
            AspNetRole role = db.AspNetRoles
                             .Where(r => r.Name == newUser.UserType).FirstOrDefault();

            user.AspNetRoles.Add(role);
            db.NAJB_user.Add(thisUser);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        return false;
                    }
                }
            }
            return true;
        }

        public bool SaveJobSearch(AddSearch newSearchProfile, string email)
        {
            string[] salary = newSearchProfile.SalaryMin.Split(';');
            JobSearch thisJobSearch = new JobSearch();

            thisJobSearch.najb_userID = db.NAJB_user.Where(u => u.email == email).Select(u => u.ID).FirstOrDefault();
            thisJobSearch.location = newSearchProfile.Location;
            thisJobSearch.companyName = newSearchProfile.CompanyName;
            thisJobSearch.industry = newSearchProfile.Industry;
            thisJobSearch.jobType = newSearchProfile.JobType;
            thisJobSearch.experience = newSearchProfile.Experience;
            thisJobSearch.jobName = newSearchProfile.JobName;
            thisJobSearch.radius = newSearchProfile.Radius;
            thisJobSearch.salaryMin = salary[0];
            thisJobSearch.salaryMax = salary[1];
            thisJobSearch.additionalLocations = newSearchProfile.AdditionalLocations;
            thisJobSearch.active = true;
            thisJobSearch.experience = newSearchProfile.Experience;

            db.JobSearches.Add(thisJobSearch);

            db.SaveChanges();

            // Go to Business Logic to add job titles, companies, and industries

            //var jobNames = db.Jobs.ToList();
            //var jobTitles = db.JobTitles.ToList();

            //foreach (var jobName in jobNames)
            //{
            //    if (jobTitles.Count() != 0)
            //    {
            //        foreach (var jobTitle in jobTitles)
            //        {
            //            if (jobName.name.ToLower().Contains(jobTitle.Title.ToLower()))
            //            {

            //            }
            //            else
            //            {
            //                JobTitle newJobTitle = new JobTitle();
            //                newJobTitle.Title = jobName.name;

            //                db.JobTitles.Add(newJobTitle);
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JobTitle newJobTitle = new JobTitle();
            //        newJobTitle.Title = jobName.name;

            //        db.JobTitles.Add(newJobTitle);
            //        db.SaveChanges();
            //    }
            //}

            //var jobIndustries = db.Jobs.ToList();
            //var jobIndustriesNames = db.JobIndustries.ToList();

            //foreach (var jobIndustry in jobIndustries)
            //{
            //    if (jobIndustriesNames.Count() != 0)
            //    {
            //        foreach (var jobIndustriesName in jobIndustriesNames)
            //        {
            //            if (jobIndustry.industry.ToLower().Contains(jobIndustriesName.Industries.ToLower()))
            //            {

            //            }
            //            else
            //            {
            //                JobIndustry newJobIndustry = new JobIndustry();
            //                newJobIndustry.Industries = jobIndustry.industry;

            //                db.JobIndustries.Add(newJobIndustry);
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JobIndustry newJobIndustry = new JobIndustry();
            //        newJobIndustry.Industries = jobIndustry.industry;

            //        db.JobIndustries.Add(newJobIndustry);
            //        db.SaveChanges();
            //    }
            //}

            //var jobCompanies = db.Jobs.ToList();
            //var jobCompanyNames = db.JobCompanies.ToList();

            //foreach (var jobCompany in jobCompanies)
            //{
            //    if (jobCompanyNames.Count() != 0)
            //    {
            //        foreach (var jobCompanyName in jobCompanyNames)
            //        {
            //            if (jobCompany.name.ToLower().Contains(jobCompanyName.Companies.ToLower()))
            //            {

            //            }
            //            else
            //            {
            //                JobCompany newJobCompany = new JobCompany();
            //                newJobCompany.Companies = newSearchProfile.CompanyName;

            //                db.JobCompanies.Add(newJobCompany);
            //                db.SaveChanges();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        JobCompany newJobCompany = new JobCompany();
            //        newJobCompany.Companies = newSearchProfile.CompanyName;

            //        db.JobCompanies.Add(newJobCompany);
            //        db.SaveChanges();
            //    }
            //}
            return true;
        }

        public ViewUser GetJobSearch(int jobID)
        {
            ViewUser user = new ViewUser();
            JobSearch thisJobSearch = db.JobSearches.Where(j => j.ID == jobID).FirstOrDefault();

            user.JobName = thisJobSearch.jobName;
            user.CompanyName = thisJobSearch.companyName;
            user.Industry = thisJobSearch.industry;
            user.JobType = thisJobSearch.jobType;
            user.Experience = thisJobSearch.experience;
            user.Location = thisJobSearch.location;
            user.Radius = thisJobSearch.radius;
            user.SalaryMin = thisJobSearch.salaryMin;
            user.SalaryMax = thisJobSearch.salaryMax;

            return user;
        }

        public bool UpdateJobSearch(JobSearch jobSearch)
        {
            
            return true;
        }

        public bool SaveUser(ViewUser user)
        {
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == user.Email).FirstOrDefault();

            najbUser.firstname = user.FirstName;
            najbUser.lastname = user.LastName;
            najbUser.email = user.Email;
            najbUser.userImage = user.Image;

            db.NAJB_user.Add(najbUser);

            if (user.CompanyName != null || user.Industry != null || user.JobType != null || user.Location != null || user.JobName != null)
            {
                JobSearch thisJobSearch = new JobSearch();

                thisJobSearch.NAJB_user.firstname = user.FirstName;
                thisJobSearch.NAJB_user.lastname = user.LastName;
                thisJobSearch.NAJB_user.email = user.Email;
                thisJobSearch.location = user.Location;
                thisJobSearch.industry = user.Industry;
                thisJobSearch.jobType = user.JobType;
                thisJobSearch.jobName = user.JobName;
                thisJobSearch.radius = user.Radius;
                thisJobSearch.salaryMin = user.SalaryMin;
                thisJobSearch.salaryMax = user.SalaryMax;

                db.JobSearches.Add(thisJobSearch);
            }
            db.SaveChanges();
            return true;
        }

        public bool UpdateUser(EditUser user, string myUploadedImg)
        {
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == user.Email).FirstOrDefault();

            najbUser.firstname = user.FirstName;
            najbUser.lastname = user.LastName;
            najbUser.email = user.Email;
            najbUser.userImage = user.Image;

            try
            {
                db.SaveChanges();
                return true;
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

            return true;
        }



        //public bool ValidLogin(Login login)
        //{
        //    UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
        //    UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(userStore)
        //    {
        //        UserLockoutEnabledByDefault = true,
        //        DefaultAccountLockoutTimeSpan = new TimeSpan(0, 10, 0),
        //        MaxFailedAccessAttemptsBeforeLockout = 3
        //    };
        //    var user = userManager.FindByName(login.UserName);

        //    if (user == null)
        //        return false;

        //    // User is locked out.
        //    if (userManager.SupportsUserLockout && userManager.IsLockedOut(user.Id))
        //        return false;

        //    // Validated user was locked out but now can be reset.
        //    if (userManager.CheckPassword(user, login.Password)
        //        && userManager.IsEmailConfirmed(user.Id))
        //    {
        //        if (userManager.SupportsUserLockout
        //         && userManager.GetAccessFailedCount(user.Id) > 0)
        //        {
        //            userManager.ResetAccessFailedCount(user.Id);
        //        }
        //    }
        //    // Login is invalid so increment failed attempts.
        //    else
        //    {
        //        bool lockoutEnabled = userManager.GetLockoutEnabled(user.Id);
        //        if (userManager.SupportsUserLockout && userManager.GetLockoutEnabled(user.Id))
        //        {
        //            userManager.AccessFailed(user.Id);
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public const string EMAIL_CONFIRMATION = "EmailConfirmation";
        public const string PASSWORD_RESET = "ResetPassword";
        //public bool CreateTokenProvider(UserManager<IdentityUser> manager, string tokenType)
        //{
        //    var provider = new DpapiDataProtectionProvider("MyApplicaitonName");
        //    manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(
        //    provider.Create(tokenType));
        //    return true;
        //}

        public Job GetJob(int id)
        {
            var job = db.Jobs.Where(j => j.ID == id).FirstOrDefault();

            return job;
        }
        public ViewUser GetJob(int id, string email)
        {
            ViewUser user = new ViewUser();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();
            var job = db.Jobs.Where(j => j.ID == id).FirstOrDefault();

            user.UserID = najbUser.ID;
            user.FirstName = najbUser.firstname;
            user.LastName = najbUser.lastname;
            user.Email = najbUser.email;
            user.Image = najbUser.userImage;

            user.JobName = job.name;

            user.Job = job;

            return user;
        }

        public IEnumerable<Job> GetJobs()
        {
            var jobs = db.Jobs.ToList();
            return jobs;
        }



        



        public Company GetCompany(string userName)
        {
            var company = db.Companies.Where(c => c.NAJB_user.email == userName).FirstOrDefault();

            return company;
        }

        public ViewCompany GetCompanyDetails(int id)
        {
            var company = db.Companies.Where(c => c.ID == id).FirstOrDefault();
            ViewCompany viewCompany = new ViewCompany();

            viewCompany.CompanyID = company.ID;
            viewCompany.CompanyName = company.name;
            viewCompany.Address = company.address;
            viewCompany.Website = company.website;
            viewCompany.Contact = company.contact;
            viewCompany.CompanyDescription = company.description;
            viewCompany.CompanyImage = company.logo;

            viewCompany.Job = db.Jobs.Where(j => j.companyID == id).ToList();

            return viewCompany;
        }

        public ViewUser GetCompanyDetails(int id, string email)
        {
            ViewUser user = new ViewUser();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            user.UserID = najbUser.ID;
            user.FirstName = najbUser.firstname;
            user.LastName = najbUser.lastname;
            user.Email = najbUser.email;
            user.UserImage = najbUser.userImage;

            var company = db.Companies.Where(c => c.ID == id).FirstOrDefault();
            user.Company = company;

            return user;
        }

        public static bool GetJobMatches()
        {
            notajobb_devEntities db = new notajobb_devEntities();
            var jobs = db.Jobs.Where(j => j.active == true).ToList();
            var jobSearches = db.JobSearches.Where(j => j.active == true).ToList();
            var jobMatches = db.JobMatches.ToList();

            int Searchcounter = 0;
            int Jobcounter = 0;

            foreach (var jobSearch in jobSearches)
            {
                var nullCount = 0;

                foreach (var job in jobs)
                {
                    nullCount = 0;

                    //check matches on location
                    if ((jobSearch.location == null || job.location.ToLower().Contains(jobSearch.location.ToLower())))
                    {
                        if (jobSearch.location == null)
                        {
                            nullCount = nullCount + 1;
                        }
                        if ((jobSearch.additionalLocations == null || jobSearch.additionalLocations.ToLower().Contains(job.location.ToLower())))
                        {
                            if ((jobSearch.industry == null || job.industry.ToLower().Contains(jobSearch.industry.ToLower()) || jobSearch.industry.ToLower().Contains(job.industry.ToLower())))
                            {
                                if (jobSearch.industry == null)
                                {
                                    nullCount = nullCount + 1;
                                }
                                if ((jobSearch.jobType == null || job.jobType.ToLower().Contains(jobSearch.jobType.ToLower())))
                                {
                                    if (jobSearch.jobType == null)
                                    {
                                        nullCount = nullCount + 1;
                                    }
                                    if ((jobSearch.companyName == null || job.Company.name.ToLower().Contains(jobSearch.companyName.ToLower())))
                                    {
                                        if (jobSearch.companyName == null)
                                        {
                                            nullCount = nullCount + 1;
                                        }
                                        if ((jobSearch.jobName == null || job.name.ToLower().Contains(jobSearch.jobName.ToLower()) || jobSearch.jobName.ToLower().Contains(job.name.ToLower())))
                                        {
                                            if (jobSearch.jobName == null)
                                            {
                                                nullCount = nullCount + 1;
                                            }

                                            if (nullCount <= 3)
                                            {
                                                var jobMatch = new JobMatch();

                                                jobMatch.email = db.NAJB_user.Where(u => u.ID == jobSearch.najb_userID).FirstOrDefault().email;
                                                jobMatch.firstname = jobSearch.NAJB_user.firstname;
                                                jobMatch.lastname = jobSearch.NAJB_user.lastname;
                                                jobMatch.location = job.location;
                                                jobMatch.industry = jobSearch.industry;
                                                jobMatch.jobType = jobSearch.jobType;
                                                jobMatch.name = job.name;
                                                jobMatch.description = job.description;
                                                jobMatch.companyName = job.Company.name;
                                                jobMatch.jobID = (int)job.ID;
                                                jobMatch.active = true;
                                                jobMatch.favourite = false;
                                                jobMatch.ignore = false;
                                                jobMatch.logo = job.Company.logo;
                                                jobMatch.jobDate = job.jobDate;
                                                jobMatch.endDate = job.jobEndDate;
                                                 
                                                jobMatch.NAJB_userID = (int)jobSearch.najb_userID;
                                                jobMatch.companyID = job.companyID;

                                                

                                                if (jobMatches.Any(j => (j.jobID == job.ID && j.email == jobMatch.email)))
                                                {
                                                    
                                                }
                                                else
                                                {
                                                    db.JobMatches.Add(jobMatch);
                                                    
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

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Jobcounter = Jobcounter + 1;
                }
                Searchcounter = Searchcounter + 1;
            }

            return true;
        }
        public static List<JobMatches> GetEmailList(string email)
        {
            notajobb_devEntities db = new notajobb_devEntities();

            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            List<JobMatches> jobMatchesList = new List<JobMatches>();
            JobMatches jobMatches = new JobMatches();

            var emailsList = db.JobMatches.Where(jm => jm.email == email).ToList();
            //var distinct = emails.Distinct().ToList();

            foreach (var item in emailsList)
            {
                List<JobMatch> jobMatchList = new List<JobMatch>();
                var thisJobMatchList = db.JobMatches.Where(j => j.email == email && j.ignore != true).ToList();

                foreach (var job in thisJobMatchList)
                {
                    JobMatch jobMatch = new JobMatch();
                    {
                        jobMatch.companyName = myTI.ToTitleCase(job.companyName);
                        jobMatch.description = job.description;
                        jobMatch.industry = myTI.ToTitleCase(job.industry);
                        jobMatch.name = myTI.ToTitleCase(job.name);
                        jobMatch.location = myTI.ToTitleCase(job.location);
                        jobMatch.jobType = job.jobType;
                        jobMatch.salaryMin = job.salaryMin;
                        jobMatch.salaryMax = job.salaryMax;
                        jobMatch.logo = job.logo;
                        jobMatch.jobID = job.jobID;
                    }
                    jobMatchList.Add(jobMatch);
                }
                jobMatches.Jobs = jobMatchList;
            }
            jobMatchesList.Add(jobMatches);
            return jobMatchesList;
        }

        public static bool ClearJobMatches()
        {
            notajobb_devEntities db = new notajobb_devEntities();
            //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [JobMatches]");
            var jobMatches = db.JobMatches.ToList();
            foreach (var jobMatch in jobMatches)
            {
                jobMatch.active = false;
            }
            db.SaveChanges();
            return true;
        }
        public EditUser GetUser(string email)
        {
            EditUser user = new EditUser();
            var najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            user.UserID = najbUser.ID;
            user.FirstName = najbUser.firstname;
            user.LastName = najbUser.lastname;
            user.Email = najbUser.email;
            user.Image = najbUser.userImage;

            user.HasPassword = HasPassword(email);

            //var jobSearches = db.JobSearches.Where(j => j.NAJB_user.email == email).ToList();

            //List<JobSearches> jobs = new List<JobSearches>();
            //foreach (var job in jobSearches)
            //{
            //    jobs.Add(new JobSearches(job.ID, job.location, job.companyName, job.industry, job.jobType, job.experience, job.jobName, job.radius, job.salaryMin, job.salaryMax, job.active));
            //}

            //user.JobSearches = jobs;

            //var jobMatches = db.JobMatches.Where(j => j.NAJB_user.email == email).ToList();

            //List<JobMatchFavourites> matches = new List<JobMatchFavourites>();
            //foreach (var match in jobMatches)
            //{
            //    matches.Add(new JobMatchFavourites(match.ID, match.location, match.companyName, match.industry, match.jobType, match.jobType, match.name, match.salaryMin, match.salaryMax, (bool)match.active, (bool)match.favourite));
            //}

            //user.JobMatchFavourites = matches;

            //var jobMatchFavs = db.JobMatches.Where(j => j.NAJB_user.email == email && j.favourite == true).ToList();

            //List<JobMatchFavourites> matchFavs = new List<JobMatchFavourites>();
            //foreach (var match in jobMatchFavs)
            //{
            //    matchFavs.Add(new JobMatchFavourites(match.ID, match.location, match.companyName, match.industry, match.jobType, match.experience, match.name, match.salaryMin, match.salaryMax, (bool)match.active, (bool)match.favourite));
            //}

            //user.JobMatchFavourites = matchFavs;

            //var jobsList = db.Jobs.ToList();

            //List<Jobs> allJobs = new List<Jobs>();
            //foreach (var job in allJobs)
            //{
            //    allJobs.Add(new Jobs());
            //}

            //user.Jobs = allJobs;

            //List<Company> allCompanies = db.Companies.ToList();
            //user.Companies = allCompanies;

            //List<JobMatch> jobMatch = db.JobMatches.Where(jm => jm.email == email && jm.ignore == false).ToList();
            //user.JobMatch = jobMatch;
            return user;
        }

        public ViewUser ChangeUserPassword(string email)
        {
            ViewUser user = new ViewUser();
            NAJB_user najbUser = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            user.UserID = najbUser.ID;
            user.FirstName = najbUser.firstname;
            user.LastName = najbUser.lastname;
            user.Email = najbUser.email;
            user.Image = najbUser.userImage;

            var jobSearches = db.JobSearches.Where(j => j.NAJB_user.email == email).ToList();

            List<JobSearches> jobs = new List<JobSearches>();
            foreach (var job in jobSearches)
            {
                jobs.Add(new JobSearches(job.ID, job.location, job.companyName, job.industry, job.jobType, job.experience, job.jobName, job.radius, job.salaryMin, job.salaryMax, job.active));
            }

            user.JobSearches = jobs;

            var jobMatches = db.JobMatches.Where(j => j.NAJB_user.email == email && j.favourite == true).ToList();

            List<JobMatchFavourites> matches = new List<JobMatchFavourites>();
            foreach (var match in jobMatches)
            {
                matches.Add(new JobMatchFavourites(match.ID, match.location, match.companyName, match.industry, match.jobType, match.experience, match.name, match.salaryMin, match.salaryMax, (bool)match.active, (bool)match.favourite));
            }

            user.JobMatchFavourites = matches;

            var jobsList = db.Jobs.ToList();

            List<Jobs> allJobs = new List<Jobs>();
            foreach (var job in allJobs)
            {
                allJobs.Add(new Jobs());
            }

            user.Jobs = allJobs;

            List<Company> allCompanies = db.Companies.ToList();
            user.Companies = allCompanies;

            return user;
        }

        public ViewCompany GetUserCompany(string email)
        {
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
                userCompany.Credits = (int)company.credits;
                userCompany.Website = company.website;
                userCompany.CompanyDescription = company.description;
            }
            else
            {
                userCompany.CompanyName = null;
                userCompany.Jobs = null;
                return userCompany;
            }

            var today = DateTime.Now;
            var jobs = db.Jobs.Where(j => j.Company.name == company.name && (j.jobEndDate < today)).ToList();
            var oldJobs = db.Jobs.Where(j => j.jobEndDate > today).ToList();

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



        public bool DeactivateUserSearches(string email)
        {
            var user = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

            var searches = db.JobSearches.Where(s => s.najb_userID == user.ID).ToList();

            foreach (var search in searches)
            {
                search.active = false;
            }

            try
            {
                db.SaveChanges();
                return true;
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
            return true;
        }

        public bool ActivateJobSearch(int id)
        {
            var jobSearch = db.JobSearches.Where(j => j.ID == id).FirstOrDefault();
            jobSearch.active = true;

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        public bool DeactivateJobSearch(int id)
        {
            var jobSearch = db.JobSearches.Where(j => j.ID == id).FirstOrDefault();
            jobSearch.active = false;

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        return false;
                    }
                }
            }

            return true;
        }

        //public bool AddCredits(IPN ipn, string email)
        //{
        //    var company = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault();

        //    if(company.Transactions.Count() == 0)
        //    {
        //        company.credits += (2*(Int32.Parse(ipn.custom)));
        //    }
        //    else 
        //    {
        //        company.credits += Int32.Parse(ipn.custom);
        //    }


        //    db.SaveChanges();

        //    if(AddTransaction(ipn, email))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}





        //public bool AddTransaction(IPN ipn, string email)
        //{
        //    Transaction transaction = new Transaction();

        //    transaction.PayPalID = ipn.transactionID;
        //    transaction.CompanyID = db.Companies.Where(c => c.NAJB_user.email == email).FirstOrDefault().ID;
        //    transaction.Credits = Int32.Parse(ipn.custom);
        //    transaction.Dollars = (decimal)ipn.amount;
        //    transaction.TransactionDate = DateTime.Now;

        //    db.Transactions.Add(transaction);
        //    db.SaveChanges();

        //    return true;
        //}



        public bool SaveFavourite(int id, string email)
        {
            var favourite = db.JobMatches.Where(j => j.jobID == id && j.NAJB_user.email == email).FirstOrDefault();
            if (favourite == null)
            {
                JobMatch jobMatch = new JobMatch();
                var job = db.Jobs.Where(j => j.ID == id).FirstOrDefault();
                var user = db.NAJB_user.Where(u => u.email == email).FirstOrDefault();

                jobMatch.active = true;
                jobMatch.applied = false;
                jobMatch.Company = job.Company;
                jobMatch.companyID = job.companyID;
                jobMatch.companyName = job.Company.name;
                jobMatch.description = job.description;
                jobMatch.email = email;
                jobMatch.endDate = job.jobEndDate;
                jobMatch.experience = job.experience;
                jobMatch.firstname = user.firstname;
                jobMatch.favourite = true;
                jobMatch.ignore = false;
                jobMatch.industry = job.industry;
                jobMatch.jobDate = job.jobDate;
                jobMatch.jobID = job.ID;
                jobMatch.lastname = user.lastname;
                jobMatch.location = job.location;
                jobMatch.logo = job.Company.logo;
                jobMatch.NAJB_user = user;
                jobMatch.name = job.name;
                jobMatch.salaryMax = job.salaryMax;
                jobMatch.salaryMin = job.salaryMin;

                db.JobMatches.Add(jobMatch);
                db.SaveChanges();
                return true;
            }
            else
            {
                favourite.favourite = true;
                db.SaveChanges();
                return true;
            }

        }

        public bool UnSaveFavourite(int id, string email)
        {
            var favourite = db.JobMatches.Where(j => j.ID == id && j.NAJB_user.email == email).FirstOrDefault();
            favourite.favourite = false;

            db.SaveChanges();

            return true;
        }

        public bool SaveIgnore(int id, string email)
        {
            var ignore = db.JobMatches.Where(j => j.jobID == id && j.NAJB_user.email == email).FirstOrDefault();
            ignore.ignore = true;

            db.SaveChanges();

            return true;
        }


        public Task SendAsync(int id, string email)
        {
            var job = db.Jobs.Where(j => j.ID == id).FirstOrDefault();
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
                new System.Net.Mail.MailMessage(sentFrom, email);
            var image = "http://dev.notajobboard.com/Content/" + job.Company.logo;
            mail.Subject = "A friend has shared a NAJB job with you.";
            mail.Body = "<div style='box-shadow: 0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12); box-sizing: border-box;'><div style='padding:25px; color: black!important; background-color: white!important;'><img src='http://dev.notajobboard.com/Content/images/NAJB_Logo_Web.png' style='height: 50px;'><p class='lead'>The best way to connect job searchers with job hirers.</p></div><div style='padding:25px;'><p><strong>Hello " + email + "!  A friend wants you to look at this job:</strong></p></div><div style='box-sizing: border-box; padding:25px; border-top: 1px solid rgba(0,0,0,.14); border-bottom: 1px solid rgba(0,0,0,.14);'><img src='" + image + "' alt='image logo' style='display:inline-block; padding:10px;  margin:0px; max-height: 100px; max-width: 120px;'/><p style='display:inline-block; padding:10px; margin:0px;'>Job Name: " + "<a href='http://dev.notajobboard.com/Home/Job?id=" + job.ID.ToString() + "'>" + job.name + "</a></p>" + "<p style='display:inline-block; padding:10px;  margin:0px;'>Company Name: " + job.Company.name + "</p>" + "<p style='display:inline-block; padding:10px;  margin:0px;'>Industry: " + job.industry + "</p>" + "<p style='display:inline-block; padding:10px;  margin:0px;'>Location: " + job.location + "</p>" + "<p style='display:inline-block; padding:10px;  margin:0px;'>Job Type: " + job.jobType + "</p><br />" + "<p style='display:inline-block; padding:10px;  margin:0px;'>Description: " + job.description + "</p>" + "<a style='background: 0 0; border: none; border-radius: 2px; color: #000; position: relative; height: 36px; margin: 0; min-width: 64px; padding: 0 16px; display: inline-block; font-family: 'Roboto','Helvetica','Arial',sans-serif; font-size: 14px; font-weight: 500; text-transform: uppercase; letter-spacing: 0; overflow: hidden; transition: box-shadow .2s cubic-bezier(.4,0,1,1),background-color .2s cubic-bezier(.4,0,.2,1),color .2s cubic-bezier(.4,0,.2,1); outline: none; cursor: pointer; text-decoration: none; text-align: center; line-height: 36px; vertical-align: middle; background: rgba(158,158,158,.2); box-shadow: 0 2px 2px 0 rgba(0,0,0,.14),0 3px 1px -2px rgba(0,0,0,.2),0 1px 5px 0 rgba(0,0,0,.12); color: rgb(255,255,255); background-color: rgb(255,82,82);' href='http://dev.notajobboard.com/Home/Job?id=" + job.ID.ToString() + "'>View Job</a></div><div style='padding:10px;'><p style='font-size: 10px;'>Copyright NAJB Inc. 2016</p><p style='font-size: 10px;'>All rights reserved.</p></div></div>";
            mail.IsBodyHtml = true;

            // Send:
            return client.SendMailAsync(mail);
        }

        public bool HasPassword(string email)
        {
            var user = db.AspNetUsers.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
    }
}