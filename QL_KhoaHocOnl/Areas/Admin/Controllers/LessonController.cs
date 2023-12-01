using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Areas.Admin.Controllers
{
    public class LessonController : Controller
    {
        // GET: Admin/Lesson
        QL_COURSEEntities db = new QL_COURSEEntities();
        //
        // GET: /Lesson/
        public ActionResult ListLESSON(string search = "")
        {
            List<LESSON> listLesson = db.LESSONs.Where(row => row.TITLE_LESSON.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(listLesson);
        }
        public ActionResult AddLesson()
        {

            ViewBag.COURSE = db.COURSEs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddLesson(LESSON lesson)
        {
            if (string.IsNullOrEmpty(lesson.ID_LESSON) == true)
            {
                ModelState.AddModelError("", "Mã Bài Học không được trống");
                return View(lesson);
            }

            if (string.IsNullOrEmpty(lesson.ID_COURSE) == true)
            {
                ModelState.AddModelError("", "Mã COURSE không được trống");
                return View(lesson);
            }

            if (string.IsNullOrEmpty(lesson.TITLE_LESSON) == true)
            {
                ModelState.AddModelError("", "Tiêu đề không được trống");
                return View(lesson);
            }

            if (ModelState.IsValid)
            {
                db.LESSONs.Add(lesson);
                db.SaveChanges();
                db.PRO_UPDATE_COUNTLESSONS(); 
                return RedirectToAction("ListLESSON");

            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(lesson);
            }
        }

        public ActionResult Edit(string id = "")
        {
            var coursemodel = db.LESSONs.Find(id);

            return View(coursemodel);
        }
        [HttpPost]
        public ActionResult Edit(LESSON lesson)
        {
            DateTime currentDateTime = DateTime.Now;
            string cur = currentDateTime.ToString("dd/MM/yyyy");

            if (string.IsNullOrEmpty(lesson.ID_LESSON) == true)
            {
                ModelState.AddModelError("", "Mã Bài Học không được trống");
                return View(lesson);
            }

            //if (string.IsNullOrEmpty(lesson.ID_COURSE) == true)
            //{
            //    ModelState.AddModelError("", "Mã COURSE không được trống");
            //    return View(lesson);
            //}

            if (string.IsNullOrEmpty(lesson.TITLE_LESSON) == true)
            {
                ModelState.AddModelError("", "Tiêu đề không được trống");
                return View(lesson);
            }
            var updateModel = db.LESSONs.Find(lesson.ID_LESSON);
            // gắn giá trị mới cho đối tượng
            updateModel.ID_COURSE = lesson.ID_COURSE;
            updateModel.TITLE_LESSON = lesson.TITLE_LESSON;
            updateModel.DESCRIPTION_LESSON = lesson.DESCRIPTION_LESSON;
            updateModel.THUMBNAIL = lesson.THUMBNAIL;
            updateModel.LINK_LESSON = lesson.LINK_LESSON;
            updateModel.CREATED_AT = lesson.CREATED_AT;
            updateModel.UPDATED_AT = currentDateTime;
            var id = db.SaveChanges();
            if (id > 0)
            {
                db.SaveChanges();
                db.PRO_UPDATE_COUNTLESSONS();
                return RedirectToAction("ListLESSON");
            }
            else
            {
                ModelState.AddModelError("", " Không lưu được vào DB !");
                return View(lesson);
            }
        }
        public ActionResult Delete(string id = "")
        {
            var model = db.LESSONs.Find(id);
            db.LESSONs.Remove(model);
            db.SaveChanges();
            db.PRO_UPDATE_COUNTLESSONS();
            return RedirectToAction("ListLESSON");
        }
        public ActionResult Detail(string id = "")
        {
            LESSON lesson1 = db.LESSONs.Where(row => row.ID_LESSON == id).FirstOrDefault();
            if (id == "")
            {
                return HttpNotFound();
            }
            return View(lesson1);
        }
    }
}