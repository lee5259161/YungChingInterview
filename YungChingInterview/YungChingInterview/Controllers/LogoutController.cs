using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YungChingInterview.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            TempData["Logout"] = "Logout";
            return RedirectToAction("Index", "Home");
        }
    }
}