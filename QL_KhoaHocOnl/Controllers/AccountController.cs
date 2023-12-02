using QL_KhoaHocOnl.Models;
using QL_KhoaHocOnl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Handlers;

namespace QL_KhoaHocOnl.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        QL_COURSEEntities db = new QL_COURSEEntities();
        public ActionResult DetailUser(int id)
        {
            if (Request.Cookies["User"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                USER_COURSE u = db.USER_COURSE.Where(x => x.ID_USER == id).FirstOrDefault();  
                return View(u);
            }
        }
        public ActionResult Edit(int id)
        {
            var user = db.USER_COURSE.Find(id); 
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(USER_COURSE u)
        {
            if (string.IsNullOrEmpty(u.FULLNAME_USER) == true)
            {
                ModelState.AddModelError("", "Tài Khoản không được trống");
                return View();
            }
            if (u.BIRTHDAY == null)
            {
                ModelState.AddModelError("", "Ngày sinh không được trống");
                return View();
            }
            if (string.IsNullOrEmpty(u.PHONE_USER) == true)
            {
                ModelState.AddModelError("", "Số điện thoại không được trống");
                return View();
            }
            if (string.IsNullOrEmpty(u.STATUS_USER) == true)
            {
                ModelState.AddModelError("", "Trạng thái  không được trống");
                return View();
            }

            var user = db.USER_COURSE.Find(u.ID_USER);
            user.FULLNAME_USER = u.FULLNAME_USER;
            user.BIRTHDAY = u.BIRTHDAY;
            user.PHONE_USER = u.PHONE_USER;
            user.STATUS_USER = u.STATUS_USER;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            // db.Products.Add(p);
            // db.SaveChanges();
            return View();
        }
        public ActionResult Register()
        {
            // db.Products.Add(p);
            // db.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM rgt)
        {
            if (ModelState.IsValid)
            {
                if (db.USER_COURSE.Where(x => x.ROLE_USER == "Admin").FirstOrDefault() == null)
                {
                    USER_COURSE u = new USER_COURSE();
                    u.ROLE_USER = "Admin";
                    u.USERNAME = "admin";
                    u.PASSWORD = mahoa("admin123");
                    u.EMAIL_USER = "admin@gmail.com";
                    db.USER_COURSE.Add(u);
                    db.SaveChanges();
                }
                USER_COURSE user = new USER_COURSE();
                user.ROLE_USER = "Student";
                user.USERNAME = rgt.Username;
                user.PASSWORD = mahoa(rgt.Password);
                user.EMAIL_USER = rgt.Email;
                user.FULLNAME_USER=rgt.Fullname;
                db.USER_COURSE.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            Password = mahoa(Password);
            string role = db.USER_COURSE.Where(x => x.USERNAME == Username).Select(x => x.ROLE_USER).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (role == "Admin")
                {
                    if (db.USER_COURSE.Where(x => x.USERNAME == Username).Where(x => x.PASSWORD == Password).FirstOrDefault() != null)
                    {

                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                        return View();
                }
                else
                {
                    if (db.USER_COURSE.Where(x => x.USERNAME == Username).Where(x => x.PASSWORD == Password).FirstOrDefault() != null)
                    {
                        HttpCookie cookie = new HttpCookie("User");
                        cookie.Values["Username"] = Username;
                        cookie.Values["Password"] = Password;
                        cookie.Values["ID"] = (db.USER_COURSE.Where(x => x.USERNAME == Username).Select(x => x.ID_USER).FirstOrDefault()).ToString();
                        Session["Fullname"] = db.USER_COURSE.Where(x => x.USERNAME == Username).Select(x => x.FULLNAME_USER).FirstOrDefault();
                        Session["CartItem"] = db.CART_OF_USER.Join(db.COURSEs, x => x.ID_COURSE, y => y.ID_COURSE, (x, y) => new { x, y }).ToList();
                        cookie.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }

                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["User"] != null)
            {
                HttpCookie cookie = new HttpCookie("User");
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Set(cookie);

                Session.Abandon();
                if (Request.Cookies["User"] == null)
                    return RedirectToAction("Index", "Home");
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(PassWordVM p)
        {
            string pass = mahoa(p.OldPassword);
            var u = db.USER_COURSE.Where(x => x.PASSWORD == pass).FirstOrDefault();
            if (u == null)
            {
                ModelState.AddModelError("", "Mật khẩu củ không đúng");
                return View();
            }
            else
            {
                u.PASSWORD = mahoa(p.NewPassword);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        public string mahoa(string pass)
        {
            string HashPass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass.Trim(), "SHA1");
            return HashPass;
        }
    }
}