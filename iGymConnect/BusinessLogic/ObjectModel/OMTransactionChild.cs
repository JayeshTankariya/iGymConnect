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
    }
}
