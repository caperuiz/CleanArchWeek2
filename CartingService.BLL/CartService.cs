using CartingService.BLL.Models;
using CartingService.DAL.Models;
using CartingService.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL
{

    namespace CartingService.BLL
    {

        public class CartService : ICartService
        {
            private readonly ICartRepository _cartRepository;

            public CartService(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            public Cart GetCartById(string cartId)
            {
                var cartDbModel = _cartRepository.GetById(cartId);

                if (cartDbModel == null)
                {
                    return null;
                }

                var cart = MapToCart(cartDbModel);
                return cart;
            }




            public void AddItemToCart(string cartId, CartItem item)
            {
                var cartDbModel = _cartRepository.GetById(cartId);

                if (cartDbModel == null)
                {
                    cartDbModel = new CartDBModel
                    {
                        Id = cartId
                    };
                }

                var existingItem = cartDbModel.Items.Find(i => i.Id == item.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                    _cartRepository.Update(cartDbModel);
                }
                else
                {

                    _cartRepository.Insert(
                        
                        new CartDBModel() { Id = cartId, Items = new List<CartItemDBModel>() { MapToCartItemDBModel(item) } });
                }

                
            }

            public void RemoveItemFromCart(string cartId, int itemId)
            {
                var cartDbModel = _cartRepository.GetById(cartId);

                if (cartDbModel == null)
                {
                    return;
                }

                cartDbModel.Items.RemoveAll(i => i.Id == itemId);
                _cartRepository.Update(cartDbModel);
            }

            private Cart MapToCart(CartDBModel cartDbModel)
            {
                var cart = new Cart
                {
                    Id = cartDbModel.Id,
                    Items = new List<CartItem>()
                };

                foreach (var item in cartDbModel.Items)
                {
                    cart.Items.Add(MapToCartItem(item));
                }

                return cart;
            }

            private CartItem MapToCartItem(CartItemDBModel itemDbModel)
            {
                return new CartItem
                {
                    Id = itemDbModel.Id,
                    Name = itemDbModel.Name,
                    Image = itemDbModel.Image,
                    Price = itemDbModel.Price,
                    Quantity = itemDbModel.Quantity
                };
            }

            private CartItemDBModel MapToCartItemDBModel(CartItem item)
            {
                return new CartItemDBModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
            }

            public IEnumerable<Cart> GetAllCarts()
            {
                IList<Cart> cart = new List<Cart>();
                var collection = _cartRepository.GetAll();
                foreach (var item in collection)
                {
                    cart.Add(MapToCart(item));
                }
                return cart;
            }
        }

        public interface ICartService
        {
            public Cart GetCartById(string cartId);
            public void AddItemToCart(string cartId, CartItem item);
            public IEnumerable<Cart> GetAllCarts();
            public void RemoveItemFromCart(string cartId, int itemId);

        }
    }
}
