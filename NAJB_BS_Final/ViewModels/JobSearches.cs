using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class JobSearches
    {
        public int ID { get; set; }

        [Required]
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        [Required]
        public string JobName { get; set; }
        public string Radius { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }

        public bool Active { get; set; }
        public JobSearches()
        {

        }
        public JobSearches(int id, string location, string companyName, string industry, string jobType, string experience, string jobName, string radius, string salaryMin, string salaryMax, bool active)
        {
            ID = id;
            Location = location;
            CompanyName = companyName;
            Industry = industry;
            JobType = jobType;
            Experience = experience;
            JobName = jobName;
            Radius = radius;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            Active = active;
        }
    }
}