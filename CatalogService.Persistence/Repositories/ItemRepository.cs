using CatalogService.Domain.Entities;
using CatalogService.Persistence.Contexts;
using CatalogService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence.Repositories
{
        public class ItemRepository : IItemRepository
        {
            private readonly ApplicationDbContext _context;

            public ItemRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Item>> GetAllAsync()
            {
                return await _context.Items.ToListAsync();
            }

            public async Task<Item> GetByIdAsync(int id)
            {
                return await _context.Items.FindAsync(id);
            }

            public async Task<Item> AddAsync(Item item)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }

            public async Task<Item> UpdateAsync(Item item)
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                    return false;

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
        
    }
}
