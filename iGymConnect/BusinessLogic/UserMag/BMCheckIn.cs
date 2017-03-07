using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BMCheckIn
    {
        public static List<OMCheckIn> GetCheckIn()
        {
            var checklist = new List<OMCheckIn>();
            using (var context = new iGymConnectEntities())
            {
                checklist = context.CheckInMasters
                .Where(x => x.Deleted)
                 .Select(x => new OMCheckIn
                 {
                     CheckIn = x.CheckIn,
                     Memberid = x.Memberid,
                     Status = x.Status.HasValue ? x.Status.Value : false,
                     InTime = x.InTime ?? DateTime.Now,
                     OutTime = x.OutTime ?? DateTime.Now,
                     Deleted = x.Deleted,
                     DateUpdated = x.CreatedDate,
                     CreatedBy = x.CreatedBy,
                     Updated = x.Updated.HasValue ? x.Updated.Value : 0

                 }).ToList();
            }
            return checklist;
        }
        public static List<OMCheckIn> GetCheckindetails()
        {
            var checklist = new List<OMCheckIn>();
            using (var context = new iGymConnectEntities())
            {
                checklist = context.CheckInMasters
                .Where(x => !x.Deleted)
                 .Select(x => new OMCheckIn
                 {
                     CheckIn = x.CheckIn,
                     Memberid = x.Memberid,
                     Status = x.Status.HasValue ? x.Status.Value : false,
                     InTime = x.InTime ?? DateTime.Now,
                     OutTime = x.OutTime ?? DateTime.Now,
                     Deleted = x.Deleted,
                     DateUpdated = x.CreatedDate,
                     CreatedBy = x.CreatedBy,
                     Updated = x.Updated.HasValue ? x.Updated.Value : 0

                 }).ToList();
            }
            return checklist;
        }
        public static OMCheckIn SaveCheckIn(int memberId)
        {
            CheckInMaster chckin = new CheckInMaster();
            using (var ck = new iGymConnectEntities())
            {
                var lastcheckin = ck.CheckInMasters.Where(x => x.Memberid == memberId).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (lastcheckin != null &&
                    lastcheckin.OutTime == null)
                {
                    var outtime = DateTime.Now;
                    lastcheckin.CheckIn = lastcheckin.CheckIn;
                    lastcheckin.Status = !lastcheckin.Status.Value;
                    lastcheckin.OutTime = outtime;
                    lastcheckin.Updated = 1;
                    lastcheckin.DateUpdated = DateTime.Now;
                    ck.SaveChanges();
                    return lastcheckin;
                }
                else
                {
                    var intime = DateTime.Now;
                    chckin.CheckIn = chckin.CheckIn;
                    chckin.Memberid = memberId;
                    chckin.Status = true;
                    chckin.InTime = intime;
                    chckin.OutTime = null;
                    chckin.CreatedBy = 1;
                    chckin.Deleted = false;
                    chckin.CreatedDate = intime;
                    ck.CheckInMasters.Add(chckin);
                    ck.SaveChanges();
                    var efcheckin = ck.CheckInMasters.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    return efcheckin;
                }
            }
        }
        public static List<OMCheckIn> membercheck(int id)
        {
            var checklist = new List<OMCheckIn>();
            using (var context = new iGymConnectEntities())
            {
                checklist = context.CheckInMasters.Where(x => x.Memberid == id)
                 .Select(x => new OMCheckIn
                 {
                     CheckIn = x.CheckIn,
                     Memberid = x.Memberid,
                     Status = x.Status.HasValue ? x.Status.Value : false,
                     InTime = x.InTime ?? DateTime.Now,
                     OutTime = x.OutTime ?? DateTime.Now,
                     Deleted = x.Deleted,
                     DateUpdated = x.CreatedDate,
                     CreatedBy = x.CreatedBy,
                     Updated = x.Updated.HasValue ? x.Updated.Value : 0

                 }).ToList();
            }
            return checklist;
        }
    }
}
