using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using KardoPasso.Models;


namespace Kardo.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            if (Response.Cookies["KardoLanguagePreference"] == null)
            {
                Session.Add("KardoLanguagePreference", "tr-TR"); // change "tr-TR" with a method // if this method runs, visitor can't allow cookies or it visites firstly 
            }
            //sessionsControl();
            ViewBag.redirect = "True";
            return View();
        }
        public ActionResult popupIndex()
        {
            sessionsControl();
            ViewBag.redirect = "False";
            return View("Index");
        }
        public string findUser()
        {
            //sessionsControl();
            string userLoginKey = Request.Form["user_login_key"];
            if (userLoginKey == null || userLoginKey == "")
                return "False";

            string user_name_type = Request.Form["user_name_type"];
            if (user_name_type == "email")
            {
                if (userLoginKey.Length > 100 || userLoginKey.Length < 6) //spam girdi engelleme
                    return "False";
            }
            else
            {
                if (userLoginKey.Length > 16 || userLoginKey.Length < 6) //spam girdi engelleme
                    return "False";
            }
            return runFindUser(userLoginKey);//, user_name_type
        }
        private string runFindUser(string userLoginKey)
        {
            Dictionary<string, List<Object>> theUser = LoginModel.getUserFromUsernameOrMail(userLoginKey);

            if (theUser.Count == 0)
                return "False";
            else
            {
                if (controlAuthentications((int) theUser["userId"][0]) == "showCaptcha")
                    return "showCaptcha";
                else if (controlAuthentications((int)theUser["userId"][0]) == "banned30Min")
                    return "banned30Min";
                return "True";
            }
        }

        public string controlLogin() 
        {
            //sessionsControl();
            string userLoginKey = Request.Form["user_login_key"];
            string remember_me = Request.Form["remember_me"];
            string session_checkbox = Request.Form["session_checkbox"];
            string password = Request.Form["password"];
            /*string ipAddress = Request.Form["ipAddress"];
            string country = Request.Form["country"];
            string city = Request.Form["city"];
            string loc = Request.Form["loc"];
            string hostname = Request.Form["hostname"];
            string nowDate = KardoStaticMethods.getNowDateTimeUTC();*/
            string accountUUID = Request.Form["account_uuid"];

            return runControlLogin(userLoginKey, remember_me, session_checkbox, password);//, ipAddress, country, city, loc, hostname, nowDate
        }
        private string runControlLogin(string userLoginKey, string remember_me, string session_checkbox, string password)//, string ipAddress, string country, string city, string loc, string hostname, string nowDate
        {
            if (password.Length > 16 && !(password.Equals(userLoginKey + "_doNotControl")))// add && password.Length < 6
            {
                password = "invalid"; //spam girdi engelleme
                return "invalidPassword";
            }
            //controlLoginDatas(ipAddress, country, city, loc, hostname, nowDate);

            Dictionary<string, List<Object>> theUser = LoginModel.getUserFromUsernameOrMail(userLoginKey);
            
            string receivedPassword="";
           if (password.Equals(userLoginKey + "_doNotControl")) // User has changed account.
            {
                if (theUser.Count == 0) {return "False"; }
                receivedPassword = "_haveNotBeenControled";
            }
            else
            {
                if (theUser.Count == 0) { return "False"; }
                receivedPassword = UseKardoEncryption.getDecipherString(theUser["password"][0].ToString());
            }

           int userId = (int)theUser["userId"][0];
            string cAut = controlAuthentications(userId);
            if (cAut == "banned30Min")
            {
                return "banned30Min";
            }

            if (password.Equals(receivedPassword) || (password.Equals(userLoginKey + "_doNotControl")))
            {
                setAuthentication(userId, UseKardoEncryption.getEncryptedString(password,65), true);
                if (Session["KardoVisitorId"] != null && Session["KardoVisitorName"] != null)
                {
                    Session["KardoVisitorId"] = null;
                    Session["KardoVisitorName"] = null;
                }
                if (Session["KardoUserId"] != null && Session["KardoUserName"] != null && Session["KardoLanguage"] != null)
                {
                    //There is a account that is logined
                    Session["KardoUserId"] = null;
                    Session["KardoUserName"] = null;
                    Session["KardoLanguage"] = null;
                }

                Session.Add("KardoUserId", UseKardoEncryption.getEncryptedString(userId,50));//*UUID
                Session.Add("KardoUserName", UseKardoEncryption.getEncryptedString(userLoginKey,50));
                Session.Add("KardoLanguage", theUser["languagePreference"][0].ToString());
                if (Request.Cookies["KardoUserInfos"] != null)
                {
                    //KardoUserInfos cookie bir veya daha fazla userId içerebilir. status açık bırakılmak istenen hesaba ulaşmamızı sağlar.
                    //Ayrı userId'ler birbirinden '_' ile ayrılı
                    string[] savedAccounts = Request.Cookies["KardoUserInfos"]["userId"].Split('_');
                    string[] status = Request.Cookies["KardoUserInfos"]["status"].Split('_');
                    string[] priorities = Request.Cookies["KardoUserInfos"]["priority"].Split('_');
                    int tempUserId = -1;
                    for (int i = 0; i < savedAccounts.Length; i++ )
                    {
                        //Giriş yapımaya çalışan hesaba daha önce giriş yapıldığını anlamak için kontrol yapılır.
                        string receviedId = UseKardoEncryption.getDecipherString(savedAccounts[i]);
                        if (receviedId == "KardoEncryptionError")
                        {
                            Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddDays(-1);
                            Response.Redirect("/Kardo/Index");
                            break;
                        }
                        if (receviedId != "")
                        {
                            if (Convert.ToInt32(receviedId) == userId)//*UUID
                            {
                                tempUserId = i;
                                //status[i] = "active";
                                //priorities[i] = "1";
                                break;
                            }
                        }
                    }
                    if (tempUserId == -1)
                    {
                        //Daha önceden hesap mevcut fakat yeni bir hesap ekleniyor.
                        if (remember_me == "checked")
                        {
                            string statusString = "", priorityString = "";
                            if (session_checkbox == "checked")
                            {
                                for (int i = 0; i < status.Length; i++)
                                {
                                    statusString += (i == 0) ? "passive" : "_passive";
                                }
                            }
                            for (int i = 0; i < priorities.Length; i++ ) 
                            {
                                priorityString += (i == 0) ? "0" : "_0";
                            }

                            Response.Cookies["KardoUserInfos"]["userId"] = Request.Cookies["KardoUserInfos"]["userId"] + "_" + UseKardoEncryption.getEncryptedString(userId.ToString(),50);//*UUID
                            Response.Cookies["KardoUserInfos"]["status"] = (session_checkbox == "checked") ? statusString + "_active" : Request.Cookies["KardoUserInfos"]["status"] + "_passive";
                            Response.Cookies["KardoUserInfos"]["priority"] = priorityString + "_1";
                            Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddYears(99);
                            Response.Cookies["KardoLanguagePreference"].Value = theUser["languagePreference"][0].ToString();
                            Response.Cookies["KardoLanguagePreference"].Expires = DateTime.Now.AddYears(99);
                        }
                    }
                    else 
                    {
                        //Daha önce giriş yapılmış hesaba tekrar geçiş yapılıyor.
                        string userIdString = "", statusString = "", priorityString = "";
                        for (int i = 0; i < savedAccounts.Length; i++)
                        {
                            userIdString += (i == 0) ? savedAccounts[i] : "_" + savedAccounts[i];
                            statusString += (i == 0) ? ((i == tempUserId) ? ((session_checkbox == "checked") ? "active" : "passive") : "passive") : ((i == tempUserId) ? ((session_checkbox == "checked") ? "_active" : "_passive") : "_passive");
                            priorityString += (i == 0) ? ((i == tempUserId) ? "1" : "0") : ((i == tempUserId) ? "_1" : "_0");
                        }
                        Response.Cookies["KardoUserInfos"]["userId"] = userIdString;
                        Response.Cookies["KardoUserInfos"]["status"] = statusString;
                        Response.Cookies["KardoUserInfos"]["priority"] = priorityString;
                        Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddYears(99);
                        Response.Cookies["KardoLanguagePreference"].Value = theUser["languagePreference"][0].ToString();
                        Response.Cookies["KardoLanguagePreference"].Expires = DateTime.Now.AddYears(99);
                    }
                }
                else
                {
                    //İlkkez giriş yapılıyor veya kullanıcı cookie oluşturulmasına izin vermiyor.
                    if (remember_me == "checked")
                    {
                        Response.Cookies["KardoUserInfos"]["userId"] = UseKardoEncryption.getEncryptedString(userId.ToString(),50);//*UUID
                        Response.Cookies["KardoUserInfos"]["status"] = (session_checkbox == "checked") ? "active" : "passive";
                        Response.Cookies["KardoUserInfos"]["priority"] = "1";
                       Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddYears(99);
                        Response.Cookies["KardoLanguagePreference"].Value = theUser["languagePreference"][0].ToString();
                        Response.Cookies["KardoLanguagePreference"].Expires = DateTime.Now.AddYears(99);
                     }
                }
                return "True";
            }
            else
            {
                setAuthentication(userId, UseKardoEncryption.getEncryptedString(password,65), false);
                return controlAuthentications(userId);
            }
        }
        private string controlAuthentications(int userId) 
        {
            //be controlled last of 5 authentication that is last in 30 minutes. 
            //If there is a anormal stution, will be done captcha verification
            Dictionary<string, List<Object>> dic = LoginModel.getAuthentications(userId);
            if (dic.Count == 0)
                return "False";

            int authentications = dic["authenticationId"].Count;
            
            if (authentications >= 3 && authentications < 5)
                return "showCaptcha";
            if (authentications >= 5)
                return "banned30Min";
            return "False";
        }
        private void controlLoginDatas(string ipAddress, string country, string city, string loc, string hostname, string nowDate) 
        {
            //MySqlException'ın önüne geçmek için girdiler kontrol edilir.
            ipAddress = (ipAddress == null || ipAddress.Length > 15) ? "invalid" : ipAddress;
            country = (country == null || country.Length > 16) ? "invalid" : country;
            city = (city == null || city.Length > 16) ? "invalid" : city;
            loc = (loc == null || loc.Length > 15) ? "invalid" : loc;
            hostname = (hostname == null || hostname.Length > 50) ? "invalid" : hostname;
            nowDate = (nowDate == null || nowDate.Length > 23) ? "invalid" : nowDate;
        }

        private bool setAuthentication(int userId, string password, Boolean status)
        {
            string ipAdd = Request.UserHostAddress;
            string hostName = Request.UserHostName;
            if (hostName.Length > 255)
                hostName = "NULL";

            return LoginModel.setAuthentication(ipAdd, "NULL", "NULL", "NULL", hostName, password, status, userId);
        }

        public string controlUseEma() 
        {
            string userName = Request.Form["userName"];
            string eMail = Request.Form["eMail"];
            return runControlUseEma(userName, eMail);
        }

        private string runControlUseEma(string userName, string eMail)
        {
            if (LoginModel.isUsernameAvaible(userName))
                return "userName";
            if (LoginModel.isEMailAvaible(eMail))
                return "eMail";

            return "True";
        }
        public string completeRegistry() 
        {
            string userName = Request.Form["userName"];
            string eMail = Request.Form["eMail"];
            string password = Request.Form["password"];
            string rName = Request.Form["rName"];
            string sName = Request.Form["sName"];
            string day = Request.Form["day"];
            string month = Request.Form["month"];
            string year = Request.Form["year"];
            string nowDate = KardoStaticMethods.getNowDateTimeUTC();
            string languagePreference = Request.Form["languagePreference"];

            return runCompleteRegistry(userName, eMail, password, rName, sName, day, month, year, nowDate, languagePreference);
        }
        private string runCompleteRegistry(string userName, string eMail, string password, string rName, string sName, string day, string month, string year, string nowDate, string languagePreference) 
        {
            string date = year + "-" + month + "-" + day;
            controlRegistryValues(userName, eMail, password, rName, sName, date , nowDate, languagePreference);
            if (userName == "invalid" || eMail == "invalid" || password == "invalid" || rName == "invalid" || sName == "invalid" || date == "invalid")
                return "errorValues";
            
            bool result = LoginModel.insertUser(
                userName.ToLower(), 
                eMail.ToLower(), 
                UseKardoEncryption.getEncryptedString(password,65), 
                KardoStaticMethods.FirstCharToUpper(rName), 
                KardoStaticMethods.FirstCharToUpper(sName), 
                date,
                languagePreference
                );

            if(!result)
                return "False";


            int userId = (int) LoginModel.getUserFromUsernameOrMail(userName)["userId"][0];

            string activationKey = UseKardoEncryption.getEncryptedString(userId,50) + "-" + KardoStaticMethods.creatUniqString(36);//(min)50+1+36 char

            LoginModel.setAKeyForEMail(userId, activationKey);
            
            string verifyAccountLink = "/Login/verifyAccount?activationKey=" + activationKey;
            return "True";
        }

        private void controlRegistryValues(string userName, string eMail, string password, string rName, string sName, string date , string nowDate, string languagePreference) 
        {
            userName = (userName == null || userName.Length > 16 || userName.Length < 6) ? "invalid" : userName;
            eMail = (eMail == null || eMail.Length > 100 || eMail.Length < 6) ? "invalid" : eMail;
            password = (password == null || password.Length > 16 || password.Length < 6) ? "invalid" : password;
            rName = (rName == null || rName.Length > 32 || rName.Length < 2) ? "invalid" : rName;
            sName = (sName == null || sName.Length > 32 || sName.Length < 2) ? "invalid" : sName;
            date = (date == null || date.Length > 10) ? "invalid" : date;
            nowDate = (nowDate == null || nowDate.Length > 23) ? "invalid" : nowDate;
            languagePreference = (languagePreference == null || languagePreference.Length > 16) ? "tr-TR" : languagePreference;
        }

        // Verify account
        public ActionResult verifyAccount(string activationKey) 
        {
            return runVerifyAccount(activationKey);
        }
        private ActionResult runVerifyAccount(string activationKey)
        {
            if (activationKey == null || activationKey == "")
                return View();//"wrongKey"

            string[] splitedKey = activationKey.Split('-');
            if (activationKey.Length < 86 || splitedKey.Length != 3 || splitedKey[0].Length < 50)
                return View();//"wrongKey"
            else {
                string tempUserId = UseKardoEncryption.getDecipherString(splitedKey[0]);
                if (tempUserId == "KardoEncryptionError")
                {
                    ViewBag.verifyResult = "KardoEncryptionError";
                    return View();
                }
                else
                {
                    int tempUserIdInt;
                    if(!int.TryParse("123", out tempUserIdInt))
                    {
                        ViewBag.verifyResult = "errorUserId";
                        return View();
                    }

                    Dictionary<string,List<Object>> theUser = LoginModel.getUserFromUserId(tempUserIdInt);
                 
                    if (theUser.Count == 0)
                    {
                        ViewBag.verifyResult = "errorUserId";
                        return View();
                    }
                    if((int) theUser["activated"][0] == 1)
                    {
                        ViewBag.verifyResult = "verifiedAccount";
                        return View();
                    }
                    if (activationKey == (String) theUser["activationKey"][0])
                    {
                        //UPDATE mails activationKey -> null & add activationDate & mailStatus -> 0
                        bool result = LoginModel.verifyEMail((int) theUser["userId"][0]);
                        if(result)
                        {
                            ViewBag.verifyResult = "verified";
                            return View();
                        }
                        else
                        {
                            ViewBag.verifyResult = "wrongKey";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.verifyResult = "wrongKey";
                        return View();
                    }
                }
            }
        }

        private void sessionsControl()
        {
            //Session'lar kontrol edilir session mevcutsa anasayfaya yönlendirme yapılır.
            //Fonksiyonlara direk ulaşmayı engeller.
            if (Request.Cookies["KardoUserInfos"] != null)
            {
                string[] status = Request.Cookies["KardoUserInfos"]["status"].Split('_');
                foreach (string item in status)
                {
                    if (item == "active")
                    {
                        if (Session["KardoUserId"] == null && Session["KardoUserName"] == null)
                        {
                            Response.Redirect("/Default/Index");
                        }
                    }
                }
            } 
            if (Session["KardoUserId"] != null && Session["KardoUserName"] != null)
            {
                Response.Redirect("/Default/Index");
            }
        }
        public bool sessionsControlBool()
        {
            //Session'lar kontrol edilir session mevcutsa True döndürülür.
            if (Request.Cookies["KardoUserInfos"] != null)
            {
                string[] status = Request.Cookies["KardoUserInfos"]["status"].Split('_');
                foreach (string item in status)
                {
                    if (item == "active")
                        return true;
                }
                if (Session["KardoUserId"] == null && Session["KardoUserName"] == null)
                {
                    return true;
                }
            }
            if (Session["KardoUserId"] != null && Session["KardoUserName"] != null)
            {
                return true;
            }
            return false;
        }

        public void clearSessions() 
        {
            Session.Abandon();
            if (Request.Cookies["KardoUserInfos"] != null)
            {
                string userIdString = Request.Cookies["KardoUserInfos"]["userId"];
                string[] savedUserIds = userIdString.Split('_');
                string statusString = "", savedUserIdString = "";
                for (int i = 0; i < savedUserIds.Length; i++ )
                {
                    savedUserIdString += (i == 0) ? savedUserIds[i] : "_" + savedUserIds[i];
                    statusString += (i == 0) ? "passive" : "_passive";
                }
                Response.Cookies["KardoUserInfos"]["userId"] = savedUserIdString;
                Response.Cookies["KardoUserInfos"]["status"] = statusString;
                Response.Cookies["KardoUserInfos"]["priority"] = Request.Cookies["KardoUserInfos"]["priority"];
                Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddYears(99);
            }
        }


        //changeAccount
        public string changeAccount() {
            string UUID = Request.Form["uuid"];
            if(UUID != null || UUID.Length > 0)
            {
                Dictionary<string, List<Object>> theUser = LoginModel.getUserFromUUID(UUID);
                if(theUser.Count == 0)
                    return "errorUUID";


                string userLoginKey = (string)theUser["username"][0]; 
                string remember_me = "checked";

                string session_checkbox = "unchecked"; 
                string[] status = Request.Cookies["KardoUserInfos"]["status"].Split('_');
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "active")
                    {
                        session_checkbox = "checked";
                        break;
                    }
                }

                string password = userLoginKey + "_doNotControl";
                string ipAddress = Request.Form["ipAddress"];
                string country = Request.Form["country"];
                string city = Request.Form["city"];
                string loc = Request.Form["loc"];
                string hostname = Request.Form["hostname"];
                string nowDate = KardoStaticMethods.getNowDateTimeUTC();
                runControlLogin(userLoginKey, remember_me, session_checkbox, password);//, ipAddress, country, city, loc, hostname, nowDate
                return "True";
            }
            return "errorUUID";
        }
	}
}