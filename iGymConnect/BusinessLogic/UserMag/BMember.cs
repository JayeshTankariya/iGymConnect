using BusinessLogic.ObjectModel;
using DataLogic.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BMember
    {
        public static List<OMMember> GetAllByMember()
        {
            var MemberList = new List<OMMember>();
            using (var context = new UserLoginEntities1())
            {
                MemberList = context.MemberMasters
                    .Where(x => !x.Deleted)
                    .Select(x => new OMMember
                    {
                        MemberId = x.MemberId,
                        MemberImage=x.MemberImage,
                        MemberName = x.MemberName,
                        Gender = x.Gender,
                        Address = x.Address,
                        Address2 = x.Address2,
                        City = x.City,
                        State = x.State,
                        Zip = x.Zip.HasValue ? x.Zip.Value : 0,
                        PhoneHome1 = x.PhoneHome1.HasValue ? x.PhoneHome1.Value : 0,
                        PhoneWork1 = x.PhoneWork1.HasValue ? x.PhoneWork1.Value : 0,
                        Email = x.Email,
                        Note = x.Note,
                    }).ToList();
            }
            return MemberList;
        }

        public static List<OMMember> Save(OMMember mem)
        {
            var memberlist = new List<OMMember>();
            MemberMaster member = new MemberMaster();
            member.MemberId = mem.MemberId;
            member.MemberName = mem.MemberName;
            member.MemberImage = mem.MemberImage;
            member.Gender = mem.Gender;
            member.Address = mem.Address;
            member.Address2 = mem.Address2;
            member.City = mem.City;
            member.State = mem.State;
            member.PhoneHome1 = mem.PhoneHome1;
            member.PhoneWork1 = mem.PhoneWork1;
            member.Email = mem.Email;
            member.Note = mem.Note;
            member.MemberId = mem.MemberId;
            member.CreatedBy = 1;
            member.Deleted = false;
            member.DateCreated = DateTime.Now;

            using (var m = new UserLoginEntities1())
            {
                m.MemberMasters.Add(member);
                m.SaveChanges();
                memberlist = GetAllByMember();
            }
            return memberlist;
        }
        public static List<OMMember> Deletemem(int MemberId)
        {
            var memberlist = new List<OMMember>();
            using (var m = new UserLoginEntities1())
            {
                var dlMember = m.MemberMasters.FirstOrDefault(x => x.MemberId == MemberId);
                m.MemberMasters.Remove(dlMember);
                m.SaveChanges();
                memberlist = GetAllByMember();
            }
            return memberlist;
        }
    }
}
