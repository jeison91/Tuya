using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;

namespace Tuya.Application.Port
{
    public interface IOrderService
    {
        Task Add(OrderRequest orderRequest);
        Task<List<OrderResponse>> GetAll(int? pageNumber = null, int? pageSize = null);
        Task<OrderResponse> GetById(int Id);
        Task FinishOrder(int Id);
        Task CancelOrder(int Id);
    }
}
