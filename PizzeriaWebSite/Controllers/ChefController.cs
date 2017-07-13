using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.Controllers
{
    public class ChefController : Controller
    {
        PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Chef
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["userId"]);
            var center = db.User_Center.Where(u => u.UserID.Equals(id)).Select(u=>u.CenterID).SingleOrDefault();
            var orders = db.Orders.Where(x => x.IsActive == true && x.CenterID == center).ToList();

            return View(orders);
        }

        public ActionResult OrderDetails(int id)
        {
            ViewBag.Address = db.Orders.Where(i => i.OrderID == id).Select(i => i.Address).SingleOrDefault();
            ViewBag.Price = db.OrderDetails.Where(c => c.OrderID == id).Sum(i => i.PricePerProduct);
            ViewBag.User = db.Orders.Where(u => u.OrderID == id).Select(u => u.User.Phone).SingleOrDefault();
            ViewBag.Center = db.Orders.Where(u => u.OrderID == id).Select(u => u.Center.CenterAddress).SingleOrDefault();
            ViewBag.OderdID = db.Orders.Where(x => x.OrderID == id).Select(x => x.OrderID).SingleOrDefault();          
            var orderDetails = db.OrderDetails.Where(c => c.OrderID == id).ToList();

            return View(orderDetails);
        }

        public ActionResult AllOrders()
        {
            var allorders = db.Orders.ToList();
            return View(allorders);
        }

        public ActionResult Done(int id)
        {
            if (ModelState.IsValid)
            {
                Order o = db.Orders.Find(id);
                o.IsActive = false;
                db.SaveChanges();
                Session.Remove("toppings");
            }
            return RedirectToAction("Index");
        }
    }
}