using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _storeContext;

        public OrderRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public void Add(Order order)
        {
            _storeContext.Orders.Add(order);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _storeContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.ItemOrdered)
                .Include(o => o.Delivery)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _storeContext.Orders
                .Where(o => o.BuyerEmail == buyerEmail)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.ItemOrdered)
                .Include(o => o.Delivery)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Delivery>> GetDeliveriesAsync()
        {
            return await _storeContext.Delivery.ToListAsync();
        }
    }

    
}