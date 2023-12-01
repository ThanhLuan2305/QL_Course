using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KHOnl_CF.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        QL_COURSEEntities db = new QL_COURSEEntities();
        public ActionResult Index()
        {
            var lst = db.TEACHERs.ToList(); 
            return View(lst);
        }
        public ActionResult Detail(string id)
        {
            TEACHER t = db.TEACHERs.Where(x => x.ID_TEACHER == id).FirstOrDefault();    
            return View(t);
        }
    }
}