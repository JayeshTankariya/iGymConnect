using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BCategory
    {
        public static List<OMCategory> GetAllCategories()
        {
            var catList = new List<OMCategory>();
            using (var context = new UserLoginEntities2())
            {
                catList = context.Categories
                    .Where(x => !x.Deleted)
                    .Select(x => new OMCategory
                    {
                        CategoryName = x.CategoryName
                    }).ToList();
            }
            return catList;
        }
    }
}
