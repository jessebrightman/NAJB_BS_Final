using NAJB_BS_Final.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NAJB_BS_Final.ViewModels
{
    public class EditUser : IndexViewModel
    {
        public int UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Profile Image")]
        public string Image { get; set; }

        public string Resume { get; set; }

        public EditUser() : base() { }
    }
}