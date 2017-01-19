using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMUser
    {
        public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }

        public string username { get; set; }
        public string pwd { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        public string email { get; set; }
    }

}
