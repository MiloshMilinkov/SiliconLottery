using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateOrCreateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string basketId);

    
    }
}