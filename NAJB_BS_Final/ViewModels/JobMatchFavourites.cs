using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class JobMatchFavourites
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        public string JobName { get; set; }
        public string SalaryMin { get; set; }
        public string SalaryMax { get; set; }
        public bool Favourite { get; set; }

        public bool Active { get; set; }
        public JobMatchFavourites()
        {

        }
        public JobMatchFavourites(int id, string location, string companyName, string industry, string jobType, string experience, string jobName, string salaryMin, string salaryMax, bool active, bool favourite)
        {
            ID = id;
            Location = location;
            CompanyName = companyName;
            Industry = industry;
            JobType = jobType;
            Experience = Experience;
            JobName = jobName;
            SalaryMin = salaryMin;
            SalaryMax = salaryMax;
            Active = active;
            Favourite = favourite;
        }
    }
}