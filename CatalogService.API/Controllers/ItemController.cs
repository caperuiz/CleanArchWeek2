using Microsoft.AspNetCore.Mvc;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using CatalogService.Domain.Dtos;

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

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItemsAsync()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemByIdAsync(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItemAsync(CreateItemInputDto item)
        {
            var mappedItem = _mapper.Map<Item>(item);
            var addedItem = await _itemService.AddItemAsync(mappedItem);
            return CreatedAtAction(nameof(AddItemAsync), new { id = addedItem.Id }, addedItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(Item item)
        {
            var updatedItem = await _itemService.UpdateItemAsync(item);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
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
