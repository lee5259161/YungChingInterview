using Model;
using Service.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace YungChingInterview.Controllers
{
    public class HomeController : Controller
    {

        private IUserService _IUserService;
        private IUtilService _IUtilService;

        public HomeController(IUserService UserService, IUtilService UtilService)
        {
            _IUserService = UserService;
            _IUtilService = UtilService;
        }

        //GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string UserID, string Password)
        {


            bool loginPass = false;
            UserDetail user = null;

            if (UserID=="admin" && Password == "admin")
            {

                Session[UserID] = "CRUD";
                loginPass = true;
            }
            else
            {
                user = _IUserService.GetUserByID(UserID);

                if (user != null)
                {
                    string checkPassword = _IUtilService.encryptPWD(UserID, Password);

                    if (checkPassword == user.Password)
                    {
                        loginPass = true;
                    }
                }

            }

            if (loginPass)
            {
                var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: UserID, //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: true,// 是否要記住我 true or false
                userData: UserID, //可以放使用者角色名稱
                cookiePath: FormsAuthentication.FormsCookiePath);

                var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);

                if (UserID != "admin")
                {
                    Session[UserID] = user.Access;
                }

                FormsAuthentication.RedirectFromLoginPage
                    (UserID, true);
                return RedirectToAction("StoreInfoView", "StoreInfo");
            }
            ViewBag.IsLogin = true;


            return View();
        }

    }
}