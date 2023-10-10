﻿// CatalogService.API/Controllers/ItemController.cs

using Microsoft.AspNetCore.Mvc;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System;
using CatalogService.Domain;

namespace CatalogService.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _itemService.GetAllItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> AddItem(CreateItemInputDto item)
        {
            var addedItem = await _itemService.AddItemAsync(MapCreateItemInputDtoToItem(item));
            return CreatedAtAction(nameof(GetItemById), new { id = addedItem.Id }, addedItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _itemService.UpdateItemAsync(item);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
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
