using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuya.Application.Dto.Response;
using Tuya.Application.UseCase;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;
using Tuya.Transversal;

namespace Tuya.Test.Application
{
    public class ProductUseCaseTest
    {
        private readonly Mock<IProductRepository> _mockIProductRepository;
        private readonly Mock<IMapper> _mockIMapper;
        private readonly ProductUseCase _productUseCase;

        public ProductUseCaseTest()
        {
            _mockIProductRepository = new();
            _mockIMapper = new();
            _productUseCase = new(_mockIProductRepository.Object, _mockIMapper.Object);
        }

        [Fact]
        public async Task All_Product_List()
        {
            // Arrange
            var productResponse = new ProductResponse()
            {
                Id = 10256,
                Name = "Huevos",
                Description = "Producto de la canasta familiar",
                UnitPrice = 16000,
                Active = true
            };
            var entity = new ProductEntity()
            {
                Id = 10256,
                Name = "Huevos",
                Description = "Producto de la canasta familiar",
                UnitPrice = 16000,
                Active = true
            };

            _mockIProductRepository.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync([entity]);
            _mockIMapper.Setup(m => m.Map<List<ProductResponse>>(It.IsAny<List<ProductEntity>>())).Returns([productResponse]);

            // Act
            var result = await _productUseCase.GetAll(1, 10);

            Assert.Single(result);
            _mockIProductRepository.Verify(r => r.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetById_Product_Found()
        {
            // Arrange
            int id = 10256;
            var productResponse = new ProductResponse()
            {
                Id = 10256,
                Name = "Huevos",
                Description = "Producto de la canasta familiar",
                UnitPrice = 16000,
                Active = true
            };
            var entity = new ProductEntity()
            {
                Id = 10256,
                Name = "Huevos",
                Description = "Producto de la canasta familiar",
                UnitPrice = 16000,
                Active = true
            };

            _mockIProductRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(entity);
            _mockIMapper.Setup(m => m.Map<ProductResponse>(It.IsAny<ProductEntity>())).Returns(productResponse);

            // Act
            var result = await _productUseCase.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(productResponse.Id, result.Id);
            _mockIProductRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [Fact]
        public async Task GetById_Product_NotFound()
        {
            // Arrange
            int id = 10256;
            _mockIProductRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((ProductEntity)null);

            // Act 
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _productUseCase.GetById(id));

            //Assert
            Assert.Contains("No se encontro el producto.", exception.Message);
        }
    }
}
