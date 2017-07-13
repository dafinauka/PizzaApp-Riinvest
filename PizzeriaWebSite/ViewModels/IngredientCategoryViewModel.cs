using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.ViewModels
{
    public class IngredientCategoryViewModel
    {
        public List<Category> listCat { get; set; }
        public Ingredient ingID { get; set; }
    }
}