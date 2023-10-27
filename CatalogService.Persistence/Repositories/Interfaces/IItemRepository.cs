<<<<<<< HEAD
﻿using CatalogService.Common.Dtos;
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
            Task<Item> AddItemAsync(IItemDto item);
            Task<Item> UpdateAsync(Item item);
            Task<bool> DeleteAsync(int id);
        }
    
=======
﻿using CatalogService.Domain.Entities;

namespace CatalogService.Persistence.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItemsAsync(int categoryId, int page, int pageSize);
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }

>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
}
