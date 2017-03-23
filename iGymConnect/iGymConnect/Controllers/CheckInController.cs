using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iGymConnect.Controllers
{
    public class CheckInController : Controller
    {
        // GET: CheckIn
        public ActionResult CheckIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveCheckIn(int MemberId)
        {
            var mem = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == MemberId);
            if (mem != null && mem.MemberId == MemberId)
            {
                var memship = BMembership.GetAllByMembership().FirstOrDefault(x => x.MembershipTypeId == mem.Membershiptypeid);
                if (memship.InActiveDate < DateTime.Now)
                {
                    return Json(new { isSuccess = false, responseMsg = "Your membership is expired, Please contact support administrator." });
                }
                else
                {
                    var checkin = BMCheckIn.SaveCheckIn(MemberId);
                    return Json(new { isSuccess = true, responseMsg = new { checkinDetails = checkin, memberDetails = mem, membershipdetail = memship } });
                }
            }
            else
            {
                return Json(new { isSuccess = false, responseMsg = "Member not found." });
            }

        }
    }
}