using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Infrastructure.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailEntity> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.OrderEntity)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.ProductEntity)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
