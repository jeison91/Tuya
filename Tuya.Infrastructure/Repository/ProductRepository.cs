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
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {
        public async Task<bool> Exist(int Id)
            => await _context.Products.AsNoTracking().AnyAsync(e => e.Id == Id);

        public async Task<List<ProductEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<ProductEntity> Employees;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Employees = _context.Products.AsNoTracking()
                    .Where(x => x.Active == true)
                    .OrderBy(x => x.Name)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                Employees = _context.Products.AsNoTracking()
                    .Where(x => x.Active == true)
                    .OrderBy(x => x.Name);
            }

            return await Employees.ToListAsync();
        }
        public async Task<ProductEntity?> GetById(int Id)
            => await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
    }
}
