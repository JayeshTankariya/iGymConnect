using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
   public class OMMembership
    {
        public int MembershipTypeId { get; set;}
        public string Description { get; set;}
        public DateTime ActiveDate { get; set;}
        public DateTime InActiveDate { get; set;}
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Updated { get; set; }

    }
}
