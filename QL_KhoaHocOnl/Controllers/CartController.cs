using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using QL_KhoaHocOnl.Models;
using QL_KhoaHocOnl.ViewModel;

namespace QL_KhoaHocOnl.Controllers
{
    public class CartController : Controller
    {
        QL_COURSEEntities db = new QL_COURSEEntities();
        // GET: Cart

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

        public List<CartVM> GetViewCart()
        {
            List<CartVM> listCourse = Session["ViewCart"] as List<CartVM>;
            Session["ViewCart"] = listCourse;
            if (listCourse == null)
            {
                listCourse = new List<CartVM>();
                Session["ViewCart"] = listCourse;
            }
            return listCourse;
        }

        public ActionResult AddCart(string idCourse)
        {
            List<CART_OF_USER> listCart = GetCart();
            List<CartVM> listCourse = GetViewCart();
            CartVM course = listCourse.Where(item => item.ID_COURSE == idCourse).FirstOrDefault();
            if (course == null)
            {
                if (Request.Cookies["User"] != null)
                {
                    int id = Int32.Parse(Request.Cookies["User"]["ID"]);
                    if (db.ORDER_COURSE.Where(item => item.ID_USER == id).Where(item => item.ID_COURSE == idCourse).FirstOrDefault() == null)
                    {
                        CART_OF_USER newItem = new CART_OF_USER();
                        newItem.ID_USER = id;
                        newItem.ID_COURSE = idCourse;
                        db.CART_OF_USER.Add(newItem);
                        db.SaveChanges();
                        listCart.Add(newItem);

                        CartVM itemcourse = new CartVM(idCourse, id);
                        listCourse.Add(itemcourse);
                    }
                    else
                    {
                        Session["checkCart"] = "-1";
                        return RedirectToAction("Index", "Course");
                    }
                }
                else
                {
                    CartVM itemcourse = new CartVM(idCourse, 0);
                    listCourse.Add(itemcourse);
                }
            }
            else
            {
                Session["checkCart"] = "-1";
                return RedirectToAction("Index", "Course");
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            if (Session["ViewCart"] == null)
            {
                ViewBag.NoCart = "Không có sản phẩm trong giỏ hàng";
            }
            else
            {
                List<CartVM> listCourse = GetViewCart();
                ViewBag.Totalmoney = totalMoney();
                ViewBag.TotalQuantity = totalQuantity();
                return View(listCourse);
            }
            return View();
        }
        public ActionResult DeleteCart(string idCourse)
        {
            List<CART_OF_USER> listCart = GetCart();
            List<CartVM> listCourse = GetViewCart();

            if (Request.Cookies["User"] != null)
            {
                int id = Int32.Parse(Request.Cookies["User"]["ID"]);
                CART_OF_USER newItem = db.CART_OF_USER.Where(x => x.ID_COURSE == idCourse).Where(x => x.ID_USER == id).FirstOrDefault();
                db.CART_OF_USER.Remove(newItem);
                db.SaveChanges();
                listCourse.RemoveAll(item => item.ID_COURSE == idCourse);
            }
            else
            {
                if (listCourse.Single(item => item.ID_COURSE == idCourse) != null)
                {
                    listCourse.RemoveAll(item => item.ID_COURSE == idCourse);
                }
            }

            return RedirectToAction("Cart");
        }

        private int totalQuantity()
        {
            int total = 0;
            List<CartVM> listCourse = GetViewCart();
            if (listCourse != null)
            {
                total = listCourse.Count();
            }
            return total;
        }

        private double totalMoney()
        {
            double total = 0;
            List<CartVM> listCourse = GetViewCart();
            if (listCourse != null)
            {
                total = listCourse.Sum(item => item.PRICE_COURSE);
            }
            return total;
        }
    }
}