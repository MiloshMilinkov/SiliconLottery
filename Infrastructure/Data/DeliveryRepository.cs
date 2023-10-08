using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly StoreContext _storeContext;
        public DeliveryRepository(StoreContext storeContext){
            _storeContext=storeContext;
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await _storeContext.Delivery.FirstOrDefaultAsync(p=>p.Id == deliveryId);
        }
    }
}