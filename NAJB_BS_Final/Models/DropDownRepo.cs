using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NAJB_BS_Final.Models
{
    public class DropDownRepo
    {
        public static IEnumerable<SelectListItem> GetUserTypes()
        {
            var userTypeNames = new List<SelectListItem>();

            userTypeNames.Add(new SelectListItem { Text = "Searcher", Value = "Searcher" });
            userTypeNames.Add(new SelectListItem { Text = "Employer", Value = "Employer" });

            return userTypeNames;
        }

        public static IEnumerable<SelectListItem> GetJobTypes()
        {
            var userTypeNames = new List<SelectListItem>();

            userTypeNames.Add(new SelectListItem { Text = "Full-Time", Value = "Full-Time" });
            userTypeNames.Add(new SelectListItem { Text = "Part-Time", Value = "Part-Time" });
            userTypeNames.Add(new SelectListItem { Text = "Casual", Value = "Casual" });
            userTypeNames.Add(new SelectListItem { Text = "Contract", Value = "Contract" });

            return userTypeNames;
        }
    }
}