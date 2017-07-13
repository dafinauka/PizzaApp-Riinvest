using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PizzeriaWebSite.ViewModels;

namespace PizzeriaWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(vm.Email);
                mm.To.Add("dafinauka5@gmail.com");
                mm.Subject = vm.Subject;

                mm.Body = "<b> Sender Name: </b>" + vm.Name +
                    "<br><b> Sender Email: </b>" + vm.Email +
                    "<br><b> Message: </b>" + vm.Message;
                mm.IsBodyHtml = true;


                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("dafinauka5@gmail.com", "duffyiphone6");
                smtpClient.Send(mm);

            }
            ModelState.Clear();
            return View();
        }
    }
}