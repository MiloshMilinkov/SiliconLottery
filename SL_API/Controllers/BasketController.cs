using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SL_API.Dtos;

namespace SL_API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository
            , IMapper mapper)
        {
            _basketRepository=basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket= await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(customerBasketDto);
            var updateBasket = await _basketRepository.UpdateOrCreateBasketAsync(customerBasket);
            return Ok(updateBasket);
        }

        [HttpDelete]
        public async Task Deletebasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}