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
            using (var context = new UserLoginEntities())
            {
                userList = context.user_login
                .Where(x => x.username == user.username && x.pwd == user.pwd)
                 .Select(x => new OMUser
                 {
                     email = x.email,
                     username = x.username,
                     pwd = x.pwd
                 }).ToList();
            }
            return userList;
        }
    }
}

