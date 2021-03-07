using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Primary.Interfaces;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CartServiceController : ControllerBase
    {
        #region Constructor 

        public readonly ICartRepository cartRepository;
        public CartServiceController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        #endregion

        // GET: api/CartService/GetCount/5
        [HttpGet("{userId}")]
        [Route("GetCount")]
        public int GetCount(string userId)
        {
            int count = 0;
            if (userId != null || userId != "")
            {
                count = cartRepository.GetCount(userId);
            }

            return count;
        }

    }
}