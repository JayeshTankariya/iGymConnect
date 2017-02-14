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

        //Memberform Show//
        public ActionResult GetMember()
        {

            var members = BMember.GetAllByMember();
            var abc = BMembership.GetAllByMembership().ToList();
            ViewBag.membershiplist = abc;
            return View("_Member", members);

        }

        //Save Member Detail//
        [HttpPost]
        public ActionResult Save(OMMember mem, HttpPostedFileBase file)
        {

            if (file!=null && file.ContentLength > 0)
            {
                var fileName = file.FileName;
                var path = Server.MapPath("~/Content/MemberImg/") + fileName;
                file.SaveAs(path);
                mem.MemberImage = file.FileName;

            }
            
            var member = BMember.Save(mem);
            return Json(member);
        }

        //ShowMemberdetail//
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


        //Member Name Duplication//

       public bool CheckDuplicationMember(int MemberId, string MemberName)
       {
            var mem = BMember.GetAllByMember();
            if(MemberId > 0)
            {
                mem = mem.Where(x => x.MemberId != MemberId && x.MemberName == MemberName).ToList();
            }
            else
            {
                mem = mem.Where(x => x.MemberName == MemberName).ToList();
            }
            if(mem.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
       }


        //**********MemberShip**************//

        public ActionResult GetMembership()
        {
            var membership = BMembership.GetAllByMembership();
            return View("_Membership", membership);
        }
        [HttpPost]
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

        //Duplication Membership//
        public bool CheckDuplicationMembership(int MembershipTypeId, string Description)
        {
            var emp = BMembership.GetAllByMembership();
            if (MembershipTypeId > 0)
            {
                emp = emp.Where(x => x.MembershipTypeId != MembershipTypeId && x.Description == Description).ToList();
            }
            else
            {
                emp = emp.Where(x => x.Description == Description).ToList();
            }
            if (emp.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //***************Employee***************

        public ActionResult GetEmployee()
        {
            var emp = BEmployee.GetAllByEmployee();
            return View("_Employee", emp);
        }
       [HttpPost]
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

        public bool CheckDuplicationEmployee(int EmployeeId, string FullName)
        {
            var emp = BEmployee.GetAllByEmployee();
            if (EmployeeId > 0)
            {
                emp = emp.Where(x => x.EmployeeId != EmployeeId && x.FullName == FullName).ToList();
            }
            else
            {
                emp = emp.Where(x => x.FullName == FullName).ToList();
            }
            if (emp.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}