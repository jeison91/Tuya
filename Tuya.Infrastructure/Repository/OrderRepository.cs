using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;

namespace Tuya.Infrastructure.Repository
{
    public class OrderRepository(AppDbContext _context) : IOrderRepository
    {
        public async Task<bool> Exist(int Id)
            => await _context.Orders.AsNoTracking().AnyAsync(e => e.Id == Id);

        public async Task<List<OrderEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<OrderEntity> Orders;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Orders = _context.Orders.AsNoTracking()
                    .Include(x => x.OrderDetails).ThenInclude(a => a.ProductEntity)
                    .Include(x => x.Customer)
                    .OrderBy(x => x.Id)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                Orders = _context.Orders.AsNoTracking()
                    .Include(x => x.OrderDetails).ThenInclude(a => a.ProductEntity)
                    .Include(x => x.Customer)
                    .OrderBy(x => x.OrderDate);
            }

            return await Orders.ToListAsync();
        }
        public async Task<OrderEntity?> GetById(int Id)
            => await _context.Orders.AsNoTracking()
            .Include(x => x.OrderDetails).ThenInclude(a => a.ProductEntity)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Create(OrderEntity entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OrderEntity entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
