using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Primary.Interfaces;
using System.Security.Claims;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class CartController : Controller
    {
        #region Construstor 

        private readonly ICartRepository cartReposity;
        public CartController(ICartRepository cartReposity)
        {
            this.cartReposity = cartReposity;
        }

        private string LoggedInUserId
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        #endregion

        // GET: Cart
        public ActionResult Index()
        {
            var userId = LoggedInUserId;
            var cartItems = cartReposity.GetByUserId(userId);
            return View(cartItems);
        }

        // POST: Cart/Add
        [HttpPost]
        public ActionResult Add(Product product)
        {
            Cart cartItem = new Cart()
            {
                ProductId = product.ProductId,
                Quantity = 1,
                TotalPrice = product.Price,
                UserId = LoggedInUserId
            };

            cartReposity.Add(cartItem);

            return RedirectToAction("Index");
        }

        // Get: Cart/Remove
        public ActionResult Remove(int cartId)
        {
            var cartItem = cartReposity.GetById(cartId);
            cartReposity.Delete(cartItem);    

            return RedirectToAction("Index");
        }
    }
}