// CatalogService.Application/Interfaces/IItemService.cs

<<<<<<< HEAD
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogService.Common.Dtos;
=======
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync(int categoryId, int page, int pageSize);
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(IItemDto item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
