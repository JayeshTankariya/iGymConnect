using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iGymConnect.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(OMUser model)
        {
            if (BUser.GetByUserNameAndPassword(model).Count > 0)
            {
                return RedirectToAction("MembershipView", "MamberShip");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //****************UserDetails**************//

        public ActionResult GetUserDetail(OMUser usr)
        {
            var user = BUser.GetByUserNameAndPassword(usr);
            return View("_UpdateProfile", user);
        }
        public ActionResult UpdateUser(OMUser usr)
        {
            var user = BUser.UpdateUser(usr);
            return Json(user);
        }

        //***********Change Password***********//
        public ActionResult Changepwd()
        {
            return View("_Changepassword");
        }
    }
}