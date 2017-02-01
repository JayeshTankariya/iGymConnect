using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iGymConnect.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(OMUser model)
        {
            if (BUser.GetByUserNameAndPassword(model).Count > 0)
            {
               // Session["User"] = user;
                // return RedirectToAction("Index", "Dashboard");
               return RedirectToAction("MembershipView", "MamberShip");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            //  return View();
        }
    }
}