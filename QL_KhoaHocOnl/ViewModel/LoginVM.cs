using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_KhoaHocOnl.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }
    }
}