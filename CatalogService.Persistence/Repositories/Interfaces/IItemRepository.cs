using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence.Repositories.Interfaces
{
        public interface IItemRepository
        {
            Task<List<Item>> GetAllAsync();
            Task<Item> GetByIdAsync(int id);
            Task<Item> AddAsync(Item item);
            Task<Item> UpdateAsync(Item item);
            Task<bool> DeleteAsync(int id);
        }
    
}
