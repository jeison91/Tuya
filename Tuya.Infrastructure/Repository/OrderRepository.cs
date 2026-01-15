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
            IQueryable<OrderEntity> Employees;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Employees = _context.Orders.AsNoTracking()
                    .OrderBy(x => x.OrderDate)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                Employees = _context.Orders.AsNoTracking()
                    .OrderBy(x => x.OrderDate);
            }

            return await Employees.ToListAsync();
        }
        public async Task<OrderEntity?> GetById(int Id)
            => await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Create(OrderEntity entity)
        {
            await _context.Orders.AddAsync(entity);
        }

        public void Update(OrderEntity entity)
        {
            _context.Orders.Update(entity);
            _context.SaveChanges();
        }
    }
}
