using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View(BCategory.GetAllCategories());
        }
        public ActionResult GetInvntoryByCategoryId(int Id)
        {
            var inv = BInventory.GetAllInventory().Where(x => x.CategoryId == Id)
                .Select(x => new OMInventory
                {
                    Id = x.Id,
                    Item = x.Item,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    Discount = x.Discount,
                    Cost = x.Cost,
                    Barcode = x.Barcode
                }).ToList();

            return Json(inv, JsonRequestBehavior.AllowGet);

        }
        //Add Quantity in Inventory********
        public ActionResult AddQty(int Id)
        {
            var invData = BInventory.GetAllInventory().FirstOrDefault(x => x.Id == Id);

            return Json(invData, JsonRequestBehavior.AllowGet);
        }
        //Check Duplication Vendor**************
        public bool CheckDulplicationVendor(int Id, string Name)
        {
            var ven = BVendor.GetAllVendors();
            if (Id > 0)
            {
                ven = ven.Where(x => x.Id != Id && x.Name == Name).ToList();
            }
            else
            {
                ven = ven.Where(x => x.Name == Name).ToList();
            }
            if (ven.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check Duplication Category**************
        public bool CheckDulplicationCategory(int Id, string CategoryName)
        {
            var cat = BCategory.GetAllCategories();
            if (Id > 0)
            {
                cat = cat.Where(x => x.Id != Id && x.CategoryName == CategoryName).ToList();
            }
            else
            {
                cat = cat.Where(x => x.CategoryName == CategoryName).ToList();
            }
            if (cat.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Check Duplication Inventory***********
        public bool CheckDulplicationInventory(int Id, string InventoryName)
        {
            var inv = BInventory.GetAllInventory();
            if (Id > 0)
            {
                inv = inv.Where(x => x.Id != Id && x.Item == InventoryName).ToList();
            }
            else
            {
                inv = inv.Where(x => x.Item == InventoryName).ToList();
            }
            if (inv.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Cash Payment***********
        public ActionResult CashPayment(int Id)
        {
            var inv = BInventory.GetAllInventory().FirstOrDefault(x => x.Id == Id);
            return Json(inv, JsonRequestBehavior.AllowGet);

        }

        //Transaction Master *****[ CASH ]************
        [HttpPost]
        public ActionResult GetAllByTransactionMaster(OMTransaction tran)
        {
            var transaction = BTransaction.Save(tran);
            return Json(transaction);

        }


        //********* Category ***********
        public ActionResult SaveCategory(OMCategory cat)
        {

            var category = BCategory.Save(cat);
            return Json(category);
        }
        [HttpPost]
        public ActionResult SaveCategory(OMCategory cat, HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var fileName = file.FileName;
                var path = Server.MapPath("~/Content/CategoryImages") + fileName;
                file.SaveAs(path);
                cat.Image = file.FileName;
            }


            var category = BCategory.Save(cat);
            return Json(category);
        }




        public ActionResult GetCategories()
        {
            var categories = BCategory.GetAllCategories();
            ViewBag.AllCategory = categories;
            var abc = BCategory.GetAllCategories().ToList();
            ViewBag.ParentCategoryList = abc;
            return View("_Category", categories);
        }

        public ActionResult DeleteCategory(int Id)
        {
            var cat = BCategory.Delete(Id);
            return Json(cat);
        }

        public ActionResult ShowCategoryDetails(int Id)
        {
            var cat = BCategory.GetAllCategories().FirstOrDefault(x => x.Id == Id);

            return Json(cat, JsonRequestBehavior.AllowGet);
        }

        //******* Vendor *********
        public ActionResult GetVendors()
        {
            var vendors = BVendor.GetAllVendors();
            return View("_Vendor", vendors);
        }

        public ActionResult SaveVendor(OMVendor vendor)
        {
            var ven = BVendor.Save(vendor);
            return Json(ven);
        }

        public ActionResult DeleteVendor(int Id)
        {
            var ven = BVendor.Delete(Id);
            return Json(ven);
        }
        public ActionResult ShowVendorDetails(int Id)
        {
            var ven = BVendor.GetAllVendors().FirstOrDefault(x => x.Id == Id);
            return Json(ven, JsonRequestBehavior.AllowGet);
        }


        //********* Inventory ***********
        public ActionResult GetInventories()
        {
            var Inventories = BInventory.GetAllInventory();
            var abc = BVendor.GetAllVendors().ToList();
            ViewBag.VendorList = abc;
            var abc1 = BCategory.GetAllCategories().ToList();
            ViewBag.CategoryList = abc1;
            var abc2 = BSubCategory.GetAllSubCategories().ToList();
            ViewBag.SubCategoryList = abc2;
            return View("_Inventory", Inventories);
        }

        public ActionResult SaveInventory(OMInventory inv)
        {
            var inventory = BInventory.Save(inv);
            return Json(inventory);
        }

        public ActionResult DeleteInventory(int Id)
        {
            var inv = BInventory.Delete(Id);
            return Json(inv);
        }
        public ActionResult ShowInventoryDetails(int Id)
        {
            var inv = BInventory.GetAllInventory().FirstOrDefault(x => x.Id == Id);

            return Json(inv, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetVendor()
        //{
        //    return View();
        //}


    }
}