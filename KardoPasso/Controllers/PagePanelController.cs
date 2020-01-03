using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;
using System.IO;

namespace KardoPasso.Controllers
{
    public class PagePanelController : Controller
    {
        //
        // GET: /PagePanel/
        public ActionResult Index()
        {
            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
                ViewBag.userInfos = theUser;
                
                if (!(bool)theUser["can_enter_pagePanel"])
                {
                    return Redirect("/");
                }

                if ((bool)theUser["can_modify_users"])
                {
                    Dictionary<string, List<Object>> users = PagePanelModel.getUsers();
                    ViewBag.users = users;
                }
            }
            else
            {
                return Redirect("/");
            }

            Dictionary<string, List<Object>> teams = PagePanelModel.getTeams();
            ViewBag.teams = teams;
            Dictionary<string, List<Object>> stadiums = PagePanelModel.getStadiums();
            ViewBag.stadiums = stadiums;
            Dictionary<string, List<Object>> sportTypes = PagePanelModel.getSportTypes();
            ViewBag.sportTypes = sportTypes;
            Dictionary<string, List<Object>> events = PagePanelModel.getEvents();
            ViewBag.events = events;
            Dictionary<string, List<Object>> roles = PagePanelModel.getRoles();
            ViewBag.roles = roles;

            ViewBag.newUsers = PagePanelModel.countNewUsers();
            ViewBag.newKps = PagePanelModel.countNewKps();
            return View();
        }
        [HttpPost]
        public bool insertEvent()
        {
            string fileName = "";
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    string extension = Path.GetExtension(file.FileName);
                    fileName = KardoStaticMethods.createKardoUID() + extension;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/kardoPasso/img/uploads/") + fileName);
                }
            }
            catch 
            {
                return false;
            }

            if (fileName == "")
            {
                return false;
            }

            string hTeamIdS = Request.Form["hTeamId"];
            string oTeamIdS = Request.Form["oTeamId"];
            string stadiumIdS = Request.Form["stadiumId"];
            string eventDate = Request.Form["eventDate"];
            string description = Request.Form["description"];
            string priceS = Request.Form["price"];

            int hTeamId, oTeamId, stadiumId;
            bool isNumeric1 = int.TryParse(hTeamIdS, out hTeamId);
            bool isNumeric2 = int.TryParse(oTeamIdS, out oTeamId);
            bool isNumeric3 = int.TryParse(stadiumIdS, out stadiumId);
            float price;
            bool isNumeric4 = float.TryParse(priceS, out price);

            if (!isNumeric1 || !isNumeric2 || !isNumeric3 || !isNumeric4)
                return false;

            return PagePanelModel.insertEvent(hTeamId, oTeamId, stadiumId, eventDate, description, price, fileName); ;
        }
        [HttpPost]
        public bool updateEvent()
        {
            string fileName = "NULL";
            string updatePic = Request.Form["updatePic"];
            if (updatePic == "true") 
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        //int fileSize = file.ContentLength;
                        string extension = Path.GetExtension(file.FileName);
                        fileName = KardoStaticMethods.createKardoUID() + extension;
                        //string mimeType = file.ContentType;
                        System.IO.Stream fileContent = file.InputStream;
                        file.SaveAs(Server.MapPath("~/Content/kardoPasso/img/uploads/") + fileName);
                    }
                }
                catch
                {
                    return false;
                }
            }
            
            string hTeamIdS = Request.Form["hTeamId"];
            string oTeamIdS = Request.Form["oTeamId"];
            string stadiumIdS = Request.Form["stadiumId"];
            string eventDate = Request.Form["eventDate"];
            string description = Request.Form["description"];
            string priceS = Request.Form["price"];
            string eventIdS = Request.Form["eventId"];

            int hTeamId, oTeamId, stadiumId, eventId;
            bool isNumeric1 = int.TryParse(hTeamIdS, out hTeamId);
            bool isNumeric2 = int.TryParse(oTeamIdS, out oTeamId);
            bool isNumeric3 = int.TryParse(stadiumIdS, out stadiumId);
            bool isNumeric5 = int.TryParse(eventIdS, out eventId);
            float price;
            bool isNumeric4 = float.TryParse(priceS, out price);

            if (!isNumeric1 || !isNumeric2 || !isNumeric3 || !isNumeric4 || !isNumeric5)
                return false;

            return PagePanelModel.updateEvent(hTeamId, oTeamId, stadiumId, eventDate, description, price, fileName, eventId); ;
        }
        public bool deleteEvent()
        {
            string dataidS = Request.Form["dataid"];
            int dataid;
            bool isNumeric = int.TryParse(dataidS, out dataid);

            if (!isNumeric)
                return false;

            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));

            return PagePanelModel.deleteEvent(dataid, userId);
        }
        [HttpPost]
        public bool insertStadium()
        {
            string fileName = "";
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    string extension = Path.GetExtension(file.FileName);
                    fileName = KardoStaticMethods.createKardoUID() + extension;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/Content/kardoPasso/img/uploads/") + fileName);
                }
            }
            catch
            {
                return false;
            }
            if (fileName == "")
            {
                return false;
            }

            string name = Request.Form["name"];
            string sportTypeIdStr = Request.Form["sportTypeId"];
            string country = Request.Form["country"];
            string state = Request.Form["state"];
            string city = Request.Form["city"];
            string location = Request.Form["location"];
            string streetAdd = Request.Form["streetAdd"];

            int sportTypeId;
            bool isNumeric1 = int.TryParse(sportTypeIdStr, out sportTypeId);

            if (!isNumeric1)
                return false;

            return PagePanelModel.insertStadium(name, fileName, location, streetAdd, state, city, country, sportTypeId); ;
        }
        /*[HttpPost]
        public bool updateStadium()
        {
            string fileName = "NULL";
            string updatePic = Request.Form["updatePic"];
            if (updatePic == "true")
            {
                try
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        //int fileSize = file.ContentLength;
                        string extension = Path.GetExtension(file.FileName);
                        fileName = KardoStaticMethods.createKardoUID() + extension;
                        //string mimeType = file.ContentType;
                        System.IO.Stream fileContent = file.InputStream;
                        file.SaveAs(Server.MapPath("~/Content/kardoPasso/img/uploads/") + fileName);
                    }
                }
                catch
                {
                    return false;
                }
            }

            string name = Request.Form["name"];
            string sportTypeIdStr = Request.Form["sportTypeId"];
            string country = Request.Form["country"];
            string state = Request.Form["state"];
            string city = Request.Form["city"];
            string location = Request.Form["location"];
            string streetAdd = Request.Form["streetAdd"];
            string stadiumIdS = Request.Form["stadiumId"];

            int sportTypeId, stadiumId;
            bool isNumeric1 = int.TryParse(sportTypeIdStr, out sportTypeId);
            bool isNumeric2 = int.TryParse(stadiumIdS, out stadiumId);

            if (!isNumeric1 || !isNumeric2)
                return false;

            return PagePanelModel.updateStadium(name, fileName, location, streetAdd, state, city, country, sportTypeId, stadiumId); 
        }*/
        public bool deleteStadium()
        {
            string dataidS = Request.Form["dataid"];
            int dataid;
            bool isNumeric = int.TryParse(dataidS, out dataid);

            if (!isNumeric)
                return false;

            return PagePanelModel.deleteStadium(dataid);
        }
        public bool insertTeam()
        {
            string name = Request.Form["name"];
            string sportTypeIdStr = Request.Form["sportTypeId"];

            int sportTypeId;
            bool isNumeric1 = int.TryParse(sportTypeIdStr, out sportTypeId);

            if (!isNumeric1)
                return false;

            return PagePanelModel.insertTeam(name, sportTypeId);
        }
        public bool updateTeam()
        {
            string name = Request.Form["name"];
            string sportTypeIdStr = Request.Form["sportTypeId"];
            string teamIdS = Request.Form["teamId"];

            int sportTypeId, teamId;
            bool isNumeric1 = int.TryParse(sportTypeIdStr, out sportTypeId);
            bool isNumeric2 = int.TryParse(teamIdS, out teamId);

            if (!isNumeric1 || !isNumeric2)
                return false;

            return PagePanelModel.updateTeam(name, sportTypeId, teamId);
        }
        public bool deleteTeam()
        {
            string dataidS = Request.Form["dataid"];
            int dataid;
            bool isNumeric = int.TryParse(dataidS, out dataid);

            if (!isNumeric)
                return false;

            return PagePanelModel.deleteTeam(dataid);
        }
        public bool updateUser()
        {
            string roleIdS = Request.Form["roleId"];
            string userIdS = Request.Form["userId"];

            int roleId, userId;
            bool isNumeric1 = int.TryParse(roleIdS, out roleId);
            bool isNumeric2 = int.TryParse(userIdS, out userId);

            if (!isNumeric1 || !isNumeric2)
                return false;

            return PagePanelModel.updateUser(roleId, userId);
        }

        public bool deleteUser()
        {
            string dataidS = Request.Form["dataid"];
            int dataid;
            bool isNumeric = int.TryParse(dataidS, out dataid);

            if (!isNumeric)
                return false;

            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));

            return PagePanelModel.deleteUser(dataid, userId);
        }
        public bool insertSportType()
        {
            string name = Request.Form["name"];
            return PagePanelModel.insertSportType(name);
        }
        public bool updateSportType()
        {
            string name = Request.Form["name"];
            string sportTypeIdS = Request.Form["sportTypeId"];

            int sportTypeId;
            bool isNumeric1 = int.TryParse(sportTypeIdS, out sportTypeId);

            return PagePanelModel.updateSportType(name, sportTypeId);
        }
        public bool deleteSportType()
        {
            string dataidS = Request.Form["dataid"];
            int dataid;
            bool isNumeric = int.TryParse(dataidS, out dataid);

            if (!isNumeric)
                return false;

            return PagePanelModel.deleteSportType(dataid);
        }
        public string getTopTeams() 
        {
            return PagePanelModel.getTopTeams();
        }
        public string getTopEvents()
        {
            return PagePanelModel.getTopEvents();
        }
        public string getTopStadiums()
        {
            return PagePanelModel.getTopStadiums();
        }
        public string getEventsDateGroups()
        {
            return PagePanelModel.getEventsDateGroups();
        }
        public string getEventFromId()
        {
            string eventIdS = Request.Form["eventId"];

            int eventId;
            bool isNumeric1 = int.TryParse(eventIdS, out eventId);
            
            if (!isNumeric1)
                return "{}";

            return PagePanelModel.getEventFromId(eventId);
        }
        /*public string getStadiumFromId()
        {
            string stadiumIdS = Request.Form["stadiumId"];

            int stadiumId;
            bool isNumeric1 = int.TryParse(stadiumIdS, out stadiumId);

            if (!isNumeric1)
                return "{}";

            return PagePanelModel.getStadiumFromId(stadiumId);
        }*/
        public string getTeamFromId()
        {
            string teamIdS = Request.Form["teamId"];

            int teamId;
            bool isNumeric1 = int.TryParse(teamIdS, out teamId);

            if (!isNumeric1)
                return "{}";

            return PagePanelModel.getTeamFromId(teamId);
        }
        public string getSportTypeFromId()
        {
            string sportTypeIdS = Request.Form["sportTypeId"];

            int sportTypeId;
            bool isNumeric1 = int.TryParse(sportTypeIdS, out sportTypeId);

            if (!isNumeric1)
                return "{}";

            return PagePanelModel.getSportTypeFromId(sportTypeId);
        }
        public string getUserFromId()
        {
            string userIdS = Request.Form["userId"];

            int userId;
            bool isNumeric1 = int.TryParse(userIdS, out userId);

            if (!isNumeric1)
                return "{}";

            return PagePanelModel.getUserFromId(userId);
        }
	}
}