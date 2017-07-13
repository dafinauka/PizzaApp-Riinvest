using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.Models;
using PizzeriaWebSite.ViewModels;
using System.Web.Security;
using System.IO;

namespace PizzeriaWebSite.Controllers
{
    public class AccountController : Controller
    {
        PizzaDemoDBEntities db = new PizzaDemoDBEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login l, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var isValidUser = Membership.ValidateUser(l.Username, l.Password);
                if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        Session["userId"] = db.Users.Where(x => x.Username.Equals(l.Username) && x.Password.Equals(l.Password)).Select(c => c.UserID).FirstOrDefault();
                        Session["username"] = l.Username;
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session["userId"] = db.Users.Where(x => x.Username.Equals(l.Username) && x.Password.Equals(l.Password)).Select(c => c.UserID).FirstOrDefault();
                        Session["username"] = l.Username;
                        var role = db.Users.Where(x => x.Username.Equals(l.Username) && x.Password.Equals(l.Password)).Select(x => x.RoleID).SingleOrDefault();
                        if (role == 1)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (role == 2)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (role == 3)
                        {
                            return RedirectToAction("Index", "Chef");
                        }
                    }
                }
            }

            ModelState.Remove("Password");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Register
        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Role1");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,Surname,Image,Phone,Email,Username,Password,ConfirmPassword,RoleID")] User user, HttpPostedFileBase photofile)
        {
            if (ModelState.IsValid)
            {
                if (photofile != null)
                {
                    var fileName = Path.GetFileName(photofile.FileName);
                    var path = Path.Combine(Server.MapPath("/Images/Users/"), fileName);
                    photofile.SaveAs(path);

                    user.Image = fileName;
                    user.RoleID = 2;
                }

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Role1", user.RoleID);
            return View(user);
        }

        #endregion

        [Authorize]
        public ActionResult ClientProfile()
        {
            int id = Convert.ToInt32(Session["userId"]);
            var user = db.Users.Where(x => x.UserID == id).SingleOrDefault();
            var ordersDone = db.Orders.Where(c => c.ClientID == id).ToList();

            var viewModel = new UserProfile
            {
                useri = user,
                orders = ordersDone
            };

            return View(viewModel);
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
            return RedirectToAction("ClientProfile");
        }

    }
}