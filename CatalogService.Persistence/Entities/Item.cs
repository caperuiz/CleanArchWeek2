// CatalogService.Domain/Entities/Item.cs

using CatalogService.Common.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Persistence
{
    public partial class Item
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}


