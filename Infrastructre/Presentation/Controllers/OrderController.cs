using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            var services = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrder(OrderRequest orderRequest)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orderResult = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, userEmail);
            return Ok(orderResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetOrderByEmail()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.GetOrdersByEmailAsync(userEmail);
            return Ok(order);
        }
    }
}
