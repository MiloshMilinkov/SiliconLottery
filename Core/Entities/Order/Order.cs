using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order(){}
        public Order(string buyerEmail, Address shipToAddress, decimal subtotal,
                     Delivery delivery, IReadOnlyList<OrderItem> orderItems) 
        {
            this.BuyerEmail = buyerEmail;
            this.ShipToAddress = shipToAddress;
            this.Subtotal = subtotal;
            this.Delivery = delivery;
            this.OrderItems = orderItems;
        }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public Delivery Delivery { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set;}
        public decimal Subtotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal  GetTotal(){
            return Subtotal + Delivery.Price;
        }
    }
}