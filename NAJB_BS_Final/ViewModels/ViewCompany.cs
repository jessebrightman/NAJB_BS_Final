using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NAJB_BS_Final.Models;

namespace NAJB_BS_Final.ViewModels
{
    public class ViewCompany : IndexViewModel
    {
        //User Data

        public int UserID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Image2 { get; set; }

        public string UserImage { get; set; }

        //Company Data
        public Company Company { get; set; }

        public int CompanyID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Industry { get; set; }

        [Required]
        public string Website { get; set; }

        public string Contact { get; set; }

        public string Phone { get; set; }
        public int Credits { get; set; }

        [AllowHtml]
        [Display(Name = "Company Description")]
        public string CompanyDescription { get; set; }


        [Display(Name = "Upload Image")]
        //[DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public string CompanyImage { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase File { get; set; }

        //Job Data

        public virtual List<Jobs> Jobs { get; set; }

        public virtual List<Job> Job { get; set; }

        [Required]
        [Display(Name = "Job Name")]
        public string JobName { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }


        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public string Date { get; set; }

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

        // Searchers
        public virtual List<NAJB_user> Users { get; set; }

        //Transaction Data
        public virtual List<Transaction> Transactions { get; set; }

        public int TransactionID { get; set; }
        public string PayPalID { get; set; }
        public int TransactionCompanyID { get; set; }
        public string TransactionCredits { get; set; }
        public string Dollars { get; set; }
        public DateTime TransactionDate { get; set; }

        public ViewCompany() : base()
        {

        }
    }
}