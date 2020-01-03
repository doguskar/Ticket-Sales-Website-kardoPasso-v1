using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;

namespace KardoPasso.Controllers
{
    public class EventDetailController : Controller
    {
        //
        // GET: /EventDetail/
        public ActionResult Index(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
                return Redirect("/");

            string eventIdStr = UseKardoEncryption.getDecipherString(eventId);
            if (eventIdStr == "KardoEncryptionError")
                return Redirect("/");

            int eventIdInt;
            bool isNumeric = int.TryParse(eventIdStr, out eventIdInt);

            if (!isNumeric)
                return Redirect("/");

            Dictionary<string, Object> theEvent = EventDetailModel.getEventInfo(eventIdInt);
            Dictionary<string, List<Object>> ticketInfos = EventDetailModel.getEventTicketInfo(eventIdInt);
            
            ViewBag.theEvent = theEvent;
            ViewBag.ticketInfos = ticketInfos;

            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);

                ViewBag.userInfos = theUser;
                ViewBag.userTickets = UserProfileModel.getUserTickets((int)theUser["userId"]);

            }

            return View();
        }

        public string getSectionInfo() 
        {
            string eventIdStr = Request.Form["eventId"];
            string ssi = Request.Form["ssi"];
            string ssiDec = UseKardoEncryption.getDecipherString(ssi);
            if (ssiDec == "KardoEncryptionError")
                return "{}";

            int eventId, ssId;
            bool isNumeric = int.TryParse(eventIdStr, out eventId);
            bool isNumeric2 = int.TryParse(ssiDec, out ssId);
            if (isNumeric && isNumeric2)
                return EventDetailModel.getSectionInfo(eventId, ssId);
            return "{}";
        }

        public bool insertSale()
        {
            string eventIdStr = Request.Form["eventId"];
            string ssi = Request.Form["ssi"];
            string amount = Request.Form["amount"];
            string ssiDec = UseKardoEncryption.getDecipherString(ssi);
            if (ssiDec == "KardoEncryptionError")
                return false;
            
            int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
            Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
            int eventId, ssId, amountInt;
            int kpId = (int) theUser["kardoPassoId"];
            bool isNumeric = int.TryParse(eventIdStr, out eventId);
            bool isNumeric2 = int.TryParse(ssiDec, out ssId);
            bool isNumeric3 = int.TryParse(amount, out amountInt);
            if (isNumeric && isNumeric2 && isNumeric3)
                return EventDetailModel.insertSale(eventId, ssId, kpId, amountInt, userId);
            return false;
        }
	}
}