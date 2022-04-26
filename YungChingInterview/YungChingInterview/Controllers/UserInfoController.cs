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

        public UserInfoController(IUserService UserService)
        {
            _UserService = UserService;
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