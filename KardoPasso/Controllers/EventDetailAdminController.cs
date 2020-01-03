using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;

namespace KardoPasso.Controllers
{
    public class EventDetailAdminController : Controller
    {
        //
        // GET: /EventDetailAdmin/
        public ActionResult Index(string eventId)
        {
            if (string.IsNullOrEmpty(eventId))
                return Redirect("/");

            int eventIdInt;
            bool isNumeric = int.TryParse(eventId, out eventIdInt);
            if (!isNumeric)
                return Redirect("/");

            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
                ViewBag.userInfos = theUser;

                if (!(bool)theUser["can_examine_salesDetails"])
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }


            Dictionary<string, Object> theEvent = EventDetailModel.getEventInfo(eventIdInt);
            ViewBag.theEvent = theEvent;
            Dictionary<string, List<Object>> ticketInfos = EventDetailModel.getEventTicketInfo(eventIdInt);
            ViewBag.ticketInfos = ticketInfos;


            return View();
        }
	}
}