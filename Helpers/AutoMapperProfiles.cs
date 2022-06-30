using AutoMapper;
using Market.Consumer.Dtos;
using Market.Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Consumer.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Product, ProductListViewDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src. category.categoryName));
        }
    }
}
