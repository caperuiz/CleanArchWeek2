using AutoMapper;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Dtos;
using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<Item>>> GetAllItemsAsync(int categoryId, int page, int pageSize)
        {
            var items = await _itemService.GetAllItemsAsync(categoryId, page, pageSize);
            return Ok(items);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Item>> GetItemByIdAsync(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Item>> AddItemAsync(CreateItemInputDto item)
        {
            var mappedItem = _mapper.Map<Item>(item);
            var addedItem = await _itemService.AddItemAsync(mappedItem);
            return CreatedAtAction(nameof(AddItemAsync), new { id = addedItem.Id }, addedItem);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItemAsync(Item item)
        {
            var updatedItem = await _itemService.UpdateItemAsync(item);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItemAsync(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        private Item MapCreateItemInputDtoToItem(CreateItemInputDto inputDto)
        {
            return new Item
            {
                Name = inputDto.Name,
                Description = inputDto.Description,
                ImageUrl = inputDto.ImageUrl,
                CategoryId = inputDto.CategoryId,
                Price = inputDto.Price,
                Amount = inputDto.Amount
            };
        }

    }
}
