using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
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
            using (var context = new iGymConnectEntities())
            {
                EmployeeList = context.EmployeeMasters
                    .Where(x => !x.Deleted)
                    .Select(x => new OMEmployee
                    {
                        EmployeeId = x.EmployeeId,
                        AdharcardId = x.AdharcardId.HasValue ? x.AdharcardId.Value : 0,
                        FullName = x.FullName,
                        Address = x.Address,
                        City = x.City,
                        State = x.State,
                        Zip = x.Zip.HasValue ? x.Zip.Value : 0,
                        Position = x.Position,
                        HireDate = x.HireDate ?? DateTime.Now,
                        Note = x.Note,
                    }).ToList();
                

            }
            return EmployeeList;


        }
        public static List<OMEmployee> SaveEmployee(OMEmployee emp)
        {
            var employeelist = new List<OMEmployee>();
            EmployeeMaster employee = new EmployeeMaster();
            if (emp.EmployeeId > 0)
            {
                using (var e = new iGymConnectEntities())
                {
                    //employee.EmployeeId = emp.EmployeeId;
                    employee.AdharcardId = emp.AdharcardId;
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
                    e.SaveChanges();
                   
                }
            }
            else
            {

                //employee.EmployeeId = emp.EmployeeId;
                employee.AdharcardId = emp.AdharcardId;
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
                using (var e = new iGymConnectEntities())
                {
                    e.EmployeeMasters.Add(employee);
                    e.SaveChanges();
                    var curEmp = GetAllByEmployee().FirstOrDefault(x => x.FullName == emp.FullName);
                    if (curEmp != null)
                    {
                        OMUser user = new OMUser();
                        user.Employeeid = curEmp.EmployeeId;
                        user.Username = emp.EmplUserName;
                        user.Password = emp.EmpPassword;
                        BUser.UpdateUser(user);
                    }
                    //e.SaveChanges();
                    employeelist = GetAllByEmployee();
                   
                }
            }
            return employeelist;
        }
        public static List<OMEmployee> Deleteemp(int EmployeeId)
        {
            var employeelist = new List<OMEmployee>();
            using (var e = new iGymConnectEntities())
            {
                var delemp = e.EmployeeMasters.FirstOrDefault(x => x.EmployeeId == EmployeeId);
                delemp.Deleted = true;
                e.SaveChanges();
                employeelist = GetAllByEmployee();
            }
            return employeelist;
        }
    }
}
