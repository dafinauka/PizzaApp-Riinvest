using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.Controllers
{
    public class Product_IngredientsController : Controller
    {
        private PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Product_Ingredients
        public ActionResult Index()
        {
            var product_Ingredients = db.Product_Ingredients.Include(p => p.Ingredient).Include(p => p.Product);
            return View(product_Ingredients.ToList());
        }

        // GET: Product_Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Ingredients product_Ingredients = db.Product_Ingredients.Find(id);
            if (product_Ingredients == null)
            {
                return HttpNotFound();
            }
            return View(product_Ingredients);
        }

        // GET: Product_Ingredients/Create
        public ActionResult Create()
        {
            ViewBag.IngredientID = new SelectList(db.Ingredients, "IngredientID", "IngredientDesc");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: Product_Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Prod_Ing_ID,ProductID,IngredientID")] Product_Ingredients product_Ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Product_Ingredients.Add(product_Ingredients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientID = new SelectList(db.Ingredients, "IngredientID", "IngredientDesc", product_Ingredients.IngredientID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_Ingredients.ProductID);
            return View(product_Ingredients);
        }

        // GET: Product_Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Ingredients product_Ingredients = db.Product_Ingredients.Find(id);
            if (product_Ingredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientID = new SelectList(db.Ingredients, "IngredientID", "IngredientDesc", product_Ingredients.IngredientID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_Ingredients.ProductID);
            return View(product_Ingredients);
        }

        // POST: Product_Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Prod_Ing_ID,ProductID,IngredientID")] Product_Ingredients product_Ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Ingredients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientID = new SelectList(db.Ingredients, "IngredientID", "IngredientDesc", product_Ingredients.IngredientID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", product_Ingredients.ProductID);
            return View(product_Ingredients);
        }

        // GET: Product_Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Ingredients product_Ingredients = db.Product_Ingredients.Find(id);
            if (product_Ingredients == null)
            {
                return HttpNotFound();
            }
            return View(product_Ingredients);
        }

        // POST: Product_Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Ingredients product_Ingredients = db.Product_Ingredients.Find(id);
            db.Product_Ingredients.Remove(product_Ingredients);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
