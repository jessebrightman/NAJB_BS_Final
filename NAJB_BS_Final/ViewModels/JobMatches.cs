using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class JobMatches
    {
        public string Email { get; set; }
        public List<JobMatch> Jobs { get; set; }

        public JobMatches()
        {

        }
    }
}