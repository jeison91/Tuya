using AutoMapper;
using Azure.Core;
using Moq;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;
using Tuya.Application.UseCase;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;
using Tuya.Transversal;

namespace Tuya.Test.Application
{
    public class CustomerUseCaseTest
    {
        private readonly Mock<ICustomerRepository> _mockICustomerRepository;
        private readonly Mock<IMapper> _mockIMapper;
        private readonly CustomerUseCase _customerUseCase;
        public CustomerUseCaseTest()
        {
            _mockICustomerRepository = new();
            _mockIMapper = new();
            _customerUseCase = new(_mockICustomerRepository.Object, _mockIMapper.Object);
        }

        [Fact]
        public async Task Add_Customer_NotExist()
        {
            // Arrange
            var CustomerRequest = new CustomerRequest()
            {
                Id = 999999999,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            var CustomerEntity = new CustomerEntity()
            {
                Id = 999999999,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            _mockIMapper.Setup(m => m.Map<CustomerEntity>(It.IsAny<CustomerRequest>())).Returns(CustomerEntity);
            _mockICustomerRepository.Setup(x => x.Exist(It.IsAny<int>())).ReturnsAsync(false);
            _mockICustomerRepository.Setup(x => x.Create(It.IsAny<CustomerEntity>())).Returns(Task.CompletedTask);

            // Act
            await _customerUseCase.Add(CustomerRequest);

            _mockICustomerRepository.Verify(r => r.Create(CustomerEntity), Times.Once);

        }

        [Fact]
        public async Task Add_Customer_Exist()
        {
            // Arrange
            var CustomerRequest = new CustomerRequest()
            {
                Id = 999999999,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            var CustomerEntity = new CustomerEntity()
            {
                Id = 999999999,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            _mockIMapper.Setup(m => m.Map<CustomerEntity>(It.IsAny<CustomerRequest>())).Returns(CustomerEntity);
            _mockICustomerRepository.Setup(x => x.Exist(It.IsAny<int>())).ReturnsAsync(true);
            _mockICustomerRepository.Setup(x => x.Create(It.IsAny<CustomerEntity>())).Returns(Task.CompletedTask);

            // Act
            await Assert.ThrowsAsync<BadRequestException>(() => _customerUseCase.Add(CustomerRequest));

            _mockICustomerRepository.Verify(r => r.Create(CustomerEntity), Times.Never);

        }

        [Fact]
        public async Task All_Customer_List()
        {
            // Arrange
            var customerResponse = new CustomerResponse()
            {
                Id = 999999999,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };

            _mockICustomerRepository.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync([]);
            _mockIMapper.Setup(m => m.Map<List<CustomerResponse>>(It.IsAny<List<CustomerEntity>>())).Returns([customerResponse]);

            // Act
            var result = await _customerUseCase.GetAll();

            Assert.Single(result);
            _mockICustomerRepository.Verify(r => r.GetAll(null, null), Times.Once);
        }

        [Fact]
        public async Task GetById_Customer_Found()
        {
            // Arrange
            int id = 999999999;
            var customerResponse = new CustomerResponse()
            {
                Id = id,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            var CustomerEntity = new CustomerEntity()
            {
                Id = id,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };

            _mockICustomerRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(CustomerEntity);
            _mockIMapper.Setup(m => m.Map<CustomerResponse>(It.IsAny<CustomerEntity>())).Returns(customerResponse);

            // Act
            var result = await _customerUseCase.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(customerResponse.Id, result.Id);
            _mockICustomerRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [Fact]
        public async Task GetById_Customer_NotFound()
        {
            // Arrange
            int id = 999999999;
            var CustomerEntity = new CustomerEntity()
            {
                Id = id,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };

            _mockICustomerRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((CustomerEntity)null);

            // Act 
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _customerUseCase.GetById(id));

            //Assert
            Assert.Contains("No se encontro el cliente.", exception.Message);
        }

        [Fact]
        public async Task Update_Customer_When_Exists()
        {
            // Arrange
            int id = 999999999;
            var CustomerRequest = new CustomerRequest()
            {
                Id = id,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };
            var CustomerEntity = new CustomerEntity()
            {
                Id = id,
                Name = "Prueba",
                Address = "Carrera 25",
                CellPhone = "3004568523",
                Email = "prueba@prueba.com"
            };

            _mockICustomerRepository.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(CustomerEntity);
            _mockIMapper.Setup(m => m.Map<CustomerEntity>(CustomerRequest)).Returns(CustomerEntity);

            // Act
            await _customerUseCase.Update(CustomerRequest);

            // Assert
            _mockICustomerRepository.Verify(r => r.GetById(id), Times.Once);
            _mockICustomerRepository.Verify(r => r.Update(CustomerEntity), Times.Once);
        }
    }
}