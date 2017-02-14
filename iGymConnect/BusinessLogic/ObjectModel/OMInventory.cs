using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMInventory
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int VendorId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public decimal Discount { get; set; }
        public decimal Cost { get; set; }
        public decimal Total { get; set; }
        public string Barcode { get; set; }
        public string Alias { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
    }
}
