using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace YungChingInterview.Controllers
{
    public class StoreInfoController : Controller
    {
        // GET: StoreInfo
        public ActionResult Index()
        {
            string uid = User.Identity.Name;
            string sessionAccess = Session[uid]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.SessionAccess = sessionAccess;
            return View();
        }
    }
}