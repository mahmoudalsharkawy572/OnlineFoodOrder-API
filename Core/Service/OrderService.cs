using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Services.Specifications;
using ServicesAbstraction;
using Shared.OrderModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IUnitOfWork _unitOfWork
                             ,IBasketRepository _basketRepository
                             ,IMapper _mapper) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // 1.Address 
            var AddressToShip = _mapper.Map<ShippingAddress>(orderRequest.ShipppingAddress);

            // 2.OrderItems
            var Basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId);
            if(Basket == null)  
                throw new BasketNotFoundException(Basket.Id);
            var orderItems = new List<OrderItem>();
            foreach(var item in Basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>()
                                .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }

            // 3.DeliveryMethod
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)  ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            // 4.Subtotal
            var subTotal = 0m;
            foreach(var item in orderItems)
                subTotal += item.Quantity * item.Price;

            // 5.Create Order
            var order = new Order(userEmail, AddressToShip, deliveryMethod, orderItems, subTotal,Basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResult>(order);

        }

        private static OrderItem CreateOrderItem(BasketItem item, Product product)
        {
           return new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl), item.Quantity, product.Price);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync()
        {
            var Repo = _unitOfWork.GetRepository<DeliveryMethod,int>();
            var deliveryMethods = await Repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid Id)
        {
            var Repo = _unitOfWork.GetRepository<Order,Guid>();
            var order = await Repo.GetByIdAsync( new OrderSpecification(Id));
            if (order == null)
                throw new OrderNotFoundException(Id); 
            else
                return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            var Repo = _unitOfWork.GetRepository<Order, Guid>();
            var orders = await Repo.GetAllAsync(new OrderSpecification(email));
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
