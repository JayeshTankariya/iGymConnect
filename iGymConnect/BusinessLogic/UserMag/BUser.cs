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
            using (var context = new UserLoginEntities2())
            {
                userList = context.UserLogins
                    .Where(x => x.Username == user.Username && x.Password == user.Password)
                    .Select(x => new OMUser
                    {
                        EmailId = x.EmailId,
                        Username = x.Username,
                        Password = x.Password
                    }).ToList();
            }
            return userList;
        }


    }
}
