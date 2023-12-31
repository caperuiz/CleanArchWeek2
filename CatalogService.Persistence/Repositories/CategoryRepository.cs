﻿// CatalogService.Persistence/Repositories/CategoryRepository.cs

using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
=======
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb

namespace CatalogService.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoryRepository( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(Category.AllContact);
                return result.ToList();
            }
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
