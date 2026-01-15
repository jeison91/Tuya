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
    public class ProductUseCase(IProductRepository _productRepository, IMapper _mapper) : IProductService
    {
        public async Task<List<ProductResponse>> GetAll(int? pageNumber = null, int? pageSize = null)
        {
            var Products = await _productRepository.GetAll(pageNumber, pageSize);
            var MapProducts = _mapper.Map<List<ProductResponse>>(Products);

            return MapProducts;
        }

        public async Task<ProductResponse> GetById(int Id)
        {
            var Product = await _productRepository.GetById(Id) ??
                throw new BadRequestException(JsonSerializer.Serialize(new MessageResponse() { Status = 400, Message = "No se encontro el producto." }));
            var MapProduct = _mapper.Map<ProductResponse>(Product);
            return MapProduct;
        }
    }
}
