using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NAJB_BS_Final.ViewModels
{
    public class Jobs
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string DescriptionTrimmed { get { return Description.Substring(0, 10) + "..."; } }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Job Type")]
        public string Experience { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }

        public string SalaryMin { get; set; }

        public string SalaryMax { get; set; }

        public Jobs()
        {

        }
        public Jobs(int id, string name, string description, string companyName, string location, string reference, string industry, string jobType, string experience, bool active, string salaryMin, string salaryMax)
        {
            ID = id;
            Name = name;
            Description = description;
            CompanyName = companyName;
            Location = location;
            Reference = reference;
            Industry = industry;
            JobType = jobType;
            Experience = experience;
            Active = active;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
        }
    }
}