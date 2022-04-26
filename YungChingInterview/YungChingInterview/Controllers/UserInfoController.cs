using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using PagedList;
using Service.InterFace;
using YungChingInterview.Models;

namespace YungChingInterview.Controllers
{
    public class UserInfoController : Controller
    {

        private IUserService _UserService;
        private IUtilService _IUtilService;

        public UserInfoController(IUserService UserService, IUtilService UtilService)
        {
            _UserService = UserService;
            _IUtilService = UtilService;
        }

        [Authorize]
        public ActionResult UserInfoView(string query = null, int page = 1, int pageSize = 10)
        {
            string uid = User.Identity.Name;
            string sessionAccess = Session[uid]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("R"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無查詢的權限" });
            }
            ViewBag.SessionAccess = sessionAccess;

            ViewBag.Query = query;

            List<UserDetail> userList = _UserService.GetUserList(query);

            var result = userList.OrderBy(x => x.UserID).ToPagedList(page, pageSize);

            return View(result);
        }

        [Authorize]
        public ActionResult Create()
        {
            string uid = User.Identity.Name;
            string sessionAccess = Session[uid]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("C"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無新增的權限" });
            }

            InitAccessElement();

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(UserDetail newUser, List<string> selectedItem)
        {
            string uid = User.Identity.Name;
            UserDetail oldUser = _UserService.GetUserByID(newUser.UserID);

            if (oldUser != null)
            {
                ViewBag.DuplicateUid = "*此帳號已經有人使用!!";
                InitAccessElement();
                return View(newUser);
            }

            ReturnResult result = _UserService.CreateUser(newUser, selectedItem, uid);

            return RedirectToAction("UserInfoView");
        }
        [Authorize]
        public ActionResult Edit(string uid)
        {
            string userId = User.Identity.Name;
            string sessionAccess = Session[userId]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("U"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無編輯的權限" });
            }

            UserDetail userInfo = _UserService.GetUserByID(uid);

            InitAccessElement();

            return View(userInfo);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(UserDetail userInfo, List<string> selectedItem, string submit)
        {
            string uid = User.Identity.Name;
            if (submit == "2")
            {
                TempData["UID"] = userInfo.UserID;
                return RedirectToAction("ChangePassword");
            }

            ReturnResult result = _UserService.UpdateUser(userInfo, selectedItem, uid);

            return RedirectToAction("UserInfoView");
        }
        [Authorize]
        public ActionResult ChangePassword(string uid)
        {
            string userId = User.Identity.Name;
            string sessionAccess = Session[userId]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            else if (!sessionAccess.Contains("U"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無變更密碼的權限" });
            }

            return View(uid);

        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(string uid, string oldPWD, string newPWD, string newPWDconf)
        {
            string currentUserId = User.Identity.Name;
            if (oldPWD.IsNullOrWhiteSpace() || newPWD.IsNullOrWhiteSpace() || newPWDconf.IsNullOrWhiteSpace())
            {
                return Json(new { success = false, message = "欄位內容不可是空白或空白符號" });
            }

            if (newPWD != newPWDconf)
            {
                return Json(new { success = false, message = "新密碼與新密碼確認不相符" });
            }
            if (oldPWD == newPWD)
            {
                return Json(new { success = false, message = "新密碼與舊密碼不能相同" });
            }

            UserDetail userInfo = _UserService.GetUserByID(uid);

            string checkOldPWD = _IUtilService.encryptPWD(uid, oldPWD);

            if (userInfo.Password != checkOldPWD)
            {
                return Json(new { success = false, message = "舊密碼不正確" });
            }
            else
            {
                string checkPwd = _IUtilService.encryptPWD(uid, newPWD);
                userInfo.Password = checkPwd;

                //更新密碼
                ReturnResult result =_UserService.UpdateUserPassword(userInfo, currentUserId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [HttpPost]
        public ActionResult DisableUser(string uid)
        {
            string currentUserId = User.Identity.Name;
            string sessionAccess = Session[currentUserId]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("D"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無修改帳號狀態的權限" });
            }


            ReturnResult result = _UserService.DisableUser(uid, currentUserId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EnableUser(string uid)
        {
            string currentUserId = User.Identity.Name;
            string sessionAccess = Session[currentUserId]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("D"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無修改帳號狀態的權限" });
            }

            ReturnResult result = _UserService.EnableUser(uid, currentUserId);

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        private void InitAccessElement()
        {

            CheckBox accessCB = new CheckBox();
            accessCB.ItemList = new List<SelectListItem>
            {
                new SelectListItem {Text = "新增", Value = "C"},
                new SelectListItem {Text = "查詢", Value = "R"},
                new SelectListItem {Text = "修改", Value = "U"},
                new SelectListItem {Text = "刪除", Value = "D"},
            };

            ViewBag.AccessCB = accessCB;

        }
    }
}