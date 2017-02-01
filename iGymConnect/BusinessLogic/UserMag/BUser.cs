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
        public static List<OMUser> GetByUserNameAndPassword(OMUser user_login)
        {
            var userList = new List<OMUser>();
            using (var context = new UserLoginEntities1())
            {
                userList = context.user_login
                 .Select(x => new OMUser
                 {
                     fname=x.fname,
                     lname=x.lname,
                     username = x.username,
                     pwd = x.pwd,
                     email = x.email
                 }).ToList();
            }
            return userList;
        }
    }
}

