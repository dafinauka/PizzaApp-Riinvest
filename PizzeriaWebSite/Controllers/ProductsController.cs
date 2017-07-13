using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;
using System.IO;
using PizzeriaWebSite.ViewModels;

namespace PizzeriaWebSite.Controllers
{
    public class ProductsController : Controller
    {
        private PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }
        [HttpPost]
        public ActionResult Add(int id,int catID)
        {
            //int id = Convert.ToInt32(form["productID"].ToString());
            //int id2 = Convert.ToInt32(form["category"].ToString());
        
            var lista = db.Ingredient_Category.Where(i => i.CategoryID == catID).Select(c => c.Ingredient).ToList();
            var product = db.Products.Where(i => i.ProductID == id).SingleOrDefault();
            var viewModel = new ProductIngredientsViewModel
            {
                liIngcat = lista,
                prod = product
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(List<int> liIngcat, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(form["productID"].ToString());
                foreach (var i in liIngcat)
                {
                    Product_Ingredients pi = new Product_Ingredients();
                    pi.ProductID = id;
                    pi.IngredientID = i;
                    db.Product_Ingredients.Add(pi);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditIng(int id)
        {
            var list = db.Product_Ingredients.Where(x => x.ProductID == id).ToList();
            var product = db.Products.Where(i => i.ProductID == id).SingleOrDefault();
            var viewModel = new EditProductViewModel
            {
                liIng = list,
                prod = product
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveEditIng(int id, int ingID)
        {
            if (ModelState.IsValid)
            {
                var ingredient = db.Product_Ingredients.Where(c => c.IngredientID == ingID && c.ProductID == id).Select(x=>x.Prod_Ing_ID).SingleOrDefault();
                Product_Ingredients ing = db.Product_Ingredients.Find(ingredient);
                db.Product_Ingredients.Remove(ing);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult AddSize(int id)
        {
            var size = db.Sizes.ToList();
            var prodId = db.Products.Where(i => i.ProductID == id).SingleOrDefault();

            var viewModel = new ProductSizeViewModel
            {
                liSize = size,
                prod = prodId
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SaveSize(ProductSizeViewModel k, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(form["productID"].ToString());
                int sizeID = int.Parse(form["size"].ToString());
                //decimal price = Convert.ToDecimal(form["cmimi"].ToString());
                Product_Size ps = new Product_Size();
                ps.ProductID = id;
                ps.SizeID = sizeID;
                ps.Price = k.cmimi;
                db.Product_Size.Add(ps);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Image,CategoryID,IsActive")] Product product, HttpPostedFileBase photofile)
        {
            if (ModelState.IsValid)
            {

                if (photofile != null)
                {
                    var fileName = Path.GetFileName(photofile.FileName);
                    var path = Path.Combine(Server.MapPath("/Images/Products/"), fileName);
                    photofile.SaveAs(path);

                    product.Image = fileName;

                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Description", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Description", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Image,CategoryID,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Description", product.CategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            //db.Products.Remove(product);
            product.IsActive = false;
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
