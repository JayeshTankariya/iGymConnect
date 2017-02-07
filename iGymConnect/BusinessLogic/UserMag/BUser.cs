using BusinessLogic.ObjectModel;
using DataLogic.Entity_Framework;
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
            using (var context = new UserLoginEntities1())
            {
                userList = context.user_login
                 .Select(x => new OMUser
                 {
                     fname = x.fname,
                     lname = x.lname,
                     username = x.username,
                     pwd = x.pwd,
                     email = x.email,
                     Employeeid = x.Employeeid.HasValue ? x.Employeeid.Value : 0,
                 }).ToList();
            }
            return userList;
        }
        
        public static List<OMUser> UpdateUser(OMUser user)
        {

            var userlist = new List<OMUser>();
            user_login usr = new user_login();
            if (user.id > 0)
            {
                using (var u = new UserLoginEntities1())
                {
                    usr.id = user.id;
                    usr.fname = user.fname;
                    usr.lname = user.lname;
                    usr.username = user.username;
                    usr.email = user.email;
                    usr.pwd = user.pwd;
                    u.SaveChanges();
                }
            }
            else
            {
                    usr.id = user.id;
                    usr.fname = user.fname;
                    usr.lname = user.lname;
                    usr.username = user.username;
                    usr.email = user.email;
                    usr.pwd = user.pwd;
                    using (var u = new UserLoginEntities1())
                    {
                        u.user_login.Add(usr);
                        u.SaveChanges();
                        userlist = GetByUserNameAndPassword(user);
                    }
                
            }
            return userlist;
        }
    }
}
    


