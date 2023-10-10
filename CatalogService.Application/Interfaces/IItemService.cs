// CatalogService.Application/Interfaces/IItemService.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
