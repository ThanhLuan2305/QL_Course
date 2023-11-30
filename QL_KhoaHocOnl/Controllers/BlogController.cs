using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        QL_COURSEEntities db = new QL_COURSEEntities();
        public ActionResult Index()
        {
            var list = db.ARTICLEs.ToList();
            return View(list);
        }
        public ActionResult Detail(int id)
        {
            ARTICLE a = db.ARTICLEs.Where(x => x.ID == id).FirstOrDefault();
            return View(a);
        }
    }
}