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
using BusinessLogic.ObjectModel;
using BusinessLogic.UserMag;


namespace iGymConnect.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Reports()
        {
            var abc = BMember.GetAllByMember().ToList();
            ViewBag.MemberList = abc;
            var abc1 = BCategory.GetAllCategories().ToList();
            ViewBag.CategoryList = abc1;
            
            return View();
        }

        //public ActionResult GetCardGenerated()
        //{
        //    Document document = new Document();
        //    MemoryStream stream = new MemoryStream();
        //    try
        //    {
        //        PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
        //        pdfWriter.CloseStream = false;
        //        document.Open();

        //        PdfPTable tableHeader = new PdfPTable(7);
        //        Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
        //        PdfPCell cell = new PdfPCell();
        //        cell = new PdfPCell(new Phrase("LIST OF CARDS GENERATED", arial));
        //        cell.Colspan = 7;
        //        cell.Border = 0;
        //        cell.PaddingBottom = 20f;
        //        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //        tableHeader.AddCell(cell);

        //        PdfPTable table = new PdfPTable(4);
        //        //PdfPCell cell1 = new PdfPCell(new Phrase("Demo Table"));

        //        Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
        //        PdfPCell cell1 = new PdfPCell();
        //        cell1 = new PdfPCell(new Phrase("CARD NUMBER", arial1));
        //        table.AddCell(cell1);
        //        cell1 = new PdfPCell(new Phrase("AMOUNT", arial1));
        //        table.AddCell(cell1);
        //        cell1 = new PdfPCell(new Phrase("EXPIRY DATE", arial1));
        //        table.AddCell(cell1);
        //        cell1 = new PdfPCell(new Phrase("ALLOCATED TO MEMBER", arial1));
        //        table.AddCell(cell1);

        //        foreach (var c in BCardMaster.GetAllCard())
        //        {
        //            table.AddCell(c.Number.ToString());
        //            table.AddCell(c.Amount.ToString());
        //            table.AddCell(c.ExpiryDate.ToString());
        //            table.AddCell(c.IsUsed.ToString());
        //        }
        //        document.Add(tableHeader);
        //        document.Add(table);
        //        document.Close();
        //        stream.Flush();
        //        stream.Position = 0;

        //    }
        //    catch (DocumentException de)
        //    {
        //        Console.Error.WriteLine(de.Message);
        //    }
        //    catch (IOException ioe)
        //    {
        //        Console.Error.WriteLine(ioe.Message);
        //    }            
        //    return File(stream, "application/pdf", "CardGeneratedReport.pdf");
        //}
        public ActionResult GetCardGenerated()
        {
            var document = new Document();
            var strPDFfilename = string.Format("ListOfCardAllocated" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(4);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF INVENTORIES", arial));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(4);
            table.SpacingBefore = 5f;
            table.SpacingBefore = 10f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("CARD NUMBER", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("AMOUNT", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("EXPIRY DATE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("ALLOCATED TO MEMBER", arial1));
            table.AddCell(cell1);

            foreach (var c in BCardMaster.GetAllCard())
            {
                table.AddCell(c.Number.ToString());
                table.AddCell(c.Amount.ToString());
                table.AddCell(c.ExpiryDate.ToString());
                table.AddCell(c.IsUsed.ToString());
            }
            document.Add(tableHeader);
            document.Add(table);
            document.Close();

            return Json(strPDFfilename);
        }
        public ActionResult GetInventory()
        {
            var document = new Document();
            var strPDFfilename = string.Format("GetListOfInv" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(7);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF INVENTORIES", arial));
            cell.Colspan = 7;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(7);
            table.SpacingBefore = 5f;
            table.SpacingBefore = 10f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("ITEM", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("BARCODE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("FROM VENDOR", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("WHICH CATEGORY", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("COST", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("DISCOUNT", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("TOTAL", arial1));
            table.AddCell(cell1);

            foreach (var b in BInventory.GetAllInventory())
            {
                var xyz = BCategory.GetAllCategories().FirstOrDefault(x => x.Id == b.CategoryId);
                var xyz1 = BVendor.GetAllVendors().FirstOrDefault(x => x.Id == b.VendorId);
                table.AddCell(b.Item);
                table.AddCell(b.Barcode);
                table.AddCell(xyz1.Name);
                table.AddCell(xyz.CategoryName);
                table.AddCell(b.Cost.ToString());
                table.AddCell(b.Discount.ToString());
                table.AddCell(b.Total.ToString());
            }
            document.Add(tableHeader);
            document.Add(table);
            document.Close();

            return Json(strPDFfilename);
        }
        public ActionResult GetCategory()
        {           
            var document = new Document();
            var strPDFfilename = string.Format("GetListOfCat" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(3);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF CATEGORIES", arial));
            cell.Colspan = 3;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(3);
            table.SpacingBefore = 5f;
            table.SpacingBefore = 10f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("CATEGORY NAME", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("DESCRIPTION", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("IMAGE", arial1));
            table.AddCell(cell1);

            foreach (var c in BCategory.GetAllCategories())
            {
                table.AddCell(c.CategoryName);
                table.AddCell(c.Description);
                table.AddCell(c.Image);
            }
            document.Add(tableHeader);
            document.Add(table);
            document.Close();

            return Json(strPDFfilename);
        }

        public ActionResult AllocateCard(int Id)
        {
            var a = BPOSCardMaster.GetCardByMember(Id).FirstOrDefault(x => x.Id == Id);
           
            var document = new Document();
            var strPDFfilename = string.Format("GetAllocatedCards" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename) , FileMode.Create);
            
            var writer = PdfWriter.GetInstance(document, output);
                        
            document.Open();
            PdfPTable tableHeader = new PdfPTable(4);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF CARDS ALLOCATED TO MEMBERS", arial));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(4);
            table.SpacingBefore = 5f;
            table.SpacingBefore = 10f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("CARD NUMBER", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("AMOUNT", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("EXPIRY DATE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("MEMBER", arial1));
            table.AddCell(cell1);
            var abc = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == Id);
            foreach (var card in BPOSCardMaster.GetCardByMember(Id))
            {

                table.AddCell(card.Number.ToString());
                table.AddCell(card.Amount.ToString());
                table.AddCell(card.ExpiryDate.ToString());
                table.AddCell(abc.MemberName);
            }

            document.Add(tableHeader);
            document.Add(table);

            document.Close();
                                    
            return Json(strPDFfilename);
        }

        public ActionResult InvByCat(int Id)
        {
            var a = BInventory.GetInvByCat(Id).FirstOrDefault(x => x.Id == Id);

            var document = new Document();
            var strPDFfilename1 = string.Format("GetInvByCat" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename1), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
                PdfPTable tableHeader = new PdfPTable(7);
                Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Phrase("LIST OF INVENTORY BY CATEGORY", arial));
                cell.Colspan = 7;
                cell.Border = 0;
                cell.PaddingBottom = 20f;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableHeader.AddCell(cell);

                PdfPTable table = new PdfPTable(7);
                //table.SpacingBefore = 5f;
                table.SpacingBefore = 5f;

                Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
                PdfPCell cell1 = new PdfPCell();
                cell1 = new PdfPCell(new Phrase("ITEM", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("BARCODE", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("DESCRIPTION", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("COST", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("DISCOUNT", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("TOTAL", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("CATEGORY", arial1));
                table.AddCell(cell1);

            var abc = BCategory.GetAllCategories().FirstOrDefault(x => x.Id == Id);
            foreach (var inv in BInventory.GetInvByCat(Id))
                {                
                    table.AddCell(inv.Item);
                    table.AddCell(inv.Barcode);
                    table.AddCell(inv.Description);
                    table.AddCell(inv.Cost.ToString());
                    table.AddCell(inv.Discount.ToString());
                    table.AddCell(inv.Total.ToString());                    
                    table.AddCell(abc.CategoryName);                
                }                
                document.Add(tableHeader);
                document.Add(table);
                document.Close();
            return Json(strPDFfilename1);
        }
        public ActionResult DateWiseTransaction(Nullable<DateTime> dt)
        {
            var a = BTransaction.GetAllTransaction().FirstOrDefault(x => x.TransactionDateTime > dt);

            var document = new Document();
            var strPDFfilename2 = string.Format("GetDateWiseTransaction" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename2), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(4);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF DATE WISE TRANSACTION", arial));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(4);
            //table.SpacingBefore = 5f;
            table.SpacingBefore = 5f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("MEMBER", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("TRANSACTION MODE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("TRANSACTION DATE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("REMARKS", arial1));
            table.AddCell(cell1);            
            foreach (var a1 in BTransaction.GetDateWiseTransaction(dt))
            {
                var xyz = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == a1.MemberId);
                table.AddCell(xyz.MemberName);
                table.AddCell(a1.TransactionMode);
                table.AddCell(a1.TransactionDateTime.ToString());
                table.AddCell(a1.Remarks);
            }

            document.Add(tableHeader);
            document.Add(table);
            document.Close();
            return Json(strPDFfilename2);
        }

        public ActionResult TranRange(Nullable<DateTime> dt, Nullable<DateTime> dt1)
        {
            var a = BTransaction.GetAllTransaction().FirstOrDefault(x => x.TransactionDateTime > dt && x.TransactionDateTime < dt1);

            var document = new Document();
            var strPDFfilename2 = string.Format("GetTranRange" + DateTime.Now.ToString("yyyyMMddhhmmss")) + ".pdf";
            var output = new FileStream(Server.MapPath("~/Content/PDF/" + strPDFfilename2), FileMode.Create);

            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            PdfPTable tableHeader = new PdfPTable(4);
            Font arial = FontFactory.GetFont("Arial", 16, Font.BOLDITALIC, BaseColor.BLUE);
            PdfPCell cell = new PdfPCell();
            cell = new PdfPCell(new Phrase("LIST OF TRANSACTION WITHIN DATE RANGE", arial));
            cell.Colspan = 4;
            cell.Border = 0;
            cell.PaddingBottom = 20f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            tableHeader.AddCell(cell);

            PdfPTable table = new PdfPTable(4);
            //table.SpacingBefore = 5f;
            table.SpacingBefore = 5f;

            Font arial1 = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLUE);
            PdfPCell cell1 = new PdfPCell();
            cell1 = new PdfPCell(new Phrase("TRANSACTION MODE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("TRANSACTION DATE", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("REMARKS", arial1));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("MEMBER", arial1));
            table.AddCell(cell1);
            
            foreach (var a1 in BTransaction.GetTranRange(dt, dt1))
            {
                var xyz = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == a1.MemberId);
                table.AddCell(a1.TransactionMode);
                table.AddCell(a1.TransactionDateTime.ToString());
                table.AddCell(a1.Remarks);
                table.AddCell(xyz.MemberName);
            }

            document.Add(tableHeader);
            document.Add(table);
            document.Close();
            return Json(strPDFfilename2);
        }
        public ActionResult EmployeeReport(string b1)
        {
            //var emp = BEmployee.GetAllByEmployee();
            Document document = new Document();
            MemoryStream stream = new MemoryStream();
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                PdfPTable tableHeader = new PdfPTable(6);
                Font arial = FontFactory.GetFont("Comic Sans MS", 16, BaseColor.BLUE);
                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Phrase("List of Employee", arial));
                cell.Colspan = 6;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableHeader.SpacingAfter = 5f;
                tableHeader.AddCell(cell);

                PdfPTable table = new PdfPTable(6);
                table.TotalWidth = 500f;
                Font arial1 = FontFactory.GetFont("Comic Sans MS", 12, BaseColor.BLUE);
                PdfPCell cell1 = new PdfPCell();
                cell1 = new PdfPCell(new Phrase("AdharCard", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("FullName", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("City", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Zip", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("HireDate", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Position", arial1));
                table.AddCell(cell1);

                foreach (var emp in BEmployee.GetAllByEmployee())
                {
                    table.AddCell(emp.AdharcardId.ToString());
                    table.AddCell(emp.FullName);
                    table.AddCell(emp.City);
                    table.AddCell(emp.Zip.ToString());
                    table.AddCell(emp.HireDate.ToString());
                    table.AddCell(emp.Position.ToString());

                }

                document.Add(tableHeader);
                document.Add(table);

                document.Close();
                stream.Flush();
                stream.Position = 0;


            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush();
            stream.Position = 0;

            return File(stream, "application/pdf", "EmployeeDetails.pdf");

        }
        public ActionResult MemberReport()
        {
            Document document = new Document();
            MemoryStream stream = new MemoryStream();
            try
            {

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                PdfPTable tableHeader = new PdfPTable(5);
                Font arial = FontFactory.GetFont("Comic Sans MS", 16, BaseColor.BLUE);
                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Phrase("List oF Member", arial));
                cell.Colspan = 5;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableHeader.SpacingAfter = 12f;
                tableHeader.AddCell(cell);

                PdfPTable table = new PdfPTable(5);
                Font arial1 = FontFactory.GetFont("Comic Sans MS", 12, BaseColor.BLUE);
                PdfPCell cell1 = new PdfPCell();
                cell1 = new PdfPCell(new Phrase("Member Id", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Member Name", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Barcode", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("State", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Phone", arial1));
                table.AddCell(cell1);

                table.HorizontalAlignment = Element.ALIGN_CENTER;

                foreach (var mem in BMember.GetAllByMember())
                {
                    table.AddCell(mem.MemberId.ToString());
                    table.AddCell(mem.MemberName);
                    table.AddCell(mem.Barcode.ToString());
                    table.AddCell(mem.State);
                    table.AddCell(mem.PhoneHome1.ToString());
                }

                document.Open();
                document.Add(tableHeader);
                document.Add(table);


            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush();
            stream.Position = 0;


            return File(stream, "application/pdf", "ListMemberDetails.pdf");

        }
        public ActionResult MembershipTypeDetail()
        {

            Document document = new Document();
            MemoryStream stream = new MemoryStream();
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                PdfPTable tableHeader = new PdfPTable(4);
                Font arial = FontFactory.GetFont("Comic Sans MS", 16, BaseColor.BLUE);
                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Phrase("List Of MembershipType", arial));
                cell.Colspan = 5;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableHeader.SpacingAfter = 12f;
                tableHeader.AddCell(cell);

                PdfPTable table = new PdfPTable(4);
                table.TotalWidth = 500f;
                Font arial1 = FontFactory.GetFont("Comic Sans MS", 12, BaseColor.BLUE);
                PdfPCell cell1 = new PdfPCell();
                cell1 = new PdfPCell(new Phrase("MemberhipTypeId", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Description", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Active Date", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("InActive Date", arial1));
                table.AddCell(cell1);

                foreach (var memship in BMembership.GetAllByMembership())
                {
                    table.AddCell(memship.MembershipTypeId.ToString());
                    table.AddCell(memship.Description);
                    table.AddCell(memship.ActiveDate.ToString());
                    table.AddCell(memship.InActiveDate.ToString());
                }
                document.Add(tableHeader);
                document.Add(table);

                document.Close();
                stream.Flush();
                stream.Position = 0;


            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush();
            stream.Position = 0;

            return File(stream, "application/pdf", "ListMembershiptypeDetail.pdf");

        }
        public ActionResult CheckInDetail()
        {

            Document document = new Document();
            MemoryStream stream = new MemoryStream();
            try
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;

                document.Open();
                PdfPTable tableHeader = new PdfPTable(3);
                Font arial = FontFactory.GetFont("Comic Sans MS", 16, BaseColor.BLUE);
                PdfPCell cell = new PdfPCell();
                cell = new PdfPCell(new Phrase("List Of Check-In", arial));
                cell.Colspan = 3;
                cell.Border = 0;
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tableHeader.SpacingAfter = 12f;
                tableHeader.AddCell(cell);

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 500f;
                Font arial1 = FontFactory.GetFont("Comic Sans MS", 12, BaseColor.BLUE);
                PdfPCell cell1 = new PdfPCell();


                cell1 = new PdfPCell(new Phrase("Member Name", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("In Time", arial1));
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("Out Time", arial1));
                table.AddCell(cell1);

                foreach (var memship in BMCheckIn.GetCheckindetails())
                {
                    var mem = BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == memship.Memberid);
                    table.AddCell(mem.MemberName);
                    table.AddCell(memship.InTime.ToString());
                    table.AddCell(memship.OutTime.ToString());
                }
                document.Add(tableHeader);
                document.Add(table);

                document.Close();
                stream.Flush();
                stream.Position = 0;


            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush();
            stream.Position = 0;

            return File(stream, "application/pdf", "ListCheckInDetail.pdf");

        }
    }
  }

       
