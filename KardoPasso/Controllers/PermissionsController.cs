using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KardoPasso.Models;

namespace KardoPasso.Controllers
{
    public class PermissionsController : Controller
    {
        //
        // GET: /Permissions/
        public ActionResult Index()
        {
            if ((Session["KardoUserId"] != null && Session["KardoUserName"] != null))
            {
                int userId = Convert.ToInt32(UseKardoEncryption.getDecipherString(Session["KardoUserId"].ToString()));
                Dictionary<string, Object> theUser = DefaultModel.getUserFromUserId(userId);
                ViewBag.userInfos = theUser;
                if (!(bool)theUser["can_edit_permissions"])
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }

            
            Dictionary<string, List<Object>> roles = PermissionsModel.getRoles();
            ViewBag.roles = roles;

            return View();
        }
        public bool updatePermission()
        {
            string column = Request.Form["column"];
            string roleIdS = Request.Form["roleId"];
            string isCheckedS = Request.Form["isChecked"];
            if (isCheckedS != "true" && isCheckedS != "false")
                return false;
            bool isChecked;
            if (isCheckedS == "true")
            {
                isChecked = true;
            }
            else
            {
                isChecked = false;
            }
            
            int roleId;
            bool isNumeric = int.TryParse(roleIdS, out roleId);
            if (!isNumeric)
                return false;

            return PermissionsModel.updatePermission(column, roleId, isChecked);
        }
        public bool insertRole()
        {
            string name = Request.Form["name"];
            return PermissionsModel.insertRole(name);
        }
	}
}