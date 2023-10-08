using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Task<IReadOnlyList<Delivery>> GetDeliveriesAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    }
}