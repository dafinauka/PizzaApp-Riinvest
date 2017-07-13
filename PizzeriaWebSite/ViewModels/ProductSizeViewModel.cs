using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.ViewModels
{
    public class ProductSizeViewModel
    {
        public List<Size> liSize { get; set; }
        public Product prod { get; set; }
        public decimal cmimi { get; set; }
    }
}