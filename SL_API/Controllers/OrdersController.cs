using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Order;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SL_API.Dtos;
using SL_API.Errors;
using SL_API.Extensions;

namespace SL_API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper )
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto){
            var email = HttpContext.User.RetriveEmailFromClaimpPrincipal();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if(order == null) return BadRequest( new ApiResponse(400, "Problem creating order!"));
            return Ok(order); 
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetriveEmailFromClaimpPrincipal();
            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var email = HttpContext.User.RetriveEmailFromClaimpPrincipal();
            var order = await _orderService.GetOrderByid(id, email);
            if (order == null) return NotFound(new ApiResponse(404));
            return order;
        }

        [HttpGet("deliveries")]
        public async Task<ActionResult<IReadOnlyList<Delivery>>> GetDeliveries()
        {
             var deliveries = await _orderService.GetDeliveriesAsync();
             return Ok(deliveries);
        }
    }
}