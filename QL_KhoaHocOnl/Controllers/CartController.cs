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
            
            CART_OF_USER course = listCart.Where(item=>item.ID_COURSE==idCourse).FirstOrDefault();
            if (course == null)
            {
                if (Request.Cookies["User"] != null)
                {
                    int id = Int32.Parse(Request.Cookies["User"]["ID"]);
                    CART_OF_USER newItem = new CART_OF_USER(idCourse, id);
                    newItem.ID_USER = id;
                    newItem.ID_COURSE = idCourse;
                    db.CART_OF_USER.Add(newItem);
                    db.SaveChanges();
                    listCart.Add(course);

                    CartVM itemcourse = new CartVM(idCourse, id);
                    listCourse.Add(itemcourse);
                }
                else
                {
                  
                    CartVM itemcourse = new CartVM(idCourse,0);
                    listCourse.Add(itemcourse);
                }
            }
            else
            {
                Session["checkCart"] = "-1";
                return RedirectToAction("Index","Course");
            }    
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            List<CartVM> listCourse = GetViewCart();

            return View(listCourse);
        }
        public ActionResult DeleteCart(string idCourse)
        {
            List<CART_OF_USER> listCart = GetCart();
            List<CartVM> listCourse = GetViewCart();

            if (Request.Cookies["User"] != null)
            {
                int id = Int32.Parse(Request.Cookies["User"]["ID"]);
                CART_OF_USER newItem = db.CART_OF_USER.Where(x => x.ID_COURSE == idCourse).FirstOrDefault();
                db.CART_OF_USER.Remove(newItem);
                db.SaveChanges();
            }
            listCourse.RemoveAll(item => item.ID_COURSE == idCourse);

            return RedirectToAction("Cart");
        }

    }
}