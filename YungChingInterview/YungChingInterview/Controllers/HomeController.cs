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
        //GET: Home
        public ActionResult Index()
        {
            //Session.RemoveAll();

            return View();
        }
        [HttpPost]
        public ActionResult Index(string UserID, string Password)
        {

            //登入驗證
            bool loginPass = false;

            if(UserID=="admin" && Password == "admin")
            {
                //將群組權限放進session
                Session[UserID] = "CRUD";
                loginPass = true;
            }


            //string checkPwd = UtilHelper.encryptPWD(UserID, decryptPWD);

            //SqlConnection conn = DBHelper.DBConnection();
            //SqlCommand sqlCommand = new SqlCommand(@"SELECT A.[UserID], A.[UserName], A.[Email], 
            //                                        A.[Password], B.[GroupID],B.[Access] 
            //                                        FROM UserDetail A ,UserGroupList B 
            //                                        WHERE A.UserID = B.UserID 
            //                                        AND A.UserID=@id 
            //                                        AND A.IsDelete = 0 AND A.Password=@password ");
            //sqlCommand.Parameters.Add(new SqlParameter("@id", UserID));
            //sqlCommand.Parameters.Add(new SqlParameter("@password", checkPwd));
            //sqlCommand.Connection = conn;
            //UserDetailVM user = new UserDetailVM();

            ////開啟資料庫

            //conn.Open();

            //SqlDataReader reader = sqlCommand.ExecuteReader();
            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        user = new UserDetailVM
            //        {
            //            UserID = reader.GetString(reader.GetOrdinal("UserID")),
            //            UserName = reader.GetString(reader.GetOrdinal("UserName")),
            //            Email = reader.GetString(reader.GetOrdinal("Email")),
            //            Password = reader.GetString(reader.GetOrdinal("Password")),
            //            UserGroupId = reader.GetString(reader.GetOrdinal("GroupID")),
            //        };
            //        if (!reader.IsDBNull(reader.GetOrdinal("Access")))
            //        {
            //            user.Access = reader.GetString(reader.GetOrdinal("Access"));
            //        }
            //    }
            //    loginPass = true;


            //}
            //else
            //{
            //    loginPass = false;
            //}

            //conn.Close();
            //conn.Dispose();

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
                    //將群組權限放進session
                    //Session[UserID] = user.UserGroupId + "," + user.Access;
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