using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzeriaWebSite.ViewModels
{
    public class ContactUsViewModel
    {
        [Required (ErrorMessage ="This field is required")]
        [StringLength(20)]
        public string Name { get; set; }
        [Required (ErrorMessage = "This field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required (ErrorMessage = "This field is required") ]
        public string Subject { get; set; }
        [Required (ErrorMessage = "This field is required")]
        public string Message { get; set; }
    }
}