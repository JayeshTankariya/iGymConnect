using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMCardMaster
    {
        public int Id { get; set; }
        public decimal Number { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int MemberId { get; set; }
        public bool IsUsed { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
        
    }
}
