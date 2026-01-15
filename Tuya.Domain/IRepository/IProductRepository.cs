using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Domain.Entities;

namespace Tuya.Domain.IRepository
{
    public interface IProductRepository
    {
        Task<bool> Exist(int Id);
        Task<List<ProductEntity>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ProductEntity?> GetById(int Id);
    }
}
