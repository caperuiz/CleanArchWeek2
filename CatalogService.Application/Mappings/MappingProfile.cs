using AutoMapper;
using CatalogService.Common.Dtos;
using CatalogService.Domain.Dtos;
<<<<<<< HEAD
using CatalogService.Persistence;
using CatalogService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using CatalogService.Domain.Entities;
>>>>>>> a8ba5c3a09ccd2994119f20a1b423610cd3646bb

namespace CatalogService.Application.Mappings
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateItemInputDto, Item>();
            CreateMap<Item, CreateItemInputDto>();
            CreateMap<CreateCategoryInputDto, Category>();
            CreateMap<Category, CreateCategoryInputDto>();
            CreateMap<Item, IItemDto>();
            CreateMap<IItemDto, Item>();
        }
    }
}