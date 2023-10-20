using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Common.Dtos
{
    public interface IItemDto
    {
        int Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        string ImageUrl { get; set; }

        int CategoryId { get; set; }

        decimal Price { get; set; }

        int Amount { get; set; }
    }
}
