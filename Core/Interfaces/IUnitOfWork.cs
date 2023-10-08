using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IBasketRepository BasketRepository { get; }
        
        IOrderRepository OrderRepository { get; }
        
        IDeliveryRepository DeliveryRepository { get; }
        Task<int> CompleteAsync();
    }

   
}