// CatalogService.API/Controllers/CategoryController.cs

using AutoMapper;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Dtos;
using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
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

        /// <summary>
        /// Get a list of all categories.
        /// </summary>
        /// <returns>The list of categories.</returns>
        [HttpGet("get")]
        [ProducesResponseType(200, Type = typeof(List<Category>))]
        public async Task<ActionResult<List<Category>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The category information.</returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The category to create.</param>
        /// <returns>The created category.</returns>
        [HttpPost("create")]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<ActionResult<Category>> AddCategoryAsync(CreateCategoryInputDto category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            var addedCategory = await _categoryService.AddCategoryAsync(mappedCategory);
            return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = addedCategory.Id }, addedCategory);
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <param name="category">The category to update.</param>
        /// <returns>The updated category if successful, or NotFound if the category is not found.</returns>
        [HttpPut("update/{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCategoryAsync(Category category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        /// <summary>
        /// Delete a category by ID.
        /// </summary>
        /// <param name="id">The category ID to delete.</param>
        /// <returns>The ID of the deleted category if successful, or NotFound if the category is not found.</returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
