using CartingService.Messaging;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CatalogService.Application.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IValidator<Item> _validator;
        private readonly IRabbitMqService _rabbitMqService;

        public ItemService(IItemRepository itemRepository, IValidator<Item> validator, IRabbitMqService rabbitMqService)
        {
            _itemRepository = itemRepository;
            _validator = validator;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<List<Item>> GetAllItemsAsync(int categoryId, int page, int pageSize)
        {
            return await _itemRepository.GetAllItemsAsync(categoryId, page, pageSize);
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _itemRepository.GetItemByIdAsync(id);
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                return await _itemRepository.AddItemAsync(item);
            }
            return new Item();
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            _rabbitMqService.PublishMessage(JsonSerializer.Serialize(item));
            return await _itemRepository.UpdateItemAsync(item);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await _itemRepository.DeleteItemAsync(id);
        }
    }
}
