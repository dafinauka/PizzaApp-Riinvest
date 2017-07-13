using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaWebSite.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required.", AllowEmptyStrings = false)]
        [Display (Name ="Username: ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        [Display(Name = "Remember Me: ")]
        public bool RememberMe { get; set; }
    }
}