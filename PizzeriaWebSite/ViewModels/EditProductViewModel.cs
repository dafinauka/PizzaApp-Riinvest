using PizzeriaWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaWebSite.ViewModels
{
    public class EditProductViewModel
    {
        public List<Product_Ingredients> liIng { get; set; }
        public Product prod { get; set; }
    }
}