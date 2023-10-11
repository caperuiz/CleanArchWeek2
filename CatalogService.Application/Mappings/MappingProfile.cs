﻿using AutoMapper;
using CatalogService.Domain.Dtos;
using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Mappings
{
    internal class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateItemInputDto, Item>();
            CreateMap<Item, CreateItemInputDto>();
            CreateMap<CreateCategoryInputDto, Category>();
            CreateMap<Category, CreateCategoryInputDto>();
        }
    }
}