using Microsoft.AspNet.Identity;
using ReturnGym.Models;
using ReturnGym.Models.OnlineShopping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReturnGym.Controllers.OnlineShopping
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Products");
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<CartItem> cartItems = (List<CartItem>)Session["cart"];
                foreach (var item in cartItems)
                {
                    if (item.Product.ProductID == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cartItems.Remove(item);
                            cartItems.Add(new CartItem(db.Products.Find(productId), prevQty - 1));
                        }
                        break;
                    }
                }
                Session["cart"] = cartItems;
            }
            return Redirect("CheckoutDetails");
        }

        public ActionResult AddToCart(int productId, string url)
        {
            if (Session["cart"] == null)
            {
                List<CartItem> cartItems = new List<CartItem>
                {
                    new CartItem(db.Products.Find(productId),1)
                };
                Session["cart"] = cartItems;
            }
            else
            {
                List<CartItem> cartItems = (List<CartItem>)Session["cart"];
                int check = isExistingCheck(productId);

                if (check == -1)
                {
                    cartItems.Add(new CartItem(db.Products.Find(productId), 1));
                }
                else
                {
                    cartItems[check].Quantity++;
                }
                Session["cart"] = cartItems;
            }
            db.SaveChanges();
            return RedirectToAction(url);
        }
        private int isExistingCheck(int? productId)
        {

            List<CartItem> cartItems = (List<CartItem>)Session["cart"];
            for (int i = 0; i < cartItems.Count; i++)
            {
                if (cartItems[i].Product.ProductID == productId) return i;


            }
            return -1;
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cart"];
            foreach (var item in cartItems)
            {
                if (item.Product.ProductID == productId)
                {
                    cartItems.Remove(item);
                    break;
                }
            }
            Session["cart"] = cartItems;
            return RedirectToAction("CheckoutDetails");
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cart"];
            if (Session["cart"] != null)
            {
                ViewData["CartCount"] = cartItems.Sum(q=>q.Quantity);

            }

            return PartialView("CartSummary");
        }

        //Process Orders
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cart"];
            string CurrentUserName = User.Identity.GetUserName();
            ApplicationUser member = db.Users.Where(s => s.UserName == CurrentUserName).FirstOrDefault();
            var order = new ReturnGym.Models.OnlineShopping.Order()
            {
                CustomerName = member.UserName,
                CustomerPhone = member.PhoneNumber,
                CustomerEmail = member.Email,
                CustomerAddress = member.Address,
                Refcode = "abc123",
                OrderDate = DateTime.Now,
                PaymentType = "Cash",
                Status = "Processing"
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (CartItem cart in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderID = order.OrderID,
                    ProductID = cart.Product.ProductID,
                    Quantity = cart.Quantity,
                    Price = cart.Product.Price
                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            Session.Remove("cart");
            return RedirectToAction("Index", "Products");
        }
    }
}