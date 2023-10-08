using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Order;
using SL_API.Dtos;

namespace SL_API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver< OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;
        public OrderItemUrlResolver( IConfiguration config){
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto Destination, string destMember, ResolutionContext context){
             if(!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl)){
                return _config["ApiUrl"]+source.ItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}