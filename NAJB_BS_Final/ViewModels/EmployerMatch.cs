using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class EmployerMatch
    {
        public JobMatch JobMatch { get; set; }
        public List<NAJB_user> MatchedUsers { get; set; }

        public EmployerMatch() { }

        public EmployerMatch(JobMatch jobMatch, List<NAJB_user> matchedUsers)
        {
            JobMatch = jobMatch;
            MatchedUsers = matchedUsers;
        }
    }
}