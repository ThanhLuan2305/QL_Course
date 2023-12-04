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

        public ActionResult CheckOut()
        {
            if (Request.Cookies["User"] != null)
            {
                List<CART_OF_USER> listCart = GetCart();
                int idUser = Int32.Parse(Request.Cookies["User"]["ID"]);
                List<CART_OF_USER> listCourseUserNow = db.CART_OF_USER.Where(item => item.ID_USER == idUser).ToList();
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
                        CART_OF_USER delItem = db.CART_OF_USER.Where(x => x.ID_COURSE == item.ID_COURSE).Where(x => x.ID_USER == item.ID_USER).FirstOrDefault();
                        db.CART_OF_USER.Remove(delItem);
                        db.SaveChanges();
                    }
                    Session["Quantity"] = "0";
                    Session["ViewCart"] = null;
                    Session["CartItem"] = null;
                    return RedirectToAction("Order");
                }
                else
                    return RedirectToAction("Cart", "Cart");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult Order()
        {

            int idUser = Int32.Parse(Request.Cookies["User"]["ID"]);
            List<ORDER_COURSE> orderUser = db.ORDER_COURSE.Where(item => item.ID_USER == idUser).ToList();
            List<COURSE> ListOrder = new List<COURSE>();
            foreach (var item in orderUser)
            {
                foreach (var itemCouse in db.COURSEs.ToList())
                {
                    if (itemCouse.ID_COURSE == item.ID_COURSE)
                        ListOrder.Add(itemCouse);
                }
            }
            if (ListOrder == null)
            {
                ViewBag.NoCart = "Chưa mua khoá học";
                return View();
            }
            else
                return View(ListOrder);
        }
    }
}