using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iGymConnect.Controllers
{
    public class MamberShipController : Controller
    {
        // GET: MamberShip
        public ActionResult MembershipView()
        {
            return View();
        }

        //************Member****************

        public ActionResult GetMember()
        {
            var members = BMember.GetAllByMember();
            return View("_Member", members);
        }
        public ActionResult Save(OMMember member)
        {
            var mem = BMember.Save(member);
            return Json(mem);
        }
        public ActionResult ShowMemberDetails(int MemberId)
        {
            var mem = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == MemberId);
            return Json(mem, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMember(int MemberId)
        {
            var mem = BMember.Deletemem(MemberId);
            return Json(mem);
        }


        //**********MemberShip**************

        public ActionResult GetMembership()
        {
            var membership = BMembership.GetAllByMembership();
            return View("_Membership", membership);
        }
        public ActionResult SaveMemship(OMMembership membership)
        {
            var memship = BMembership.SaveMemship(membership);
            return Json(memship);
        }

        public ActionResult ShowMembershipDetails(int MembershipTypeId)
        {
            var memship = BMembership.GetAllByMembership().FirstOrDefault(x => x.MembershipTypeId == MembershipTypeId);
            return Json(memship, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMembership(int MembershipTypeId)
        {
            var memship = BMembership.Deletememship(MembershipTypeId);
            return Json(memship);
        }

        //***************Employee***************

        public ActionResult GetEmployee()
        {
            var emp = BEmployee.GetAllByEmployee();
            return View("_Employee", emp);
        }

       public ActionResult SaveEmployee(OMEmployee emp)
        {
            var employee = BEmployee.SaveEmployee(emp);
            return Json(employee);
        }
        public ActionResult ShowEmployeeDetails(int EmployeeId)                                                                                   
        {
            var emp = BEmployee.GetAllByEmployee().FirstOrDefault(x => x.EmployeeId == EmployeeId);
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteEmployee(int EmployeeId)
        {
            var emp = BEmployee.Deleteemp(EmployeeId);
            return Json(emp);
        }
    }
}