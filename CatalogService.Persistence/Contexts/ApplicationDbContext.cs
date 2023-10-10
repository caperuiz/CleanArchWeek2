// CatalogService.Persistence/Contexts/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using CatalogService.Domain.Entities;
using System.Collections.Generic;

namespace CatalogService.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        // Configure database relationships and constraints here
    }
}
