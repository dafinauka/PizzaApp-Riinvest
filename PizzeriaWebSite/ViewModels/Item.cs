using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.ViewModels
{
    public class Item
    {
        public Product_Size product { get; set; }
        public int Quantity { get; set; }
    }
}