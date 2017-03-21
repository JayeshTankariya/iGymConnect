using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMPOSCardMaster
    {
        public int Id { get; set; }
        public int TransactionMasterId { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int UpdatedBy { get; set; }
    }
}
