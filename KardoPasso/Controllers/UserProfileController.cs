using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;

namespace KardoPasso.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/
        public ActionResult Index()
        {

            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);

                ViewBag.userInfos = theUser;
                ViewBag.userTickets = UserProfileModel.getUserTickets((int)theUser["userId"]);
                
            }
            else 
            {
                return Redirect("/");
            }

            return View();
        }

        public bool insertMoney() 
        {
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            string moneyStr = Request.Form["money"];

            int money;
            bool isNumeric = int.TryParse(moneyStr, out money);
            if (isNumeric)
                return UserProfileModel.insertMoney(userId, money);
            return false;
        }
        public bool updateDues()
        {
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            return UserProfileModel.updateDues(userId);
        }
        public bool updatePassword()
        {
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            string newPassword = Request.Form["newPassword"];
            string passEncrypted = UseKardoEncryption.getEncryptedString(newPassword,65);
            if (passEncrypted.Length > 255)
                return false;
            return UserProfileModel.updatePassword(userId, passEncrypted);
        }
        public bool updateProfile()
        {
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            string name = Request.Form["name"];
            string surname = Request.Form["surname"];
            string mail = Request.Form["mail"];
            string phone = Request.Form["phone"];
            string countryCode = Request.Form["countryCode"];
            return UserProfileModel.updateProfile(userId, name, surname, mail, phone, countryCode);
        }
        public bool getKardoPasso()
        {
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            return UserProfileModel.getKardoPasso(userId);
        }
	}
}