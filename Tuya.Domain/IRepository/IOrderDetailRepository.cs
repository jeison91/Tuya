using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Domain.IRepository
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetailEntity?> GetById(int Id);
        Task Create(OrderDetailEntity entity);
        Task Update(OrderDetailEntity entity);
    }
}
