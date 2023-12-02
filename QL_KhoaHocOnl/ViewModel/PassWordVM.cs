using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_KhoaHocOnl.ViewModel
{
    public class PassWordVM
    {
        [Required(ErrorMessage = "Password cannot be blank")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [Compare("NewPassword", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }
    }
}