using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BSubCategory
    {
        public static List<OMSubCategory> GetAllSubCategories()
        {
            var SubCatList = new List<OMSubCategory>();
            using (var context = new iGymConnectEntities())
            {
                SubCatList = context.SubCategories
                    .Select(x => new OMSubCategory
                    {
                        Id = x.Id,
                        Name = x.Name,
                        
                    }).ToList();
            }
            return SubCatList;
        }
    }
}
