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
            try
            {
                var cart = _cartService.GetCart(id);
                return Ok(cart);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCarts")]
        public IActionResult GetAllCarts()
        {
            try
            {
                var carts = _cartService.GetAllCarts().ToList();
                return Ok(carts.ToArray());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddItemToCart")]
        public IActionResult AddItemToCart(Guid cartId, AddCartItem item)
        {
            try
            {
                _cartService.AddItemToCart(cartId, item);
                return new OkResult();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveItemFromCart")]
        public IActionResult RemoveItemFromCart(Guid cartId, Guid itemId)
        {
            try
            {
                _cartService.RemoveItemFromCart(cartId, itemId);
                return new OkResult();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
