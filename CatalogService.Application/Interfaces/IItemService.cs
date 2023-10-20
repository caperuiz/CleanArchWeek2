// CatalogService.Application/Interfaces/IItemService.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogService.Common.Dtos;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> AddItemAsync(IItemDto item);
        Task<Item> UpdateItemAsync(Item item);
        Task<bool> DeleteItemAsync(int id);
    }
}
