<<<<<<< HEAD
﻿using CatalogService.Common.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Domain.Dtos
=======
﻿namespace CatalogService.Domain.Dtos
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb
{
    public class CreateItemInputDto: IItemDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
        public int Id { get; set; }
    }
}
