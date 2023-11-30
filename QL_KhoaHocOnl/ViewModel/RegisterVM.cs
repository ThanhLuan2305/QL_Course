using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_KhoaHocOnl.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email cannot be blank")]
        [EmailAddress(ErrorMessage = "")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Fullname { get; set; }
    }
}