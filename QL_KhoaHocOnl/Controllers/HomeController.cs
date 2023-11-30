using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QL_COURSEEntities db = new QL_COURSEEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CourseBasic()
        {
            var list = db.COURSE.Where(x => x.LEVEL_COURSE.Contains("Cơ bản")).ToList();
            return View(list);
        }
        public ActionResult TeacherGS()
        {
            var list = db.TEACHER.Where(x => x.LEVEL_TEACHER.Contains("Giáo Sư")).ToList();
            return View(list);
        }
        public ActionResult BlogTop()
        {
            var list = db.ARTICLE.Take(3).ToList();
            return View(list);
        }
    }
}