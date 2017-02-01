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
    }
}