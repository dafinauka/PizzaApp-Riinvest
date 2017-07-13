using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.ViewModels
{
    public class UserProfile
    {
        public User useri;
        public List<Order> orders; 
    }
}