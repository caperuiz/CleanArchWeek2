<<<<<<< HEAD
﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogService.Persistence.Entities;
=======
﻿using CatalogService.Domain.Entities;
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb

namespace CatalogService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
