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
            using (var context = new UserLoginEntities2())
            {
                vendorList = context.Vendors
                    .Where(x => x.Name)
                    .Select(x => new OMVendor
                    {
                        Name = x.Name
                    }).ToList();
            }

            return vendorList;
        }
    }
}
