<<<<<<< HEAD:CatalogService.Persistence/Entities/Category.cs
﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogService.Persistence.Entities
=======
﻿namespace CatalogService.Domain.Entities
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb:CatalogService.Domain/Entities/Category.cs
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // [Url(ErrorMessage = "Image must be a valid URL.")]
        public string Image { get; set; }
    }
}