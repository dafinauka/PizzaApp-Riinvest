using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;
using PizzeriaWebSite.ViewModels;

namespace PizzeriaWebSite.Controllers
{
    public class CartController : Controller
    {
        PizzaDemoDBEntities db = new PizzaDemoDBEntities();//

        public ActionResult Index()
        {
            ViewBag.Vlera = Session["cart"];
            if (Session["cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }
        }

        // GET: Cart
        public ActionResult Cart(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item()
                {
                    product = db.Product_Size.Find(id),
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = Exists(id);
                if (index == -1)
                {
                    cart.Add(new Item()
                    {
                        product = db.Product_Size.Find(id),
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity = cart[index].Quantity + 1;
                }
                Session["cart"] = cart;
            }

            //return RedirectToAction("PizzaMenu", "Menu");
            return View("Index");
        }

        private int Exists(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.ProdSizeID == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public ActionResult Delete(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(id);
            Session["cart"] = cart;
            return View("Index");
        }

        [Authorize]
        public ActionResult Address()
        {
            ViewBag.Category = db.OrderCategories.ToList();
            ViewBag.Centers = db.Centers.ToList();
            return View();
        }

        //[Authorize]
        //public ActionResult PaypalAddress()
        //{
        //    ViewBag.Category = db.OrderCategories.ToList();
        //    ViewBag.Centers = db.Centers.ToList();
        //    return View();
        //}

        public int Create(Order o)
        {
            db.Orders.Add(o);
            db.SaveChanges();
            return o.OrderID;
        }

        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            int category = Convert.ToInt32(form["category"].ToString());
            string address = form["address"].ToString();
            int center = Convert.ToInt32(form["center"].ToString());

            List<Item> cart = (List<Item>)Session["cart"];
            //save order in db
            Order o = new Order();
            o.Address = address;
            o.ClientID = Convert.ToInt32(Session["userId"]);
            o.OrderData = DateTime.Now;
            o.CenterID = center;
            o.CategoryID = category;
            o.IsActive = true;
            int orderId = Create(o);

            //save orderdetails in db
            foreach (var item in cart)
            {
                OrderDetail od = new OrderDetail();
                od.OrderID = orderId;
                od.ProductSizeID = item.product.ProdSizeID;
                od.Quantity = (byte)item.Quantity;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }
            Session.Remove("cart");

            return View("Thanks");
        }

        #region Paypal
        //[HttpPost]
        //public ActionResult CheckoutwithPaypal(FormCollection form)
        //{
        //    int category = 1; //Convert.ToInt32(form["category"].ToString());
        //    string address = form["address"].ToString();
        //    int center = Convert.ToInt32(form["center"].ToString());

        //    List<Item> cart = (List<Item>)Session["cart"];
        //    //save order in db
        //    Order o = new Order();
        //    o.Address = address;
        //    o.ClientID = Convert.ToInt32(Session["userId"]);
        //    o.OrderData = DateTime.Now;
        //    o.CenterID = center;
        //    o.CategoryID = category;
        //    o.IsActive = true;
        //    int orderId = Create(o);

        //    //save orderdetails in db
        //    foreach (var item in cart)
        //    {
        //        OrderDetail od = new OrderDetail();
        //        od.OrderID = orderId;
        //        od.ProductSizeID = item.product.ProdSizeID;
        //        od.Quantity = (byte)item.Quantity;
        //        db.OrderDetails.Add(od);
        //        db.SaveChanges();
        //    }
        //    //Session.Remove("cart");

        //    return RedirectToAction("PaymentWithPaypal", "Paypal");
        //}
        #endregion
    }
}