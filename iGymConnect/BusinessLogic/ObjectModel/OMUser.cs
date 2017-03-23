using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.ObjectModel
{
    public class OMUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        public string Password { get; set; }     
        public string EmailId { get; set; }
        public int Employeeid { get; set; }
    }
}
