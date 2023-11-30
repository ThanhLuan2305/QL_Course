using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.SqlClient;
using QL_KhoaHocOnl.Models;
using System.ComponentModel;
using QL_KhoaHocOnl.ViewModel;
using System.Web.Http;

namespace QL_KhoaHocOnl
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (Request.Cookies["User"] != null)
            {
                QL_COURSEEntities db = new QL_COURSEEntities();
                int id = Int32.Parse(Request.Cookies["User"]["ID"]);
                Session["Fullname"] = db.USER_COURSE.Where(x => x.ID_USER == id).Select(x => x.FULLNAME_USER).FirstOrDefault();
                Session["checkCart"] = "0";
                List<CART_OF_USER> listCart = Session["CartItem"] as List<CART_OF_USER>;
                if (listCart == null)
                {
                    listCart = new List<CART_OF_USER>();
                    listCart = db.CART_OF_USER.Where(x => x.ID_USER == id).ToList();
                    Session["CartItem"] = listCart;

                    List<CartVM> listCourse = new List<CartVM>();
                    foreach (var item in listCart)
                    {
                        CartVM itemcourse = new CartVM(item.ID_COURSE, item.ID_USER);
                        listCourse.Add(itemcourse);
                    }
                    Session["ViewCart"] = listCourse;
                }
            }
        }
    }
}
