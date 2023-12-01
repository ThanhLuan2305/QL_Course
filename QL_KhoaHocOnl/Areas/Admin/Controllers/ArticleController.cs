using QL_KhoaHocOnl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Article()
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                return View(db.ARTICLE.ToList());
            }
        }

        [HttpPost]
        public ActionResult Article(string THUMBNAIL, int? ID, int ID_USER, string TITLE, string CONTENT, string STATUS_ARTICLE, string TYPE_ARTICLE)
        {
            System.Diagnostics.Debug.WriteLine(ID);
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                if (ID > 0 && ID != null)
                {
                    ARTICLE ArticleUpdate = db.ARTICLE.Where(x => x.ID == ID).FirstOrDefault();
                    ArticleUpdate.TITLE = TITLE;
                    ArticleUpdate.CONTENT = CONTENT;
                    ArticleUpdate.STATUS_ARTICLE = STATUS_ARTICLE;
                    ArticleUpdate.TYPE_ARTICLE = TYPE_ARTICLE;
                    ArticleUpdate.CONTENT = CONTENT;
                    ArticleUpdate.THUMBNAIL = THUMBNAIL;
                    ArticleUpdate.UPDATED_AT = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    db.ARTICLE.Add(new ARTICLE { ID_USER = ID_USER, TITLE = TITLE, CONTENT = CONTENT, STATUS_ARTICLE = STATUS_ARTICLE, THUMBNAIL = THUMBNAIL, TYPE_ARTICLE = TYPE_ARTICLE, UPDATED_AT = DateTime.Now, CREATED_AT = DateTime.Now });
                    db.SaveChanges();
                }
                return View(db.ARTICLE.ToList());
            }
        }

        public ActionResult DeleteArticle(int id)
        {
            using (QL_COURSEEntities db = new QL_COURSEEntities())
            {
                ARTICLE customer = db.ARTICLE.Where(x => x.ID == id).FirstOrDefault();
                db.ARTICLE.Remove(customer);
                db.SaveChanges();

                return RedirectToAction("Article");
            }
        }
    }
}