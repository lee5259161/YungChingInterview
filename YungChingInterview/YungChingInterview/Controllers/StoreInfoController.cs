using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Model;
using PagedList;
using Service.InterFace;

namespace YungChingInterview.Controllers
{
    public class StoreInfoController : Controller
    {

        private IStoreService _StoreService;

        public StoreInfoController(IStoreService StoreService)
        {
            _StoreService = StoreService;            
        }


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
        // GET: StoreInfo
        [Authorize]
        public ActionResult StoreInfoView(string query = null, int page = 1, int pageSize = 10)
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

            //要放到Pagedlist的query
            ViewBag.Query = query;


            List<StoreInfoDetail> storeList = _StoreService.GetStoreInfoList(query);


            var result = storeList.ToPagedList(page, pageSize);
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

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Create(StoreInfoDetail newStore)
        {
            string uid = User.Identity.Name;

            ReturnResult result = _StoreService.CreateStoreInfo(newStore,uid);

            return RedirectToAction("StoreInfoView");
        }
        [Authorize]
        public ActionResult Edit(int sid)
        {
            string uid = User.Identity.Name;
            string sessionAccess = Session[uid]?.ToString() ?? "";
            if (sessionAccess.IsNullOrWhiteSpace())
            {
                return RedirectToAction("Index", "Home");
            }
            if (!sessionAccess.Contains("U"))
            {
                return RedirectToAction("Index", "PermissionErrorMsg", new { msg = "您的身份無修改的權限" });
            }

            StoreInfoDetail storeInfo = _StoreService.GetStoreInfoByID(sid);
                                
            return View(storeInfo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(StoreInfoDetail storeInfo)
        {
            string uid = User.Identity.Name;

            ReturnResult result = _StoreService.UpdateStoreInfo(storeInfo, uid);

            return RedirectToAction("StoreInfoView");
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int sid)
        {

            ReturnResult result = _StoreService.DeleteStoreInfo(sid);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}