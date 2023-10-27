// CatalogService.API/Controllers/CategoryController.cs

<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using CatalogService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
=======
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
using AutoMapper;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Dtos;
<<<<<<< HEAD
using CatalogService.Persistence.Entities;
=======
using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb

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
        public async Task<ActionResult<List<Category>>> GetAllCategoriesAsync()
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
