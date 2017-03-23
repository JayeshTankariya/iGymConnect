using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BMembership
    {
        public static List<OMMembership> GetAllByMembership()
        {
            var MembershipList = new List<OMMembership>();
            using (var context = new iGymConnectEntities())
            {
                MembershipList = context.MembershipTypeMasters
                    .Where(x => !x.Deleted)
                    .Select(x => new OMMembership()
                    {
                        MembershipTypeId=x.MembershipTypeId,
                        Description = x.Description,
                        ActiveDate = x.ActiveDate ?? DateTime.Now,
                        InActiveDate = x.InActiveDate ?? DateTime.Now,

                    }).ToList();
            }
            return MembershipList;
        }
        public static List<OMMembership> SaveMemship(OMMembership memship)
        {

            var membershiplist = new List<OMMembership>();
            MembershipTypeMaster membership = new MembershipTypeMaster();
            if (memship.MembershipTypeId > 0)
            {
                using (var ms = new iGymConnectEntities())
                {
                    
                    membership = ms.MembershipTypeMasters.FirstOrDefault(x => x.MembershipTypeId == memship.MembershipTypeId);
                    membership.Description = memship.Description;
                    membership.MembershipTypeId = memship.MembershipTypeId;
                    membership.ActiveDate = memship.ActiveDate;
                    membership.InActiveDate = memship.InActiveDate;
                    membership.Updated = 1;
                    membership.Deleted = false;
                    membership.DateUpdated = DateTime.Now;
                    ms.SaveChanges();
                }
            }
            else
            {
                membership.MembershipTypeId = memship.MembershipTypeId;
                membership.Description = memship.Description;
                membership.ActiveDate = memship.ActiveDate;
                membership.InActiveDate = memship.InActiveDate;
                membership.CreatedBy = 1;
                membership.Deleted = false;
                membership.DateCreated = DateTime.Now;
                using (var ms = new iGymConnectEntities())
                {
                    ms.MembershipTypeMasters.Add(membership);
                    ms.SaveChanges();
                    membershiplist = GetAllByMembership();
                }
            }
            return membershiplist;   
        }
        public static List<OMMembership> Deletememship(int MembershipTypeId)
        {
            var membershiplist = new List<OMMembership>();

            using (var ms = new iGymConnectEntities())
            {
                var dlmemship = ms.MembershipTypeMasters.FirstOrDefault(x => x.MembershipTypeId == MembershipTypeId);
                dlmemship.Deleted = true;
                ms.SaveChanges();
                membershiplist = GetAllByMembership();
            }
            return membershiplist;
        }
    }

}
