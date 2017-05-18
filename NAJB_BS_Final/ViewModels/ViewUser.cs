using NAJB_BS_Final.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class ViewUser : IndexViewModel
    {
        public int UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Profile Image")]
        public string Image { get; set; }

        public string UserImage { get; set; }

        [Display(Name = "Upload Image")]
        public HttpPostedFileBase File { get; set; }

        public string Resume { get; set; }

        // Job Searches Info 

        public virtual List<JobSearches> JobSearches { get; set; }

        [Display(Name = "Active")]
        public string Active { get; set; }

        [Display(Name = "Industry")]
        public string Industry { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Radius")]
        public string Radius { get; set; }

        [Display(Name = "Salary Minimum")]
        public string SalaryMin { get; set; }

        [Display(Name = "Salary Maximum")]
        public string SalaryMax { get; set; }

        [Display(Name = "Additional Locations")]
        public string AdditionalLocations { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }

        // Job Matches Info
        public virtual List<JobMatches> JobMatches { get; set; }

        public virtual List<JobMatch> JobMatch { get; set; }
        public virtual List<JobMatchFavourites> JobMatchFavourites { get; set; }

        //[Display(Name = "Favourite")]
        //public string Favourite { get; set; }

        //[Display(Name = "Industry")]
        //public string JMIndustry { get; set; }

        //[Display(Name = "Company Name")]
        //public string JMCompanyName { get; set; }

        //[Display(Name = "Job Name")]
        //public string JMJobName { get; set; }

        //[Display(Name = "Job Type")]
        //public string JMJobType { get; set; }

        //[Display(Name = "Location")]
        //public string JMLocation { get; set; }

        //[Display(Name = "Radius")]
        //public string JMRadius { get; set; }

        //[Display(Name = "Salary Minimum")]
        //public string JMSalaryMin { get; set; }

        //[Display(Name = "Salary Maximum")]
        //public string JMSalaryMax { get; set; }

        // Job Info

        [Display(Name = "Job")]
        public Job Job { get; set; }

        public virtual List<Jobs> Jobs { get; set; }

        [Display(Name = "Company")]
        public Company Company { get; set; }

        public virtual List<Company> Companies { get; set; }

        public ViewUser() : base()
        {

        }
    }
}