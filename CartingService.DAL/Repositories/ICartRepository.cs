using CartingService.DAL.Models;
using System.Collections.Generic;

namespace CartingService.DAL.Repositories
{
    public interface ICartRepository
    {

        public CartDBModel GetById(string id);

        public void Update(CartDBModel cart);

        public void Insert(CartDBModel cart);

        public void Delete(string id);

        public IEnumerable<CartDBModel> GetAll();
    }
}