using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;

namespace Tuya.Application.Port
{
    public interface ICustomerService
    {
        Task Add(CustomerRequest customer);
        Task<List<CustomerResponse>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<CustomerResponse> GetById(int Id);
        Task Update(CustomerRequest customer);
    }
}
