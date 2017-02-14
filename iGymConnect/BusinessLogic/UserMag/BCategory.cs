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
            using (var context = new iGymConnectEntities())
            {
                catList = context.Categories
                    .Where(x => !x.Deleted)
                    .Select(x => new OMCategory
                    {
                        Id = x.Id,
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                        Image = x.Image,
                        ParentId = x.ParentId.HasValue ? x.ParentId.Value : 0,
                    }).ToList();
            }
            return catList;
        }

        public static List<OMCategory> Save(OMCategory cat)
        {
            var catlist = new List<OMCategory>();
            Category category = new Category();
            if (cat.Id > 0)
            {
                using (var c = new iGymConnectEntities())
                {
                    category = c.Categories.FirstOrDefault(x => x.Id == cat.Id);
                    category.CategoryName = cat.CategoryName;
                    category.Description = cat.Description;
                    category.Image = cat.Image;
                    category.ParentId = cat.ParentId;
                    category.Deleted = false;
                    category.UpdatedBy = 1;
                    category.DateUpdated = DateTime.Now;                
                    c.SaveChanges();
                }
            }
            else
            {
                category.CategoryName = cat.CategoryName;
                category.Description = cat.Description;
                category.Image = cat.Image;
                category.ParentId = cat.ParentId;
                category.Deleted = false;
                category.CreatedBy = 1;
                category.DateCreated = DateTime.Now;
                using (var c = new iGymConnectEntities())
                {
                    c.Categories.Add(category);
                    c.SaveChanges();
                    catlist = GetAllCategories();
                }
            }
            return catlist;
        }

        public static List<OMCategory> Delete(int Id)
        {
            var categorylist = new List<OMCategory>();

            using (var c = new iGymConnectEntities())
            {
                var dlCategory = c.Categories.FirstOrDefault(x => x.Id == Id);
                //c.Categories.Remove(dlCategory);
                dlCategory.Deleted = true;
                c.SaveChanges();
                categorylist = GetAllCategories();
            }


            return categorylist;
        }
    }
}
