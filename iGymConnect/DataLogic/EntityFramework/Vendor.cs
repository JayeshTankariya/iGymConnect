//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLogic.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirmName { get; set; }
        public bool Deleted { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Number { get; set; }
        public string City { get; set; }
    }
}
