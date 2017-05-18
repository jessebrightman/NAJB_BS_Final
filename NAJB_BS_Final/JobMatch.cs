//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NAJB_BS_Final
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobMatch
    {
        public int ID { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string location { get; set; }
        public string industry { get; set; }
        public string jobType { get; set; }
        public string salaryMin { get; set; }
        public string salaryMax { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string companyName { get; set; }
        public string logo { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<bool> favourite { get; set; }
        public Nullable<bool> ignore { get; set; }
        public Nullable<int> jobID { get; set; }
        public int NAJB_userID { get; set; }
        public Nullable<int> companyID { get; set; }
        public Nullable<bool> applied { get; set; }
        public string experience { get; set; }
        public Nullable<System.DateTime> jobDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<System.DateTime> applyDate { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual NAJB_user NAJB_user { get; set; }
    }
}