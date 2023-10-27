using CatalogService.Application.Interfaces;
<<<<<<< HEAD
using CatalogService.Common.Dtos;
using CatalogService.Domain.Dtos;
=======
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace CatalogService.Application.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IValidator<Item> _validator;

        public ItemService(IItemRepository itemRepository, IValidator<Item> validator)
        {
            _itemRepository = itemRepository;
            _validator = validator;
        }

        public async Task<List<Item>> GetAllItemsAsync(int categoryId, int page, int pageSize)
        {
            return await _itemRepository.GetAllItemsAsync(categoryId, page, pageSize);
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task<bool> AddItemAsync(IItemDto itemDto)
        {
            var validationResult = _validator.Validate(itemDto);

            if (!validationResult.IsValid)
            {
<<<<<<< HEAD
                return await _itemRepository.AddItemAsync(itemDto);
=======
                return await _itemRepository.AddItemAsync(item);
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
            }
            return false;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            return await _itemRepository.UpdateItemAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }
}
