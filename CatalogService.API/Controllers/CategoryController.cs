// CatalogService.API/Controllers/CategoryController.cs

using Microsoft.AspNetCore.Mvc;
using CatalogService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Domain.Dtos;
using CatalogService.Persistence.Entities;

namespace CatalogService.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("get")]
        public async Task<ActionResult<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Category>> AddCategoryAsync(CreateCategoryInputDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            var addedCategory = await _categoryService.AddCategoryAsync(mappedCategory);
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(Category category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(id);
        }
    }
}
