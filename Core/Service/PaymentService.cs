using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDTos;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModule.Product;

namespace Services
{
    public class PaymentService(IBasketRepository _basketRepository
                              , IUnitOfWork _unitOfWork
                              , IConfiguration _configuration
                              , IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTo> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetRequiredSection("Stripe")["SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>()
                        .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }


            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No delivery Method was Selected");

            var method = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                       .GetByIdAsync(basket.DeliveryMethodId.Value)
                       ?? throw new ProductNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = method.Price;

            var service = new PaymentIntentService();

            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;


            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
               
                var paymentIntent = await service.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update 
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);

            }

            await _basketRepository.CreateOrUpdateBasketAsync(basket);

            return _mapper.Map<BasketDTo>(basket);

        }

        public async Task UpdateOrderPaymentStatus(string request, string header)
        {
            var endPointSecret = _configuration.GetRequiredSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, header, endPointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            switch (stripeEvent.Type)
            {
                case "payment_intent.payment_failed":
                    await UpdatePaymentFailed(paymentIntent!.Id);
                    break;

                case "payment_intent.succeeded":
                    await UpdatePaymentReceived(paymentIntent!.Id);
                    break;

                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentFailed(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderSpecification(paymentIntentId))
                ?? throw new Exception();

            order.OrderStatus = OrderStatus.PaymentFailed;
            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentReceived(string paymentIntentId)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderSpecification(paymentIntentId))
                ?? throw new Exception();

            order.OrderStatus = OrderStatus.PaymentReceived;

            _unitOfWork.GetRepository<Order, Guid>().Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
