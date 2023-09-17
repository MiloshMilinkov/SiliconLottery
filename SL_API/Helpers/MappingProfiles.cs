using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using SL_API.Dtos;

namespace SL_API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles(){
            CreateMap<Product,ProductDto>()
                .ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
        

    }
}