using CatalogService.Application.Interfaces;
using CatalogService.Common.Dtos;
using CatalogService.Domain.Dtos;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _itemRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddItemAsync(IItemDto itemDto)
        {
            var validationResult = _validator.Validate(itemDto);

            if (!validationResult.IsValid)
            {
                return await _itemRepository.AddItemAsync(itemDto);
            }
            return false;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            return await _itemRepository.UpdateAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _itemRepository.DeleteAsync(id);
        }
    }
}
