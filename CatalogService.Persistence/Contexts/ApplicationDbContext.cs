// CatalogService.Persistence/Contexts/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using CatalogService.Domain.Entities;
using System.Collections.Generic;

namespace CatalogService.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString) : base(GetOptions(connectionString))
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }

}
