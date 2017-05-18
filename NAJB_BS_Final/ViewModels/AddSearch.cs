using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class AddSearch
    {
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        [Display(Name = "Industry")]
        public string Industry { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Salary Minimum")]
        public string SalaryMin { get; set; }

        [Display(Name = "Salary Maximum")]
        public string SalaryMax { get; set; }

        [Required(ErrorMessage = "Please enter valid Location from the List.")]
        [RegularExpression("^[a-zA-Z]+(, )[A-Z]{2}(, )[a-zA-Z]+$")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Radius")]
        public string Radius { get; set; }

        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }

        [Display(Name = "Additional Locations")]
        public string AdditionalLocations { get; set; }

        public AddSearch() { }
    }
}