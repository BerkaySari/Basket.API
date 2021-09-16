using AutoMapper;
using Data.Models.Mapping.Requests;
using Data.Models.Mapping.Responses;
using Data.Models.Models;
using System.Collections.Generic;

namespace Data.Models.Mapping.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BasketItemRequest, BasketItem>();
            CreateMap<BasketItem, BasketItemResponse>();

            CreateMap<List<BasketItem>, BasketResponse>()
                .ForMember(t => t.Items, opt => opt.MapFrom(src => src));
        }
    }
}
