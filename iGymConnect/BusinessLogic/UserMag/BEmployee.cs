using BusinessLogic.ObjectModel;
using DataLogic.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BEmployee
    {
        public static List<OMEmployee> GetAllByEmployee()
        {
            var EmployeeList = new List<OMEmployee>();
            using (var context = new UserLoginEntities1())
            {
                EmployeeList = context.EmployeeMasters
                    .Where(x => !x.Deleted)
                    .Select(x => new OMEmployee
                    {
                        EmployeeId = x.EmployeeId,
                        AdharcardId = x.AdharcardId.HasValue ? x.AdharcardId.Value : 0,
                        MemberId = x.MemberId.HasValue ? x.MemberId.Value : 0,
                        FullName = x.FullName,
                        Address = x.Address,
                        City = x.City,
                        State = x.State,
                        Zip = x.Zip.HasValue ? x.Zip.Value : 0,
                        Position = x.Position,
                        HireDate=x.HireDate ?? DateTime.Now,
                        Note=x.Note,
                    }).ToList();
            }
            return EmployeeList;
        }
        public static List<OMEmployee> SaveEmployee(OMEmployee emp)
        {
            var employeelist = new List<OMEmployee>();
            EmployeeMaster employee = new EmployeeMaster();

            employee.EmployeeId = emp.EmployeeId;
            employee.AdharcardId = emp.AdharcardId;
            employee.MemberId = emp.MemberId;
            employee.FullName = emp.FullName;
            employee.Address = emp.Address;
            employee.City = emp.City;
            employee.State = emp.State;
            employee.Zip = emp.Zip;
            employee.Position = emp.Position;
            employee.HireDate = emp.HireDate;
            employee.Note = emp.Note;
            employee.CreatedBy = 1;
            employee.Deleted = false;
            employee.DateCreated = DateTime.Now;

            using (var e = new UserLoginEntities1())
            {
                e.EmployeeMasters.Add(employee);
                e.SaveChanges();
                employeelist = GetAllByEmployee();
            }
            return employeelist;
        }
        public static List<OMEmployee> Deleteemp(int EmployeeId)
        {
            var employeelist = new List<OMEmployee>();
            using (var e = new UserLoginEntities1())
            {
                var delemp = e.EmployeeMasters.FirstOrDefault(x => x.EmployeeId == EmployeeId);
                e.EmployeeMasters.Remove(delemp);
                e.SaveChanges();
                employeelist = GetAllByEmployee();
            }
            return employeelist;
        }
    }
}
