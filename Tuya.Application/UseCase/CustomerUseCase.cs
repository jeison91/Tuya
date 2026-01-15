using AutoMapper;
using System.Text.Json;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;
using Tuya.Application.Port;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;
using Tuya.Transversal;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Application.UseCase
{
    public class CustomerUseCase(ICustomerRepository _customerRepository, IMapper _mapper) : ICustomerService
    {
        public async Task Add(CustomerRequest customer)
        {
            var entity = _mapper.Map<CustomerEntity>(customer);
            if (await _customerRepository.Exist(entity.Id))
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = $"Ya existe un cliente con el número de identificación: {entity.Id}" }));

            await _customerRepository.Create(entity);
        }

        public async Task<List<CustomerResponse>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var customers = await _customerRepository.GetAll(pageNumber, pageSize);
            var MapCustomers = _mapper.Map<List<CustomerResponse>>(customers);

            return MapCustomers;
        }

        public async Task<CustomerResponse> GetById(int Id)
        {
            var customer = await _customerRepository.GetById(Id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "No se encontro el cliente." }));
            var MapCustomer = _mapper.Map<CustomerResponse>(customer);
            return MapCustomer;
        }

        public async Task Update(CustomerRequest customer)
        {
            await GetById(customer.Id);
            var entity = _mapper.Map<CustomerEntity>(customer);
            await _customerRepository.Update(entity);
        }
    }
}
