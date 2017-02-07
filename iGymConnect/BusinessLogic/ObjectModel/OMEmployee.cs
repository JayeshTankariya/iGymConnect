using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMEmployee
    {
        public int EmployeeId { get; set; }
        public int AdharcardId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public string Note { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy {get;set;}
        public DateTime DateUpdated { get; set; }
        public int Updated { get; set; }

        public string EmplUserName { get; set; }
        public string EmpPassword { get; set; }
    }
}
