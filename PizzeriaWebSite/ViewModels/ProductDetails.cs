using PizzeriaWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaWebSite.ViewModels
{
    public class ProductDetails
    {
        public Product_Size prodSize { get; set; }
        public List<Product_Ingredients> liProdIng { get; set; }
    }
}