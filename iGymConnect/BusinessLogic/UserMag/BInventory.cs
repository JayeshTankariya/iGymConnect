using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BInventory
    {
        public static List<OMInventory> GetAllInventory()
        {
            var InvList = new List<OMInventory>();
            using (var context = new iGymConnectEntities())
            {
                InvList = context.Inventories
                    .Where(x => !x.Deleted)
                    .Select(x => new OMInventory
                    {
                        Id = x.Id,
                        Item = x.Item,
                        Description = x.Description,
                        VendorId = x.VendorId.HasValue ? x.VendorId.Value : 0,
                        CategoryId = x.CategoryId.HasValue ? x.CategoryId.Value : 0,
                        SubCategoryId = x.SubCategoryId.HasValue ? x.SubCategoryId.Value : 0,
                        Discount = x.Discount.HasValue ? x.Discount.Value : 0,
                        Cost = x.Cost.HasValue ? x.Cost.Value : 0,
                        Total = x.Total.HasValue ? x.Total.Value : 0,
                        Barcode = x.Barcode,
                    }).ToList();
            }
            return InvList;
        }

        public static List<OMInventory> Save(OMInventory inv)
        {
                var invlist = new List<OMInventory>();
                Inventory inventory = new Inventory();
                if (inv.Id > 0)
                {
                    using (var i = new iGymConnectEntities())
                    {
                    inventory = i.Inventories.FirstOrDefault(x => x.Id == inv.Id);
                    inventory.Item = inv.Item;
                    inventory.Description = inv.Description;
                    inventory.CategoryId = inv.CategoryId;
                    inventory.VendorId = inv.VendorId;
                    inventory.SubCategoryId = inv.SubCategoryId;
                    inventory.Discount = inv.Discount;
                    inventory.Cost = inv.Cost;
                    inventory.Total = inv.Total;
                    inventory.Barcode = inv.Barcode;
                    inventory.UpdatedBy = 1;
                    inventory.DateUpdated = DateTime.Now;
                    inventory.Deleted = false;
                    i.SaveChanges();
                }
            }
            else
            {
                inventory.Item = inv.Item;
                inventory.Description = inv.Description;
                inventory.CategoryId = inv.CategoryId;
                inventory.VendorId = inv.VendorId;
                inventory.SubCategoryId = inv.SubCategoryId;
                inventory.Discount = inv.Discount;
                inventory.Cost = inv.Cost;
                inventory.Total = inv.Total;
                inventory.Barcode = inv.Barcode;
                inventory.Deleted = false;
                inventory.CreatedBy = 1;
                inventory.DateCreated = DateTime.Now;
                using (var i = new iGymConnectEntities())
                {
                    i.Inventories.Add(inventory);
                    i.SaveChanges();
                    invlist = GetAllInventory();
                }
            }
            return invlist;
        }

        public static List<OMInventory> Delete(int Id)
        {
            var invlist = new List<OMInventory>();

            using (var i = new iGymConnectEntities())
            {
                var dlInventory = i.Inventories.FirstOrDefault(x => x.Id == Id);
                //i.Inventories.Remove(dlInventory);
                dlInventory.Deleted = true;
                i.SaveChanges();
                invlist = GetAllInventory();
            }


            return invlist;
        }

        public static List<OMInventory> GetInvByCat(int Id)
        {
            var InvList = new List<OMInventory>();
            using (var context = new iGymConnectEntities())
            {
                InvList = context.Inventories
                    .Where(x => x.CategoryId == Id)
                    .Select(x => new OMInventory
                    {
                        Id = x.Id,
                        Item = x.Item,
                        CategoryId = x.CategoryId.HasValue ? x.CategoryId.Value : 0,
                        Barcode = x.Barcode,
                        Description = x.Description,
                        Cost = x.Cost.HasValue ? x.Cost.Value : 0,
                        Discount = x.Discount.HasValue ? x.Discount.Value : 0,
                        Total = x.Total.HasValue ? x.Total.Value : 0,
                    }).ToList();
            }

            return InvList;
        }
    }
}
