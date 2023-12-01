using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User

        public ActionResult UserCourse()
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                db.PRO_UPDATE_COUNTLESSONS();
                return View(db.USER_COURSE.ToList());
            }
        }
        [HttpPost]
        public ActionResult UserCourse(int? ID_USER, string FULLNAME_USER, string PASSWORD, string USERNAME, DateTime BIRTHDAY, string EMAIL_USER, string PHONE_USER, string ROLE_USER, string STATUS_USER)
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                if (ID_USER != null)
                {
                    USER_COURSE UserCourseUpdate = db.USER_COURSE.Where(x => x.ID_USER == ID_USER).FirstOrDefault();
                    UserCourseUpdate.USERNAME = USERNAME;
                    UserCourseUpdate.PASSWORD = PASSWORD;
                    UserCourseUpdate.BIRTHDAY = BIRTHDAY;
                    UserCourseUpdate.STATUS_USER = STATUS_USER;
                    UserCourseUpdate.FULLNAME_USER = FULLNAME_USER;
                    UserCourseUpdate.EMAIL_USER = EMAIL_USER;
                    UserCourseUpdate.PHONE_USER = PHONE_USER;
                    UserCourseUpdate.ROLE_USER = ROLE_USER;
                    db.SaveChanges();
                }
                else
                {
                    db.USER_COURSE.Add(new USER_COURSE { FULLNAME_USER = FULLNAME_USER, ROLE_USER = ROLE_USER, PHONE_USER = PHONE_USER, EMAIL_USER = EMAIL_USER, STATUS_USER = STATUS_USER, BIRTHDAY = BIRTHDAY, PASSWORD = PASSWORD, USERNAME = USERNAME });
                    db.PRO_UPDATE_COUNTLESSONS();
                    db.SaveChanges();
                }
                return View(db.USER_COURSE.ToList());
            }
        }

        public ActionResult DeleteUserCourse(int id)
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                USER_COURSE customer = db.USER_COURSE.Where(x => x.ID_USER == id).FirstOrDefault();
                db.USER_COURSE.Remove(customer);
                db.PRO_UPDATE_COUNTLESSONS();
                db.SaveChanges();

                return RedirectToAction("UserCourse", "User");
            }
        }
        public ActionResult DetailUser(int id)
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                USER_COURSE us = db.USER_COURSE.Where(row => row.ID_USER == id).FirstOrDefault();
                db.PRO_UPDATE_COUNTLESSONS();
                if (id <= 0)
                {
                    return HttpNotFound();
                }
                return View(us);
            }
        }
    }
}