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
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(100).IsRequired();
            builder.Property(x => x.State).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Total).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(x => x.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(x => x.OrderDetails)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
