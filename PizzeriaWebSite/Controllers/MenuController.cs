using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;
using PizzeriaWebSite.ViewModels;

namespace PizzeriaWebSite.Controllers
{
    public class MenuController : Controller
    {
        PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Menu
        public ActionResult PizzaMenu()
        {
            ViewBag.SizeList = db.Sizes.Where(n => n.SizeID == 1 || n.SizeID == 2 || n.SizeID == 3 || n.SizeID == 4).ToList();
            var menulist = db.Products.Where(x => x.CategoryID == 1 && x.IsActive == true).ToList();
            return View(menulist);
        }

        public ActionResult OthersMenu()
        {
            ViewBag.SizeList = db.Sizes.Where(s => s.SizeID == 5).SingleOrDefault();
            var otherLsit = db.Products.Where(x => (x.CategoryID == 2 || x.CategoryID == 3 || x.CategoryID == 4) && x.IsActive == true).ToList();
            return View(otherLsit);
        }

        public ActionResult DrinksMenu()
        {
            ViewBag.SizeList = db.Sizes.Where(s => s.SizeID == 5).SingleOrDefault();
            var drinklist = db.Products.Where(x => x.CategoryID == 5 && x.IsActive == true).ToList();
            return View(drinklist);
        }

        public ActionResult CreateYourOwn(int id)
        {
            var pizza = db.Product_Size.Where(c => c.ProdSizeID == id).SingleOrDefault();
            return View(pizza);
        }

        [HttpPost]
        public ActionResult CreateYourOwn(List<string> food)
        {
            List<string> toppings = new List<string>();
            toppings.AddRange(food);
            Session["toppings"] = toppings;
            return RedirectToAction("CreateYourOwn");
        }

        public ActionResult ProductDetailsView(FormCollection form)
        {
            int sizeID = Convert.ToInt32(form["size"].ToString());
            int productID = Convert.ToInt32(form["productID"].ToString());

            if (sizeID != 0)
            {
                var size = db.Product_Size.Where(x => x.ProductID == productID && x.SizeID == sizeID).FirstOrDefault();

                ViewBag.Emri = db.Products.Where(m => m.ProductID == productID).Select(c => c.Name).First();
                var ingredients = db.Product_Ingredients.Where(c => c.ProductID == productID && c.Ingredient.IsActive == true).ToList();

                ViewBag.SS = db.Product_Size.Where(m => m.ProductID == productID && m.SizeID == sizeID).Select(c => c.Size.SizeDesc).FirstOrDefault();

                var viewModel = new ProductDetails
                {
                    prodSize = size,
                    liProdIng = ingredients

                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}