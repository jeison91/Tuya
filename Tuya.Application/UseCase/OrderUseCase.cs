using AutoMapper;
using System.Text.Json;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;
using Tuya.Application.Enums;
using Tuya.Application.Port;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;
using Tuya.Domain.Unit;
using Tuya.Transversal;
using Tuya.Transversal.Helper;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Application.UseCase
{
    public class OrderUseCase(IOrderRepository _orderRepository, IOrderDetailRepository _orderDetailRepository, IMapper _mapper,
        IUnitOfWork _unitofWork) : IOrderService
    {
        public async Task Add(OrderRequest orderRequest)
        {
            var order = _mapper.Map<OrderEntity>(orderRequest);
            if (order.OrderDetails.Count == 0)
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"La orden no tiene productos seleccionados." }));

            order.State = Helpers.GetDescription(StateOrder.PROCESS);
            await _orderRepository.Create(order);
            await InsertDetail(order);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task<List<OrderResponse>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var order = await _orderRepository.GetAll(pageNumber, pageSize);
            var response = _mapper.Map<List<OrderResponse>>(order);
            return response;
        }

        public async Task<OrderResponse> GetById(int Id)
        {
            var order = await _orderRepository.GetById(Id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "No se encontro la orden." }));
            var MapOrder = _mapper.Map<OrderResponse>(order);
            return MapOrder;
        }

        public async Task FinishOrder(int Id)
        {
            var order = await _orderRepository.GetById(Id) ?? 
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"La orden no existe." }));
            if (order.State == Helpers.GetDescription(StateOrder.FINISH))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"La orden ya fue finalizada." }));

            order.State = Helpers.GetDescription(StateOrder.FINISH);
            await _orderRepository.Update(order);
        }

        public async Task CancelOrder(int Id)
        {
            var order = await _orderRepository.GetById(Id) ?? 
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"La orden no existe." }));
            if (order.State == Helpers.GetDescription(StateOrder.FINISH))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"La orden ya fue finalizada, no se puede cancelar." }));

            order.State = Helpers.GetDescription(StateOrder.CANCEL);
            await _orderRepository.Update(order);
        }

        private async Task InsertDetail(OrderEntity orderEntity)
        {
            OrderDetailEntity orderDetail = new()
            {
                OrderId = orderEntity.Id
            };
            await _orderDetailRepository.Create(orderDetail);
        }
    }
}
