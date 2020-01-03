using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using KardoPasso.Models;

namespace KardoPasso.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
        public ActionResult Index()
        {
            if (Request.Cookies["KardoLanguagePreference"] == null || Request.Cookies["KardoLanguagePreference"].Value == "" || Request.Cookies["KardoLanguagePreference"].Value == "null")
            {
                Session.Add("KardoLanguagePreference", "tr-TR");
                if (Request.Cookies["KardoLanguagePreference"] != null)
                {
                    Response.Cookies["KardoLanguagePreference"].Value = "tr-TR";
                    Response.Cookies["KardoLanguagePreference"].Expires = DateTime.Now.AddYears(99);
                }
            }
            if ((Session["KardoUserId"] == null && Session["KardoUserName"] == null))
            {
                if (Request.Cookies["KardoUserInfos"] != null)
                {
                    if (Request.Cookies["KardoUserInfos"]["userId"] != null && Request.Cookies["KardoUserInfos"]["status"] != null)
                    {
                        string[] savedAccounts = Request.Cookies["KardoUserInfos"]["userId"].Split('_');
                        string[] status = Request.Cookies["KardoUserInfos"]["status"].Split('_');
                        //string[] priority = Request.Cookies["KardoUserInfos"]["priority"].Split('_');
                        if (savedAccounts.Length == status.Length)
                        {
                            string tempUserId = null;
                            for (int i = 0; i < savedAccounts.Length; i++)
                            {
                                if (status[i] == "active")
                                {
                                    tempUserId = savedAccounts[i];
                                    break;
                                }
                            }

                            if (tempUserId != null)
                            {
                                /*KardoModel kardoModel = new KardoModel("kardo");
                                kardoModel.selectOnlyFirstData("users", "*");
                               kardoModel.addInnerJoin("users", "profils", "userId");*/
                                string tempStr = UseKardoEncryption.getDecipherString(Request.Cookies["KardoUserInfos"]["userId"]);
                                if (tempStr == "KardoEncryptionError")
                                {
                                    Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddDays(-1);
                                    Response.Redirect("/Default/Index");
                                }
                                 else
                                {
                                    int userId = Convert.ToInt32(tempStr);
                                    Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
                                    string userName = (string)theUser["username"];
                                    
                                    Session.Add("KardoUserId", Request.Cookies["KardoUserInfos"]["userId"]);
                                    Session.Add("KardoUserName", UseKardoEncryption.getEncryptedString(userName, 50));
                                    Session.Add("KardoLanguagePreference", (string)theUser["languagePreference"]);

                                    ViewBag.userInfos = theUser;
                                }

                            }
                        }
                        else
                        {
                            Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddDays(-1);
                        }

                    }
                    else
                    {
                        Response.Cookies["KardoUserInfos"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
                /*else if (Request.Cookies["KardoVisitorInfos"] != null && (Session["KardoVisitorId"] == null && Session["KardoVisitorName"] == null))
                {
                    KardoModel kardoModel = new KardoModel("kardo");
                    kardoModel.selectOnlyFirstData("visitors", "*");
                    string tempStr = UseKardoEncryption.getDecipherString(Request.Cookies["KardoVisitorInfos"]["visitorId"]);
                    if (tempStr == "KardoEncryptionError")
                    {
                        Response.Cookies["KardoVisitorInfos"].Expires = DateTime.Now.AddDays(-1);
                        Response.Redirect("/Kardo/Index");
                    }
                    else
                    {
                        kardoModel.addWhere("visitorId", Convert.ToInt32(tempStr));
                        ArrayList results = kardoModel.selectResults();
                        kardoModel.close();

                        string visitorName = KardoModel.getFirstValueFromColumnName(results, "visitorName");

                        Session.Add("KardoVisitorId", Request.Cookies["KardoVisitorInfos"]["visitorId"]);
                        Session.Add("KardoVisitorName", visitorName);
                        Session.Add("KardoLanguagePreference", Request.Cookies["KardoLanguagePreference"].Value);
                    }
                }*/
                else
                {
                    // will be creat new visitor // first visit or cookieEnabled is false
                }
            }
            else if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);

                ViewBag.userInfos = theUser;
            }

            //****************************************************

            Dictionary<string, List<Object>> events = DefaultModel.getEvents();
            ViewBag.Events = events;
            Dictionary<string, List<Object>> eventsCities = DefaultModel.getEventsCities();
            ViewBag.EventsCities = eventsCities;
            Dictionary<string, List<Object>> eventsTeams = DefaultModel.getEventsTeams();
            ViewBag.EventsTeams = eventsTeams;


            return View();
        }
        public bool creatNewVisitor()
        {
            string ipAddress = Request.Form["ipAddress"];
            string country = Request.Form["country"];
            string city = Request.Form["city"];
            string loc = Request.Form["loc"];
            string hostname = Request.Form["hostname"];
            string language = Request.Form["language"];
            string nowDate = KardoStaticMethods.getNowDateTimeUTC();

            return runCreatNewVisitor(ipAddress, country, city, loc, hostname, language, nowDate);
        }
        private bool runCreatNewVisitor(string ipAddress, string country, string city, string loc, string hostname, string language, string nowDate)
        {
            /*controlVisitorDatas(ipAddress, country, city, loc, hostname, language, nowDate);
            string[] columns = new string[] { "visitorName", "roleId", "ipAddress", "country", "city", "location", "hostname", "languagePreference", "createdDate", "deletedFlag", "visitorStatus" };
            ArrayList values = new ArrayList();
            values.Add("v-" + KardoStaticMethods.creatUniqString(30));
            values.Add(1);
            values.Add(ipAddress);
            values.Add(country);
            values.Add(city);
            values.Add(loc);
            values.Add(hostname);
            values.Add(language);
            values.Add(nowDate);
            values.Add(0);
            values.Add(0);

            KardoModel kardoModel = new KardoModel("kardo");
            int visitorId = kardoModel.insertDataAndReturnId("visitors", columns, values);
            kardoModel.close();

            if (visitorId != -1)
            {
                Response.Cookies["KardoLanguagePreference"].Value = (language != "too long") ? language : "tr-TR";
                Response.Cookies["KardoLanguagePreference"].Expires = DateTime.Now.AddYears(99);
                Response.Cookies["KardoVisitorInfos"]["visitorId"] = UseKardoEncryption.getEncryptedString(visitorId, 50);
                Response.Cookies["KardoVisitorInfos"].Expires = DateTime.Now.AddYears(99);
                Session.Add("KardoLanguagePreference", (language != "too long") ? language : "tr-TR");
                Session.Add("KardoVisitorId", visitorId);
                return true;
            }
            return false;*/
            return false;
        }

        public bool sessionsControl()
        {
            if ((Session["KardoUserId"] == null && Session["KardoUserName"] == null) || (Session["KardoVisitorId"] == null && Session["KardoVisitorName"] == null))
            {
                if (Request.Cookies["KardoVisitorInfos"] == null && Request.Cookies["KardoUserInfos"] == null)
                {
                    return false;
                }
            }
            return true;
        }

        private void controlVisitorDatas(string ipAddress, string country, string city, string loc, string hostname, string language, string nowDate)
        {
            ipAddress = (ipAddress == null || ipAddress.Length > 15) ? "invalid" : ipAddress;
            country = (country == null || country.Length > 16) ? "invalid" : country;
            city = (city == null || city.Length > 16) ? "invalid" : city;
            loc = (loc == null || loc.Length > 15) ? "invalid" : loc;
            hostname = (hostname == null || hostname.Length > 50) ? "invalid" : hostname;
            language = (language == null || language.Length > 16) ? "invalid" : language;
            nowDate = (nowDate == null || nowDate.Length > 32) ? "invalid" : nowDate;
        }

        // For Main_layout
        public string getOtherAccounts()
        {
            string[] userIds = Request.Form["userIds"].Split('_');
            return runGetOtherAccounts(userIds);
        }
        private string runGetOtherAccounts(string[] userIds)
        {
            string loginedAccount = "";
            if (Session["KardoUserId"] != null)
                loginedAccount = UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString());
            for (int i = 0; i < userIds.Length; i++)
            {
                userIds[i] = UseKardoEncryption.getDecipherString(userIds[i]);
                if (loginedAccount != "" && loginedAccount == userIds[i])
                    userIds[i] = "-1";
            }

            string str = "";
            for (int i = 0; i < userIds.Length; i++) 
            {
                if (str == "")
                    str += userIds[i];
                else
                    str += "," + userIds[i];
            }
                

            Dictionary<string, List<Object>> users = LoginModel.getUsersFromUserIds(str);

            string json = KardoStaticMethods.getJSONFromDic(users);

            return json;
        }
        public Dictionary<string, Object> getUserInfos(int userId) 
        {
            return DefaultModel.getUserFromUserId(userId);
        }
        public string getFilteredEvents()
        {
            string city = Request.Form["city"];
            string team = Request.Form["team"];
            string startDate = Request.Form["startDate"];
            string endDate = Request.Form["endDate"];

            return DefaultModel.getFilteredEvents(city, team, startDate, endDate);
        }

        public bool returnTrue() { return true; }
	}
}