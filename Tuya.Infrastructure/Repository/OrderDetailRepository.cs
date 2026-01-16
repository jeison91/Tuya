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
    public class OrderDetailRepository(AppDbContext _context) : IOrderDetailRepository
    {
        public async Task<OrderDetailEntity?> GetById(int Id)
            => await _context.OrderDetails.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Create(OrderDetailEntity entity)
        {
            await _context.OrderDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OrderDetailEntity entity)
        {
            _context.OrderDetails.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
