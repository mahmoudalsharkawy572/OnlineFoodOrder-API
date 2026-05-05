using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BasketProfile>();
                cfg.AddProfile<ProductProfile>();
                cfg.AddProfile<OrderProfile>();
            });
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddTransient<PictureUrlResolver>();

            Services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return Services;
        }
    }
}
