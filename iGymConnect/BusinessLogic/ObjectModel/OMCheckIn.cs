using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMCheckIn
    {
        public static implicit operator OMCheckIn(CheckInMaster model)
        {
            return new OMCheckIn()
            {
                CheckIn =model.CheckIn,
                Memberid = model.Memberid,
                Status = model.Status.HasValue ? model.Status.Value : true,
                InTime = model.InTime,
                OutTime = model.OutTime,
                Deleted = model.Deleted,
                CreatedDate = model.CreatedDate,
                CreatedBy = model.CreatedBy,
                DateUpdated = model.DateUpdated ?? DateTime.Now,
                Updated = model.Updated.HasValue ? model.Updated.Value : 0
            };
        }
        public int CheckIn { get; set; }
        public int Memberid { get; set; }
        public bool Status { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Updated { get; set; }
    }
}
