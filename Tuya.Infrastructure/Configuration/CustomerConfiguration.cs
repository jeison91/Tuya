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
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Address).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CellPhone).HasMaxLength(10).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
        }
    }
}
