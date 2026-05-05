using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, S => S.WithOwner());

            builder.Property(O => O.Subtotal).HasColumnType("decimal(18,3)");

            builder.HasMany(O => O.OrderItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(O => O.OrderStatus)
                .HasConversion(S => S.ToString(), S => (OrderStatus)Enum.Parse<OrderStatus>(S));

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
