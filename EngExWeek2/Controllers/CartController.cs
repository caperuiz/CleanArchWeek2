using CartingService.BLL.CartingService.BLL;
using CartingService.BLL.Models;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EngExWeek2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartService _cartService;

        public CartController(ILogger<CartController> logger, ICartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        [HttpGet("GetCartInfo/{id}")]
        public IActionResult GetCart(Guid id)
        {
            var cart = _cartService.GetCart(id);
            return Ok(cart);
        }

        [HttpGet("GetAllCarts")]
        public IActionResult GetAllCarts()
        {
            var carts = _cartService.GetAllCarts().ToList();
            return Ok(carts.ToArray());
        }


        [HttpPost("AddItemToCart")]
        public IActionResult AddItemToCart(Guid cartId, AddCartItem item)
        {
            _cartService.AddItemToCart(cartId, item);
            return new OkResult();
        }

        [HttpDelete("RemoveItemFromCart")]
        public IActionResult RemoveItemFromCart(Guid cartId, Guid itemId)
        {
            _cartService.RemoveItemFromCart(cartId, itemId);
            return new OkResult();
        }
    }
}
