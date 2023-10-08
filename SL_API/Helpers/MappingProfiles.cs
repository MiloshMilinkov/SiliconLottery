using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Order;
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
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.Order.Address>();
            CreateMap<Order, OrderToReturnDro>()
                .ForMember(d=>d.Delivery,o=>o.MapFrom(s=>s.Delivery.ShortName))
                .ForMember(d=>d.ShippingPrice,o=>o.MapFrom(s=>s.Delivery.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d=>d.ProductId,o=>o.MapFrom(s=>s.ItemOrdered.ProductItemId))
                .ForMember(d=>d.ProductName,o=>o.MapFrom(s=>s.ItemOrdered.ProductName))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=>s.ItemOrdered.PictureUrl))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<OrderItemUrlResolver>());;
        }
        

    }
}