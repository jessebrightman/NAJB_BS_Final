using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAJB_BS_Final.ViewModels
{
    public class EditCompany
    {
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

        public EditCompany() { }
    }
}