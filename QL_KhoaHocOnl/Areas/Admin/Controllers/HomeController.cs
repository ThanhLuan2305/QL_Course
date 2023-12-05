using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QL_COURSEEntities db = new QL_COURSEEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var tongtien = (from orc in db.ORDER_COURSE
                            join course in db.COURSEs
                            on orc.ID_COURSE equals course.ID_COURSE
                            select course.PRICE_COURSE
                ).ToList();
            var ttt = tongtien.Sum();
            ViewBag.TinhTongTien = ttt;

            var SUser = (from user in db.USER_COURSE select user.ID_USER).ToList().Count();
            ViewBag.TongUser = SUser;

            var feedback = (from fb in db.FEEDBACKs select fb.RATE_FEEDBACK).Average();
            ViewBag.DanhGia = feedback;

            db.PRO_UPDATE_COUNTLESSONS();
            return View();
        }
        public ActionResult Course(string search = "")
        {
            List<COURSE> course = db.COURSEs.Where(row => row.NAME_COURSE.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(course);
        }

        public ActionResult AddCourse()
        {
            ViewBag.TYPE_COURSE = db.TYPE_COURSE.ToList();
            ViewBag.TEACHERs = db.TEACHERs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(COURSE course)
        {
            if (string.IsNullOrEmpty(course.ID_COURSE) == true)
            {
                ModelState.AddModelError("", "Mã Course không được trống");
                return View(course);
            }

            if (string.IsNullOrEmpty(course.ID_TYPECOURSE) == true)
            {
                ModelState.AddModelError("", "Loại COURSE không được trống");
                return View(course);
            }

            if (string.IsNullOrEmpty(course.ID_TEACHER) == true)
            {
                ModelState.AddModelError("", "Mã GV không được trống");
                return View(course);
            }

            if (course.PRICE_COURSE < 0)
            {
                ModelState.AddModelError("", " Giá phải lớn hơn hoặc bằng 0 (MIỄN PHÍ)");
                return View(course);
            }

            if (ModelState.IsValid)
            {
                //db.COURSEs.Add(course);
                db.AddNewCourse(course.ID_COURSE, course.ID_TYPECOURSE, course.ID_TEACHER, course.NAME_COURSE, course.DESCRIPTION_COURSE, course.PRICE_COURSE, course.STATUS_COURSE, course.THUMBNAIL, course.COUNT_LESSON_COURSE, course.LEVEL_COURSE);
                db.SaveChanges();
                return RedirectToAction("Course");
            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(course);
            }
        }

        public ActionResult Detail(string id = "")
        {
            COURSE course1 = db.COURSEs.Where(row => row.ID_COURSE == id).FirstOrDefault();
            if (id == "")
            {
                return HttpNotFound();
            }
            return View(course1);
        }

        public ActionResult Edit(string id = "")
        {
            var coursemodel = db.COURSEs.Find(id);
            return View(coursemodel);
        }
        [HttpPost]
        public ActionResult Edit(COURSE course)
        {
            if (string.IsNullOrEmpty(course.ID_COURSE) == true)
            {
                ModelState.AddModelError("", "Mã Course không được trống");
                return View(course);
            }

            if (string.IsNullOrEmpty(course.ID_TYPECOURSE) == true)
            {
                ModelState.AddModelError("", "Loại COURSE không được trống");
                return View(course);
            }

            if (string.IsNullOrEmpty(course.ID_TEACHER) == true)
            {
                ModelState.AddModelError("", "Mã GV không được trống");
                return View(course);
            }

            if (course.PRICE_COURSE < 0)
            {
                ModelState.AddModelError("", " Giá phải lớn hơn hoặc bằng 0 (MIỄN PHÍ)");
                return View(course);
            }
            // Lưu
            // tìm đối tượng sửa
            var updateModel = db.COURSEs.Find(course.ID_COURSE);
            // gắn giá trị mới cho đối tượng
            updateModel.NAME_COURSE = course.NAME_COURSE;
            updateModel.ID_TYPECOURSE = course.ID_TYPECOURSE;
            updateModel.ID_TEACHER = course.ID_TEACHER;
            updateModel.DESCRIPTION_COURSE = course.DESCRIPTION_COURSE;
            updateModel.STATUS_COURSE = course.STATUS_COURSE;
            updateModel.THUMBNAIL = course.THUMBNAIL;
            updateModel.COUNT_LESSON_COURSE = course.COUNT_LESSON_COURSE;
            updateModel.LEVEL_COURSE = course.LEVEL_COURSE;
            updateModel.PRICE_COURSE = course.PRICE_COURSE;
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Course");
            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(course);
            }

        }
        public ActionResult Delete(string id = "")
        {
            var model = db.COURSEs.Find(id);
            db.COURSEs.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Course");
        }
    }
}