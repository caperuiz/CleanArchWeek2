using CartingService.BLL.CartingService.BLL;
using CartingService.BLL.Models;
using CartingService.DAL.Models;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost(nameof(GetCart))]
        public Cart GetCart(string id)
        {
            var res = _cartService.GetCartById(id);
            return res;
        }

        [HttpPost(nameof(GetAllCarts))]
        public IEnumerable<Cart> GetAllCarts()
        {
            var res= _cartService.GetAllCarts().ToList();
            return res.ToArray();
        }


        [HttpPost]
        public void SaveOrUpdateCart(string cartId, CartItem item)
        {
            _cartService.AddItemToCart(cartId, item);
        }

        [HttpDelete]
        public void RemoveCart(string cartId, int itemId)
        {
            _cartService.RemoveItemFromCart(cartId, itemId);
        }
    }
}
