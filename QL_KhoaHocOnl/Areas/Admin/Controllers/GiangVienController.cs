using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Areas.Admin.Controllers
{
    public class GiangVienController : Controller
    {
        QL_COURSEEntities db = new QL_COURSEEntities();
        //
        // GET: /GiangVien/

        public ActionResult ListTeacher(string search = "")
        {
            List<TEACHER> listTeacher = db.TEACHER.Where(row => row.NAME_TEACHER.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(listTeacher);
        }

        public ActionResult AddTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(TEACHER teacher)
        {
            if (string.IsNullOrEmpty(teacher.ID_TEACHER) == true)
            {
                ModelState.AddModelError("", "Mã Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.NAME_TEACHER) == true)
            {
                ModelState.AddModelError("", "Tên Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.LEVEL_TEACHER) == true)
            {
                ModelState.AddModelError("", "Cấp Độ Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.STATUS_TEACHER) == true)
            {
                ModelState.AddModelError("", " Trạng Thái Giảng Viên không được trống");
                return View(teacher);
            }
            if (string.IsNullOrEmpty(teacher.THUMBNAIL) == true)
            {
                ModelState.AddModelError("", " Hình Ảnh Giảng Viên không được trống");
                return View(teacher);
            }

            if (ModelState.IsValid)
            {
                db.TEACHER.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("ListTeacher");
            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(teacher);
            }
        }


        public ActionResult Edit(string id = "")
        {
            var coursemodel = db.TEACHER.Find(id);
            return View(coursemodel);
        }
        [HttpPost]
        public ActionResult Edit(TEACHER teacher)
        {
            if (string.IsNullOrEmpty(teacher.ID_TEACHER) == true)
            {
                ModelState.AddModelError("", "Mã Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.NAME_TEACHER) == true)
            {
                ModelState.AddModelError("", "Tên Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.LEVEL_TEACHER) == true)
            {
                ModelState.AddModelError("", "Cấp Độ Giảng Viên không được trống");
                return View(teacher);
            }

            if (string.IsNullOrEmpty(teacher.STATUS_TEACHER) == true)
            {
                ModelState.AddModelError("", " Trạng Thái Giảng Viên không được trống");
                return View(teacher);
            }
            if (string.IsNullOrEmpty(teacher.THUMBNAIL) == true)
            {
                ModelState.AddModelError("", " Hình Ảnh Giảng Viên không được trống");
                return View(teacher);
            }
            // Lưu
            // tìm đối tượng sửa
            var updateModel = db.TEACHER.Find(teacher.ID_TEACHER);
            // gắn giá trị mới cho đối tượng
            updateModel.NAME_TEACHER = teacher.NAME_TEACHER;
            updateModel.STATUS_TEACHER = teacher.STATUS_TEACHER;
            updateModel.THUMBNAIL = teacher.THUMBNAIL;
            updateModel.D_CREATED = teacher.D_CREATED;
            updateModel.LEVEL_TEACHER = teacher.LEVEL_TEACHER;
            var id = db.SaveChanges();
            if (id > 0)
            {
                return RedirectToAction("ListTEACHER");
            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(teacher);
            }

        }

        public ActionResult Detail(string id = "")
        {
            TEACHER teacher = db.TEACHER.Where(row => row.ID_TEACHER == id).FirstOrDefault();
            if (id == "")
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        public ActionResult Delete(string id = "")
        {
            var model = db.TEACHER.Find(id);
            db.TEACHER.Remove(model);
            db.SaveChanges();
            return RedirectToAction("ListTeacher");
        }
    }
}