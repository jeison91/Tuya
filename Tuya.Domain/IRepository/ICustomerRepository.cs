using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Domain.IRepository
{
    public interface ICustomerRepository
    {
        Task<bool> Exist(int Id);
        Task<List<CustomerEntity>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<CustomerEntity?> GetById(int Id);
        Task Create(CustomerEntity entity);
        Task Update(CustomerEntity entity);
    }
}
