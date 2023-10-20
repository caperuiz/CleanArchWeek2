namespace CatalogService.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using Moq;
    using CatalogService.Application.Services;
    using CatalogService.Persistence.Entities;

    public class CategoryServiceTests
    {
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsCategories()
        {
            // Arrange
            var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
        };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ReturnsCategory()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Category 1" };

            var mockCategoryRepository = new Mock<ICategoryRepository>();
            mockCategoryRepository.Setup(repo => repo.GetByIdAsync(categoryId)).ReturnsAsync(category);

            var categoryService = new CategoryService(mockCategoryRepository.Object);

            // Act
            var result = await categoryService.GetCategoryByIdAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
        }
    }

}
