using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_KhoaHocOnl.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50, MinimumLength = 8, ErrorMessage ="Mật khẩu phải dài từ 8 ký tự trở lên")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Nhập lại mật khẩu không được để trống")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string Fullname { get; set; }
    }
}