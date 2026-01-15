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
    public class OrderDetailDetailRepository(AppDbContext _context) : IOrderDetailRepository
    {
        //public async Task<List<OrderDetailEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        //{
        //    IQueryable<OrderDetailEntity> Employees;
        //    if (pageNumber.HasValue && pageSize.HasValue)
        //    {
        //        Employees = _context.OrderDetails.AsNoTracking()
        //            .OrderBy(x => x.OrderDetailDate)
        //            .Skip((pageNumber.Value - 1) * pageSize.Value)
        //            .Take(pageSize.Value);
        //    }
        //    else
        //    {
        //        Employees = _context.OrderDetails.AsNoTracking()
        //            .OrderBy(x => x.OrderDetailDate);
        //    }

        //    return await Employees.ToListAsync();
        //}

        public async Task<OrderDetailEntity?> GetById(int Id)
            => await _context.OrderDetails.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Create(OrderDetailEntity entity)
        {
            await _context.OrderDetails.AddAsync(entity);
        }

        public void Update(OrderDetailEntity entity)
        {
            _context.OrderDetails.Update(entity);
            _context.SaveChanges();
        }
    }
}
