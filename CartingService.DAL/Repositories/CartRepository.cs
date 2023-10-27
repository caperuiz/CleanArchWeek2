using CartingService.DAL.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly LiteDBContext _context;

        public CartRepository(LiteDBContext context)
        {
            _context = context;
        }

        public CartDBModel GetById(string id)
        {
           return _context.Carts.FindOne(c => c.Id == id);
        }

        public void Update(CartDBModel cart)
        {
            _context.Carts.Update(cart);
        }

        public void Insert(CartDBModel cart)
        {
            _context.Carts.Insert(cart);
        }

        public void Delete(string id)
        {
            _context.Carts.Delete(id);
        }

        public IEnumerable<CartDBModel> GetAll()
        {
           return _context.Carts.FindAll();
        }
    }
}
