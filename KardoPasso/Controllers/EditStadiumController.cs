using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;
using System.IO;

namespace KardoPasso.Controllers
{
    public class EditStadiumController : Controller
    {
        //
        // GET: /EditStadium/
        public ActionResult Index(string stadiumId)
        {
            if (string.IsNullOrEmpty(stadiumId))
                return Redirect("/");

            int stadiumIdInt;
            bool isNumeric = int.TryParse(stadiumId, out stadiumIdInt);
            if (!isNumeric)
                return Redirect("/");

            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
                ViewBag.userInfos = theUser;
                if (!(bool)theUser["can_modify_stadium"])
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }


            Dictionary<string, Object> theStadium = EditStadiumModel.getStadium(stadiumIdInt);
            ViewBag.theStadium = theStadium;
            Dictionary<string, List<Object>> theSSections = EditStadiumModel.getSSections(stadiumIdInt);
            ViewBag.theSSections = theSSections;
            Dictionary<string, List<Object>> sportTypes = PagePanelModel.getSportTypes();
            ViewBag.sportTypes = sportTypes;
            Dictionary<string, List<Object>> secCategories = EditStadiumModel.getSecCategories();
            ViewBag.secCategories = secCategories;

            return View();
        }
        public string getStadiumFromId()
        {
            string stadiumIdS = Request.Form["stadiumId"];

            int stadiumId;
            bool isNumeric1 = int.TryParse(stadiumIdS, out stadiumId);

            if (!isNumeric1)
                return "{}";

            return EditStadiumModel.getStadiumFromId(stadiumId);
        }
        public string getSectionFromId()
        {
            string sectionIdS = Request.Form["sectionId"];

            int sectionId;
            bool isNumeric1 = int.TryParse(sectionIdS, out sectionId);

            if (!isNumeric1)
                return "{}";

            return EditStadiumModel.getSectionFromId(sectionId);
        }
        [HttpPost]
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

            return EditStadiumModel.updateStadium(name, fileName, location, streetAdd, state, city, country, sportTypeId, stadiumId);
        }

        public bool insertSection()
        {
            string name = Request.Form["name"];
            string capacityS = Request.Form["capacity"];
            string categoryIdS = Request.Form["categoryId"];
            string stadiumIdS = Request.Form["stadiumId"];

            int capacity, categoryId, stadiumId;
            bool isNumeric1 = int.TryParse(capacityS, out capacity);
            bool isNumeric2 = int.TryParse(categoryIdS, out categoryId);
            bool isNumeric3 = int.TryParse(stadiumIdS, out stadiumId);

            if (!isNumeric1 || !isNumeric2 || ! isNumeric3)
                return false;

            return EditStadiumModel.insertSection(name, capacity, categoryId, stadiumId);
        }

        public bool updateSection()
        {
            string name = Request.Form["name"];
            string capacityS = Request.Form["capacity"];
            string categoryIdS = Request.Form["categoryId"];
            string sectionIdS = Request.Form["sectionId"];

            int capacity, categoryId, sectionId;
            bool isNumeric1 = int.TryParse(capacityS, out capacity);
            bool isNumeric2 = int.TryParse(categoryIdS, out categoryId);
            bool isNumeric3 = int.TryParse(sectionIdS, out sectionId);

            if (!isNumeric1 || !isNumeric2 || !isNumeric3)
                return false;

            return EditStadiumModel.updateSection(name, capacity, categoryId, sectionId);
        }
	}
}