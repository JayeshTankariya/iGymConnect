﻿using BusinessLogic.ObjectModel;
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
            var p = BMember.GetAllByMember().ToList();
            ViewBag.AllMember = p;
            var q = BCardMaster.GetAllCard().ToList();
            ViewBag.AllCard = q;

            return View(BCategory.GetAllCategories());
        }


        //Select Member Master Details
        public ActionResult SelectMember(int Id)
        {
            var m = BTransaction.GetAllTransaction().FirstOrDefault(x => x.MemberId == Id);
            return Json(m);
        }
        public ActionResult GetInvntoryByCategoryId(int Id)
        {
            var inv = BInventory.GetAllInventory().Where(x => x.CategoryId == Id)
                .Select(x => new OMInventory
                {
                    Id = x.Id,
                    Item = x.Item,
                    VendorId = x.VendorId,
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
        //Check Duplication Inventory***********
        public bool CheckDulplicationCard(int Id, int Number)
        {
            var card = BCardMaster.GetAllCard();
            if (Id > 0)
            {
                card = card.Where(x => x.Id != Id && x.Number == Number).ToList();
            }
            else
            {
                card = card.Where(x => x.Number == Number).ToList();
            }
            if (card.Count > 0)
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
        //Card Master *****[ GIFT CARD ]************
        //public ActionResult GetCardItems(OMCardMaster card)
        //{
        //    var CardItems = BCardMaster.SaveCardItems(card);
        //    return Json(CardItems, JsonRequestBehavior.AllowGet);
        //}

        //********* Barcode Scanner ***********
        public ActionResult GetAllByBarcode(string Barcode)
        {
            var b = BInventory.GetAllInventory().FirstOrDefault(x => x.Barcode == Barcode);
            return Json(b, JsonRequestBehavior.AllowGet);
        }

        //********* Barcode Scanner ***********
        public ActionResult GetAllByCard()
        {
            var z = BMember.GetAllByMember().ToList();
            ViewBag.MemberList = z;
            var y = BCardMaster.GetCard().ToList();
            ViewBag.CardList = y;
            return View("_GiftCard");
        }
        //********* New Card Generate ***********
        public ActionResult GenerateCard(int From, int To, decimal Amount, DateTime ExpiryDate)
        {
            if (BCardMaster.Save(From, To, Amount, ExpiryDate) == 0)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public ActionResult GetMemberIdInCard(int MemberId, int Number)
        {
            var mem = BCardMaster.GetMemberAndCard(MemberId, Number);
            return Json(mem);
        }

        //******* Get Card numbers by Member *********
        public ActionResult GetCardNoByMember(int Id)
        {
            var card = BPOSCardMaster.GetCardByMember(Id);
            var usedCards = BTransaction.GetCardItems(0);
            if (usedCards != null)
            {
                foreach (var usedCard in usedCards)
                {
                    var foundCard = card.FirstOrDefault(x => x.Id == usedCard.CardId);
                    if (foundCard != null)
                    {
                        card.Remove(foundCard);
                    }
                }
            }
            return Json(card, JsonRequestBehavior.AllowGet);

        }
        //******* Get Card Details *********
        public ActionResult GetCardDetails(int Id)
        {
            var card = BCardMaster.GetAllCard().FirstOrDefault(x => x.Id == Id);
            return Json(card, JsonRequestBehavior.AllowGet);

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
                var path = Server.MapPath("~/Content/CategoryImages/") + fileName;
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
        [HttpPost]
        public ActionResult DeleteCategory(int Id)
        {
            var inv = BInventory.GetAllInventory().FirstOrDefault(x => x.CategoryId == Id);

            if (inv != null)
            {
                return Json(new { responseMsg = "Category Is In Used,You Can't Delete Category" });
            }
            else
            {
                var i = BCategory.Delete(Id);
                return Json(new { responseMsg = "Category Deleted Successfully." });
            }
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

        //BTransaction Details modeule
        public ActionResult GetTransactionDetails()
        {
            var Transaction = BTransaction.GetAllTransaction();

            var p = BMember.GetAllByMember().ToList();
            ViewBag.AllMember = p;
            var abc = BCategory.GetAllCategories().ToList();
            ViewBag.ParentCategoryList1 = abc;
            var abc2 = BInventory.GetAllInventory().ToList();
            ViewBag.InventoryList1 = abc2;
            return View("_TransactionDetails", Transaction);

        }
        public ActionResult GetTransDetailsMaster(int Id, int Id1, Nullable<DateTime> dt, Nullable<DateTime> dt1, int Id2)
        {
            var abc = BTransaction.GetAllTransaction();
            var tran = BTransaction.GetTranRange(dt, dt1);
            if (Id > 0)
            {
                abc = BTransaction.GetTransDetailsMaster(Id);
            }
            if (Id1 > 0)
            {
                var inv = BTransaction.GetTranChild(Id1);
                List<OMTransaction> invTrans = new List<OMTransaction>();
                if (inv != null && inv.Count > 0)
                {
                    foreach (var i in inv)
                    {
                        var existingMaster = abc.FirstOrDefault(x => x.Id == i.TransactionMasterId);
                        if (existingMaster != null && existingMaster.Id == i.TransactionMasterId)
                        {
                            invTrans.Add(existingMaster);
                        }
                    }
                }
                abc = invTrans;
            }
            if (dt < dt1)
            {
                var a = BTransaction.GetTranRange(dt, dt1);
                List<OMTransaction> invTrans1 = new List<OMTransaction>();
                invTrans1.AddRange(a);
                abc = invTrans1;
            }

            if (Id2 > 0)
            {
                var inv = BInventory.GetInvByCat(Id2);
                List<OMTransactionChild> invtrans2 = new List<OMTransactionChild>();
                List<OMTransaction> invmast = new List<OMTransaction>();
                if (inv != null && inv.Count > 0)
                {
                    foreach (var i in inv)
                    {
                        invtrans2.AddRange(BTransaction.GetAllTrasactionChild().Where(x => x.ItemId == i.Id).ToList());
                    }

                    foreach (var t in invtrans2)
                    {
                        invmast.Add(abc.FirstOrDefault(x => x.Id == t.TransactionMasterId));
                    }
                }
                abc = invmast;
            }
            return Json(abc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildTransactionDetails(int Id)
        {
            var Transaction = BTransaction.GetAllTrasactionChild().Where(x => x.TransactionMasterId == Id);
            return Json(Transaction, JsonRequestBehavior.AllowGet);

        }
    }
}