﻿using AutoMapper;
using CannabisPlantations.WebApi.Models;
using CannabisPlantations.WebApi.Models.Dtos;

namespace CannabisPlantations.WebApi.Utility
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
        }
    }
}