using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Application.Dto.Response;

namespace Tuya.Application.Port
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<ProductResponse> GetById(int Id);
    }
}
