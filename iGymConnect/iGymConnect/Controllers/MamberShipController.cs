using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
            var abc = BMembership.GetAllByMembership().ToList();
            ViewBag.membershiplist = abc;
            return View("_Member", members);

        }

        [HttpPost]
        public ActionResult Save(OMMember mem, HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
            {
                var fileName = file.FileName;
                var path = Server.MapPath("~/Content/MemberImg/") + fileName;
                file.SaveAs(path);
                mem.MemberImage = file.FileName;

            }

            var member = BMember.Save(mem);
            return Json(member);
        }

        public ActionResult ShowMemberDetails(int MemberId)
        {
            var mem = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == MemberId);
            return Json(mem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMember(int MemberId)
        {
            var checkin = BMCheckIn.GetCheckindetails().FirstOrDefault(x => x.Memberid == MemberId);
            if (checkin != null)
            {
                return Json(new { responseMsg = "Member Is In Used,You Can't Delete The Member" });
            }
            else
            {
                var mem = BMember.Deletemem(MemberId);
                return Json(mem);
            }
        }
        [HttpPost]
        public ActionResult IdCard(int Id)
        {
            var mem = BMember.GetIdMember(Id).FirstOrDefault(x => x.MemberId == Id);
            var memship = BMembership.GetAllByMembership().FirstOrDefault(x => x.MembershipTypeId == mem.Membershiptypeid);
            var document = new Document();
            var strPDFfilename = string.Format("GetIDCard" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename), FileMode.Create);
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(2);
            iTextSharp.text.Font arial = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("ID CARD", arial));
            cell.Colspan = 2;
            cell.Border = 0;
            tableHeader.SpacingBefore = 20f;
            tableHeader.SpacingAfter = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);
            document.Add(tableHeader);


            PdfPTable table = new PdfPTable(2);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            string img = Server.MapPath("~/Content/MemberImg/") + mem.MemberImage;
            iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(img);
            //gif.ScalePercent(12f, 15.5f);
            // gif.ScaleAbsolute(10f,10f);
            gif.ScalePercent(10f);
            table.AddCell(gif);

            PdfPTable nested = new PdfPTable(1);
            nested.DefaultCell.Border = 0;
            nested.DefaultCell.Padding = 8;
            cell.Colspan = 2;
            cell.Rowspan = 7;
            nested.AddCell("MemberId  : " + mem.MemberId.ToString());
            nested.AddCell("Member Name: " + mem.MemberName);
            nested.AddCell("MemberShipType : " + memship.Description);
            nested.AddCell("State : " + mem.State);
            nested.AddCell("Zip : " + mem.Zip.ToString());
            nested.AddCell("Phone Number  : " + mem.PhoneHome1.ToString());
            nested.AddCell("Email :  : " + mem.Email);
            table.AddCell(nested);

            PdfPTable table1 = new PdfPTable(1);
            PdfPCell cell1 = new PdfPCell();
            table1.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table1.TotalWidth = 10f;
            //itextsharp.text.Font.FontFamily = 10f;
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
           // cell1.Width = 10f;
            table.AddCell(createBarcode(writer, mem.Barcode.ToString()));
            table.AddCell(table1);

            document.Add(table);
            document.Close();
            return Json(strPDFfilename);


        }
        public static PdfPCell createBarcode(PdfWriter writer, String code)
        {
            BarcodeEAN barcode = new BarcodeEAN();
           
            barcode.CodeType = Barcode.EAN8;
            barcode.Code = code;
            barcode.BarHeight = 5f;
            barcode.Size = 10f;
            barcode.Font = null;
            
            PdfPCell cell = new PdfPCell(barcode.CreateImageWithBarcode(writer.DirectContent, BaseColor.BLACK, BaseColor.GRAY), true);
            return cell;
        }

        public bool CheckDuplicationMember(int MemberId, string MemberName)
        {
            var mem = BMember.GetAllByMember();
            if (MemberId > 0)
            {
                mem = mem.Where(x => x.MemberId != MemberId && x.MemberName == MemberName).ToList();
            }
            else
            {
                mem = mem.Where(x => x.MemberName == MemberName).ToList();
            }
            if (mem.Count > 0)
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