using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMEmployee
    {
        public int EmployeeId { get; set; }
        public int AdharcardId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public string Note { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Updated { get; set; }
        private string _username = "";
        public string EmplUserName
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                {
                    _username = BUser.GetAllUser().FirstOrDefault(x => x.Employeeid == EmployeeId).Username;
                }
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        private string _password = "";
        public string EmpPassword
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    _password = BUser.GetAllUser().FirstOrDefault(x => x.Employeeid == EmployeeId).Password;
                }                
                return _password;
            }
            set
            {
                _password = value;
            }
        }
    }
}
