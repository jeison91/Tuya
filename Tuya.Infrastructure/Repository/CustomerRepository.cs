using Microsoft.EntityFrameworkCore;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;

namespace Tuya.Infrastructure.Repository
{
    public class CustomerRepository(AppDbContext _context) : ICustomerRepository
    {
        public async Task<bool> Exist(int Id)
            => await _context.Customers.AsNoTracking().AnyAsync(e => e.Id == Id);

        public async Task<List<CustomerEntity>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<CustomerEntity> Employees;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                Employees = _context.Customers.AsNoTracking()
                    .OrderBy(x => x.Name)
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                Employees = _context.Customers.AsNoTracking()
                    .OrderBy(x => x.Name);
            }

            return await Employees.ToListAsync();
        }

        public async Task<CustomerEntity?> GetById(int Id)
            => await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);

        public async Task Create(CustomerEntity entity)
        {
            await _context.Customers.AddAsync(entity);
            _context.SaveChanges();
        }

        public void Update(CustomerEntity entity)
        {
            _context.Customers.Update(entity);
            _context.SaveChanges();
        }
    }
}
