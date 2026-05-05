using AutoMapper;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.Identity;
using DomainLayer.Models.OrderModule;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodResult>()
                .ForMember(dist => dist.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dist => dist.ShortName, options => options.MapFrom(src => src.ShortName))
                .ForMember(dist => dist.Description, options => options.MapFrom(src => src.Description))
                .ForMember(dist => dist.DeliveryTime, options => options.MapFrom(src => src.DeliveryTime))
                .ForMember(dist => dist.Price, options => options.MapFrom(src => src.Price));

            CreateMap<ShippingAddress, AddressDTO>().ReverseMap();

            CreateMap<Order, OrderResult>()
                .ForMember(dist => dist.PaymentStatus, options => options.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dist => dist.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dist => dist.Total, options => options.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dist => dist.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dist => dist.ProductId, options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom(src => src.Product.PictureUrl));

            CreateMap<AddressDTO, Address>().ReverseMap();
              

        }
    }
}
