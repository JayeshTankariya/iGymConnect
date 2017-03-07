    using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BVendor
    {
        public static List<OMVendor> GetAllVendors()
        {
            var vendorList = new List<OMVendor>();
            using (var context = new iGymConnectEntities())
            {
                vendorList = context.Vendors
                    .Where(x => !x.Deleted)
                    .Select(x => new OMVendor
                    {
                        Id = x.Id,
                        Name = x.Name,
                        FirmName = x.FirmName,
                        Address = x.Address,
                        Number = x.Number.HasValue ? x.Number.Value:0,
                        City = x.City

                    }).ToList();
            }

            return vendorList;
        }

        public static List<OMVendor> Save(OMVendor ven)
        {
            var vendorlist = new List<OMVendor>();
            Vendor vendor = new Vendor();
            if (ven.Id > 0)
            {
                using (var v = new iGymConnectEntities())
                {
                    vendor = v.Vendors.FirstOrDefault(x => x.Id == ven.Id);
                    vendor.Name = ven.Name;
                    vendor.FirmName = ven.FirmName;
                    vendor.Address = ven.Address;
                    vendor.Number = ven.Number;
                    vendor.City = ven.City;
                    vendor.Deleted = false;
                    vendor.UpdatedBy = 1;
                    vendor.DateUpdated = DateTime.Now;
                    v.SaveChanges();
                }
            }
            else
            {
                vendor.Name = ven.Name;
                vendor.FirmName = ven.FirmName;
                vendor.Address = ven.Address;
                vendor.Number = ven.Number;
                vendor.City = ven.City;
                vendor.Deleted = false;
                vendor.CreatedBy = 1;
                vendor.DateCreated = DateTime.Now;
                using (var v = new iGymConnectEntities())
                {
                    v.Vendors.Add(vendor);
                    v.SaveChanges();
                    vendorlist = GetAllVendors();
                }
            }
            return vendorlist;  
        }

        public static List<OMVendor> Delete(int Id)
        {
            var vendorlist = new List<OMVendor>();
                       
            using (var v = new iGymConnectEntities())
            {
                var dlVendor = v.Vendors.FirstOrDefault(x => x.Id == Id);
               // v.Vendors.Remove(dlVendor);
                dlVendor.Deleted = true;
                v.SaveChanges();
                vendorlist = GetAllVendors();
            }


            return vendorlist;
        }

    }


}
