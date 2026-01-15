using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;
using Tuya.Domain.Unit;

namespace Tuya.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> option) : DbContext(option), IUnitOfWork
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Agregamos datos a la tablas maestras.
            modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity { Id = 1, Name = "Carlos Andres Perez", Address = "Carrera 1", CellPhone = "300 999 1234", Email = "Carlos@test.com" },
                new CustomerEntity { Id = 2, Name = "Maria Camila Zuluaga", Address = "Carrera 50", CellPhone = "301 987 3216", Email = "Maria@test.com" },
                new CustomerEntity { Id = 3, Name = "Daniela Castro", Address = "Avenida 10", CellPhone = "350 444 5465", Email = "Daniela@test.com" },
                new CustomerEntity { Id = 4, Name = "Juan Camilo Munera", Address = "Calle 44", CellPhone = "301 951 3548", Email = "Juan@test.com" }
            );

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity { Id = 1, Name = "Café", Description = "AAA", UnitPrice = 25000, Active = true },
                new ProductEntity { Id = 2, Name = "Arroz Premium", Description = "BBB", UnitPrice = 6000, Active = true },
                new ProductEntity { Id = 3, Name = "Lentejas", Description = "CCC", UnitPrice = 4000, Active = false },
                new ProductEntity { Id = 4, Name = "Frijoles", Description = "DDD", UnitPrice = 4500, Active = true },
                new ProductEntity { Id = 5, Name = "Arroz", Description = "EEE", UnitPrice = 3500, Active = false },
                new ProductEntity { Id = 6, Name = "Salsa de tomate", Description = "FFF", UnitPrice = 13000, Active = true },
                new ProductEntity { Id = 7, Name = "Mostaza", Description = "GGG", UnitPrice = 8000, Active = true }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
