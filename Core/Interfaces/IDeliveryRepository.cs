using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;

namespace Core.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDeliveryByIdAsync(int deliveryId);
    }
}