//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzeriaWebSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Ingredients
    {
        public int Prod_Ing_ID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> IngredientID { get; set; }
    
        public virtual Ingredient Ingredient { get; set; }
        public virtual Product Product { get; set; }
    }
}
