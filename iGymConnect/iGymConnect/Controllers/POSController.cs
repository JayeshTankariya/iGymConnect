using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iGymConnect.Controllers
{
    public class POSController : Controller
    {
        // GET: POS
        public ActionResult GetInventory()
        {
            return View();
        }


        //public ActionResult GetCategory()
        //{
        //    return View();
        //}

        public ActionResult SaveCategory(OMCategory cat,HttpPostedFileBase file)
        {

            return Json("Success");
        }

        public ActionResult GetCategories()
        {
            var categories = BCategory.GetAllCategories();
            return View("_Category", categories);
        }

        public ActionResult GetVendors()
        {
            var vendors = BVendor.GetAllVendors();
            return View("", );
        }

        //public ActionResult GetVendor()
        //{
        //    return View();
        //}
    }
}