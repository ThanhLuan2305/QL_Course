using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        QL_COURSEEntities db = new QL_COURSEEntities();
        public ActionResult Index(string search = "", string sort = "", int page = 1)
        {
            List<COURSE> list = db.COURSEs.Where(x=>x.NAME_COURSE.Contains(search)).ToList();
            ViewBag.TB = search;

            //Sort
            ViewBag.Sort = sort;    
            if (sort == "Up")
            {
                list = list.OrderBy(x => x.PRICE_COURSE).ToList();
            }
            else
            {
                list = list.OrderByDescending(x => x.PRICE_COURSE).ToList();
            }

            // Paging 
            int NoOfRecordPerPage = 9;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(list.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            list = list.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();
            return View(list);
        }
        public ActionResult Detail(string id)
        {
            COURSE course = db.COURSEs.Where(c => c.ID_COURSE == id).FirstOrDefault();
            Session["ID"] = id;
            return View(course);
        }
        public ActionResult Lesson(string id)
        {
            List<LESSON> lst = db.LESSONs.Where(c => c.ID_COURSE == id).ToList();
            int i = lst.Count;
            ViewBag.Count = i;
            return View(lst);
        }
        public ActionResult DetailLesson(string id)
        {
            LESSON ls = db.LESSONs.Where(x => x.ID_LESSON == id).FirstOrDefault();
            return View(ls);
        }
    }
}