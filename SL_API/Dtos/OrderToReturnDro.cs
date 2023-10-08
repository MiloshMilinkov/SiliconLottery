using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Order;

namespace SL_API.Dtos
{
    public class OrderToReturnDro
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string Delivery { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set;}
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}