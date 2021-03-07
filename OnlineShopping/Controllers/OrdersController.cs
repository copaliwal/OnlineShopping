using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Primary.Interfaces;
using OnlineShopping.ModelView;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        #region Constructor

        private readonly IOrdersRepository ordersRepository;
        private readonly ICartRepository cartRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICartRepository cartRepository)
        {
            this.ordersRepository = ordersRepository;
            this.cartRepository = cartRepository;
        }

        private string LoggedInUserId
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        #endregion

        // GET: Orders
        public ActionResult Index()
        {
            var userId = LoggedInUserId;

            var orders = ordersRepository.GetByUserId(userId);

            return View(orders);
        }

        
        // GET: Orders/Create
        public ActionResult Create()
        {
            var userId = LoggedInUserId;
            var cartItems = cartRepository.GetByUserId(userId);

            CreateOrderViewModel newOrder = new CreateOrderViewModel();
            newOrder.CartItems = cartItems;
            return View(newOrder);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel newOrder)
        {
            var userId = LoggedInUserId;

            var shippingAddress = newOrder.FullName + ", "
                                + newOrder.Address + ", "
                                + newOrder.City + ", "
                                + newOrder.State + ", "
                                + newOrder.Country + ", "
                                + newOrder.PostalCode;

            ordersRepository.CreateOrder(userId, shippingAddress);

            return RedirectToAction("Index");
        }
    }
}