using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMVendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        public int ItemId { get; set; }
        public int CategoryId { get; set; }

        public string Address { get; set; }
        public Decimal Number { get; set; }
        public string City { get; set; }
    }
}
