// CatalogService.Domain/Entities/Item.cs

using System;

namespace CatalogService.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public Category Category { get; set; }
        // Add navigation properties or other properties as needed
    }
}
