using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServicesAbstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper _mapper , IBasketRepository _basketRepository 
        , UserManager<User> _userManager, IConfiguration _configuration, IOptions<JwtOptions> _options,RoleManager<IdentityRole> _roleManager) : IServiceManager
    {
        private readonly Lazy<ProductService> _LazyProductService = new(() 
                                => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<BasketService> _LazyBasketService = new(() 
                                => new BasketService(_basketRepository,_mapper));
        public IBasketService BasketService => _LazyBasketService.Value;

        private readonly Lazy<AuthenticationService> _LazyAuthenticationService = new(() 
                                => new AuthenticationService(_userManager,_roleManager,_options,_mapper));
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;

        private readonly Lazy<OrderService> _LazyOrderService = new(()
                               => new OrderService(_unitOfWork,_basketRepository,_mapper));
        public IOrderService OrderService => _LazyOrderService.Value;

        private readonly Lazy<PaymentService> _LazyPaymentService = new(()
                               => new PaymentService(_basketRepository,_unitOfWork,_configuration,_mapper));
        public IPaymentService PaymentService => _LazyPaymentService.Value;
    }
}
