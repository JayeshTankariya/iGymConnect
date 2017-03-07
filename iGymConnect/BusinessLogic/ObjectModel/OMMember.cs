using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMMember
    {
        public int MemberId { get; set;}
        public string MemberImage { get; set; }
        public string MemberName { get; set;}
        public string Barcode { get; set; }
        public bool Gender { get; set;}
        public string Address { get; set;}
        public string Address2 { get; set;}
        public string City { get; set;}
        public string State { get; set;}
        public decimal Zip { get; set;}
        public decimal PhoneHome1 { get; set;}
        public decimal PhoneWork1 { get; set;}
        public string Email { get; set;}
        public string Note { get; set; }
        public int Membershiptypeid { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Updated { get; set; }


    }
}
