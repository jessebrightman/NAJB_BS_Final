using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAJB_BS_Final.ViewModels
{
    public class AddJob
    {
        [Required]
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Required]
        [Display(Name = "Industry")]
        public string Industry { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please enter valid Location from the List.")]
        [RegularExpression("^[a-zA-Z]+(, )[A-Z]{2}(, )[a-zA-Z]+$")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Reference Number")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Salary Minimum")]
        public string SalaryMin { get; set; }

        [Display(Name = "Salary Maximum")]
        public string SalaryMax { get; set; }

        [Display(Name = "Apply To")]
        public string ApplyTo { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }

        public Company Company { get; set; }
        public AddJob() { }
    }
}