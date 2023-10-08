using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private ProductRepository _productRepository;
        private BasketRepository _basketRepository;
        
        private DeliveryRepository _deliveryRepository;
        
        private OrderRepository _orderRepository;

        public UnitOfWork(StoreContext storeContext, IConnectionMultiplexer redis)
        {
            _storeContext = storeContext;
            _basketRepository = new BasketRepository(redis);
        }

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_storeContext);
        public IBasketRepository BasketRepository => _basketRepository;
        public IDeliveryRepository DeliveryRepository => _deliveryRepository ??= new DeliveryRepository(_storeContext);
           public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_storeContext);

        public async Task<int> CompleteAsync()
        {
            return await _storeContext.SaveChangesAsync();
        }

         public void Dispose()
        {
            _storeContext.Dispose();
        }
    }

   
}