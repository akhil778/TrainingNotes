using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    [HandleError]
    [Authorize]
    public class CheckoutController : Controller
    {
        BookStoreDB db = new BookStoreDB();
        const string PromoCode = "FREE";

        //
        // GET: /Checkout/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }


        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            var orderDetail = from order in db.OrderDetails
                              where order.OrderId == id
                              select order;

            if (isValid)
            {
                ViewBag.OrderId = id;
                return View(orderDetail);
                
            }
            else
            {
                return View("Error");
            }
        }
	}
}