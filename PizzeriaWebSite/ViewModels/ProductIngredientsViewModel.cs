using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.ViewModels
{
    public class ProductIngredientsViewModel
    {
        public List<Ingredient> liIngcat { get; set; }
        public Product prod { get; set; }
    }
}