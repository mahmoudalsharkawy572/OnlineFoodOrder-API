using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        // Get by email
        public Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email);
        
        // Create order
        public Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest , string userEmail);

        // Get order by id
        public Task<OrderResult> GetOrderByIdAsync(Guid Id);

        // Get all delivery methods
        public Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync();
    }
}
