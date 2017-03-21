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
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Reports()
        {
            return View();
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