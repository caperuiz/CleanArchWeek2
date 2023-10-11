// CatalogService.API/Controllers/CategoryController.cs

using Microsoft.AspNetCore.Mvc;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Domain.Dtos;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(CreateCategoryInputDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            var addedCategory = await _categoryService.AddCategoryAsync(mappedCategory);
            return CreatedAtAction(nameof(GetCategoryById), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
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
