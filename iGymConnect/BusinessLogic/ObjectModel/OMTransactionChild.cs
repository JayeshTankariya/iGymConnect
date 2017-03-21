using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMTransactionChild
    {
        public int Id { get; set; }
        public int TransactionMasterId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemTotal { get; set; }

        public OMInventory Item
        {
            get
            {
                if (ItemId != 0)
                {
                    return BInventory.GetAllInventory().FirstOrDefault(x => x.Id == ItemId);
                }
                else
                {
                    return null;
                }
            }
        }
        public OMCategory Category
        {
            get
            {
                if (ItemId != 0)
                {
                    return BCategory.GetAllCategories().FirstOrDefault(x => x.Id == Item.CategoryId);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
