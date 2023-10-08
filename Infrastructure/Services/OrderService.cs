using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Entities.Order;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        // public IBasketRepository _basketRepository;
        // public IProductRepository _productRepository;
        // public IDeliveryRepository _deliveryRepository;
        // public OrderService( IBasketRepository basketRepository, IProductRepository productRepository
        //                    , IDeliveryRepository deliveryRepository){
        //     _basketRepository = basketRepository;
        //     _productRepository = productRepository;
        //     _deliveryRepository = deliveryRepository;

        // }
        // public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
        // {
        //     var basket = await _basketRepository.GetBasketAsync(basketId);

        //     var items = new List<OrderItem>();
        //     foreach(var item in basket.Items){
        //         var productItem = await _productRepository.GetProductByIdAsync(item.Id);
        //         var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
        //         var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
        //         items.Add(orderItem);
        //     }

        //     var delivery = await _deliveryRepository.GetDeliveryByIdAsync(deliveryMethod);

        //     var subTotal = items.Sum(item => item.Price * item.Quantity);

        //     var order = new Order( buyerEmail, shippingAddress, subTotal, delivery, items);

        //     //PRBO PROBATI ALTERNATIVU PA ONDA SACUVATI U BAZU ORDER
        //     return order;

        // }
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
        {
            var basket = await _unitOfWork.BasketRepository.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach(var item in basket.Items){
                var productItem = await _unitOfWork.ProductRepository.GetProductByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            var delivery = await _unitOfWork.DeliveryRepository.GetDeliveryByIdAsync(deliveryMethod);

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order( buyerEmail, shippingAddress, subTotal, delivery, items);

            //PRVO PROBATI ALTERNATIVU PA ONDA SACUVATI U BAZU ORDER
            _unitOfWork.OrderRepository.Add(order);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.Dispose();
            return order;

        }

        

        public async Task<IReadOnlyList<Delivery>> GetDeliveriesAsync()
        {
            return await _unitOfWork.OrderRepository.GetDeliveriesAsync();
        }

        public async Task<Order> GetOrderByid(int id, string buyerEmail)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);
            if (order != null && order.BuyerEmail == buyerEmail)
            {
                return order;
            }
            return null; // or throw an exception if the order doesn't belong to the buyer or doesn't exist
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            return await _unitOfWork.OrderRepository.GetOrdersForUserAsync(buyerEmail);
        }
    }
}