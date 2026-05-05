using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        private Order()
        {
            
        }
        public Order(string userEmail
            , ShippingAddress shippingAddress
            , DeliveryMethod deliveryMethod
            , ICollection<OrderItem> orderItems
            , decimal subtotal
            , string paymentIntentId)
            
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } //
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; 
        public ShippingAddress ShippingAddress { get; set; } 
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending; 
        public DeliveryMethod DeliveryMethod { get; set; } 
        public int? DeliveryMethodId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; } 
    }
}
