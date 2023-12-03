using QL_KhoaHocOnl.Models;
using QL_KhoaHocOnl.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_KhoaHocOnl.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        QL_COURSEEntities db = new QL_COURSEEntities();
        public List<CART_OF_USER> GetCart()
        {
            List<CART_OF_USER> listCart = Session["CartItem"] as List<CART_OF_USER>;
            Session["CartItem"] = listCart;
            if (listCart == null)
            {
                listCart = new List<CART_OF_USER>();
                Session["CartItem"] = listCart;
            }
            return listCart;
        }

        public List<CartVM> GetViewOrder()
        {
            List<CartVM> listCourse = Session["ViewOrder"] as List<CartVM>;
            Session["ViewOrder"] = listCourse;
            if (listCourse == null)
            {
                listCourse = new List<CartVM>();
                Session["ViewOrder"] = listCourse;
            }
            return listCourse;
        }


        public ActionResult CheckOut()
        {
            if (Request.Cookies["User"] != null)
            {
                List<CART_OF_USER> listCart = GetCart();
                List<CartVM> listOrder = GetViewOrder();
                int idUser = Int32.Parse(Request.Cookies["User"]["ID"]);
                List<CART_OF_USER> listCourseUserNow = listCart.Where(item => item.ID_USER == idUser).ToList();
                if (listCourseUserNow != null)
                {
                    foreach (var item in listCourseUserNow)
                    {
                        string idOrder = (10001 + db.ORDER_COURSE.Count()).ToString();
                        ORDER_COURSE orderNew = new ORDER_COURSE();
                        orderNew.ID_ORDER = idOrder;
                        orderNew.ID_USER = idUser;
                        orderNew.ID_COURSE = item.ID_COURSE;
                        orderNew.TIME_AT_ORDER = DateTime.Now;
                        orderNew.STATUS_ORDER = "Mua";

                        db.ORDER_COURSE.Add(orderNew);
                        db.SaveChanges();

                        CartVM itemcourse = new CartVM(item.ID_COURSE, idUser);
                        listOrder.Add(itemcourse);

                        CART_OF_USER delItem = db.CART_OF_USER.Where(x => x.ID_COURSE == item.ID_COURSE).Where(x => x.ID_USER == idUser).FirstOrDefault();
                        db.CART_OF_USER.Remove(delItem);
                        db.SaveChanges();
                        listCart.Remove(delItem);
                    }
                    Session["ViewCart"] = null;
                    Session["CartItem"] = null;
                    return RedirectToAction("Order");
                }
                else
                    return RedirectToAction("Cart", "Cart");
            }
            else
                return RedirectToAction("Login", "AccountController");
        }

        public ActionResult Order()
        {
            if (Session["ViewOrder"] == null)
            {
                ViewBag.NoCart = "Chưa mua khoá học";
            }
            else
            {
                List<CartVM> listOrder = GetViewOrder();
                return View(listOrder);
            }
            return View();
        }
    }
}