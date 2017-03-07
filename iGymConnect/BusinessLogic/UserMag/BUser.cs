using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BUser
    {
        public static List<OMUser> GetByUserNameAndPassword(OMUser user)
        {
            var userList = new List<OMUser>();
            using (var context = new iGymConnectEntities())
            {
                userList = context.UserLogins
                 .Select(x => new OMUser
                 {
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     Username = x.Username,
                     Password = x.Password,
                     EmailId = x.EmailId,
                     Employeeid = x.Employeeid.HasValue ? x.Employeeid.Value : 0,
                 }).ToList();
            }
            return userList;
        }
        public static List<OMUser> GetAllUser()
        {
            var userlist = new List<OMUser>();
            using (var context = new iGymConnectEntities())
            {
                userlist = context.UserLogins
                    .Select(x => new OMUser
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        Employeeid = x.Employeeid.HasValue ? x.Employeeid.Value : 0,
                        Username = x.Username,
                        Password = x.Password,
                        EmailId = x.EmailId
                    }).ToList();
            }
            return userlist;
        }


        public static List<OMUser> UpdateUser(OMUser user)
        {

            var userlist = new List<OMUser>();
            UserLogin usr = new UserLogin();
            if (user.Id > 0)
            {
                using (var u = new iGymConnectEntities())
                {
                    usr = u.UserLogins.FirstOrDefault(x => x.Id == user.Id);
                    usr.FirstName = user.FirstName;
                    usr.LastName = user.LastName;
                    usr.Username = user.Username;
                    usr.EmailId = user.EmailId;
                    usr.Password = user.Password;
                    usr.Employeeid = user.Employeeid;
                    u.SaveChanges();
                }
            }
            else
            {
                usr.Id = user.Id;
                usr.FirstName = user.FirstName;
                usr.LastName = user.LastName;
                usr.Username = user.Username;
                usr.EmailId = user.EmailId;
                usr.Password = user.Password;
                usr.Employeeid = user.Employeeid;
                using (var u = new iGymConnectEntities())
                {
                    u.UserLogins.Add(usr);
                    u.SaveChanges();
                    userlist = GetByUserNameAndPassword(user);
                }

            }
            return userlist;
        }
        public static List<OMUser> Changepwd(OMUser chng)
        {

            var chngelist = new List<OMUser>();
            UserLogin cg = new UserLogin();
            if (chng.Id > 0)
            {
                using (var u = new iGymConnectEntities())
                {
                    cg = u.UserLogins.FirstOrDefault(x => x.Id == chng.Id); 
                    cg.Password = chng.Password; 
                    u.SaveChanges();
                }
            }
            else
            {
                cg.FirstName = chng.FirstName;
                cg.Password = chng.Password;  
                using (var u = new iGymConnectEntities())
                {
                    u.UserLogins.Add(cg);
                    u.SaveChanges();
                    chngelist = GetByUserNameAndPassword(chng);
                }

            }
            return chngelist;
        }
    }
}



