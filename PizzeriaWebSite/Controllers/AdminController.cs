using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PizzeriaWebSite.Models;

namespace PizzeriaWebSite.Controllers
{
    public class AdminController : Controller
    {
        PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChefsIndex()
        {
            var user_centers = db.User_Center.Include(u => u.User).Include(u => u.Center);
            return View(user_centers.ToList());
        }

        public ActionResult ChefToCenters()
        {
            ViewBag.Centers = db.Centers.ToList();
            ViewBag.User = db.Users.Where(u => u.RoleID == 3).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult ChefToCenters(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int chef = Convert.ToInt32(form["chef"].ToString());
                int center = Convert.ToInt32(form["center"].ToString());

                User_Center uc = new User_Center();
                uc.UserID = chef;
                uc.CenterID = center;
                db.User_Center.Add(uc);
            }
            db.SaveChanges();

            return View("Index");
        }
    }
}