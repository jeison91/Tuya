using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Domain.IRepository
{
    public interface IOrderRepository
    {
        Task<bool> Exist(int Id);
        Task<List<OrderEntity>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<OrderEntity?> GetById(int Id);
        Task Create(OrderEntity entity);
        Task Update(OrderEntity entity);

    }
}
