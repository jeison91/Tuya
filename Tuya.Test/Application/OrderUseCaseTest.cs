using AutoMapper;
using Azure.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tuya.Application.Dto.Request;
using Tuya.Application.Dto.Response;
using Tuya.Application.Enums;
using Tuya.Application.UseCase;
using Tuya.Domain.Entities;
using Tuya.Domain.IRepository;
using Tuya.Domain.Unit;
using Tuya.Transversal;
using Tuya.Transversal.Helper;

namespace Tuya.Test.Application
{
    public class OrderUseCaseTest
    {
        private readonly Mock<IOrderRepository> _mockIOrderRepository;
        private readonly Mock<IMapper> _mockIMapper;
        private readonly OrderUseCase _orderUseCase;
        public OrderUseCaseTest()
        {
            _mockIOrderRepository = new();
            _mockIMapper = new();
            _orderUseCase = new(_mockIOrderRepository.Object, _mockIMapper.Object);
        }

        [Fact]
        public async Task Add_Order_Insert_Details_When_Valid()
        {
            // Arrange
            var request = new OrderRequest();
            var entity = new OrderEntity
            {
                Address = "Calle 60",
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 12000 }
                ]
            };

            _mockIMapper.Setup(m => m.Map<OrderEntity>(request)).Returns(entity);
            _mockIOrderRepository.Setup(x => x.Create(entity)).Returns(Task.CompletedTask);

            // Act
            await _orderUseCase.Add(request);

            // Assert
            _mockIOrderRepository.Verify(r => r.Create(entity), Times.Once);
        }

        [Fact]
        public async Task Add_No_OrderDetails()
        {
            // Arrange
            var request = new OrderRequest();
            var entity = new OrderEntity
            {
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.PROCESS),
                OrderDetails = []
            };

            _mockIMapper.Setup(m => m.Map<OrderEntity>(request)).Returns(entity);

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.Add(request));

            // Assert
            Assert.Contains("La orden no tiene productos seleccionados.", exception.Message);
        }

        [Fact]
        public async Task All_Order_List()
        {
            // Arrange
            var orderResponse = new OrderResponse
            {
                IdCustomer = 1,
                CustomerName = "Carlos Andres Perez",
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.FINISH),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync([]);
            _mockIMapper.Setup(m => m.Map<List<OrderResponse>>(It.IsAny<List<OrderEntity>>())).Returns([orderResponse]);

            // Act
            var result = await _orderUseCase.GetAll(1, 10);

            Assert.Single(result);
            _mockIOrderRepository.Verify(r => r.GetAll(1, 10), Times.Once);
        }

        [Fact]
        public async Task GetById_Order_Found()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.FINISH),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };
            var orderResponse = new OrderResponse
            {
                Id = id,
                IdCustomer = 1,
                CustomerName = "Carlos Andres Perez",
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.FINISH),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(orderEntity);
            _mockIMapper.Setup(m => m.Map<OrderResponse>(It.IsAny<OrderEntity>())).Returns(orderResponse);

            // Act
            var result = await _orderUseCase.GetById(id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(orderResponse.Id, result.Id);
        }

        [Fact]
        public async Task GetById_Customer_NotFound()
        {
            // Arrange
            int id = 100;
            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((OrderEntity)null);

            // Act 
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.GetById(id));

            //Assert
            Assert.Contains("No se encontro la orden.", exception.Message);
        }

        [Fact]
        public async Task FinishOrder_Order_StateProcess()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.PROCESS),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(orderEntity);
            _mockIOrderRepository.Setup(x => x.Update(It.IsAny<OrderEntity>())).Returns(Task.CompletedTask);

            // Act
            await _orderUseCase.FinishOrder(id);

            //Assert
            // Assert
            _mockIOrderRepository.Verify(r => r.GetById(id), Times.Once);
            _mockIOrderRepository.Verify(r => r.Update(orderEntity), Times.Once);
        }

        [Fact]
        public async Task FinishOrder_Order_StateProcess_NotFound()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.PROCESS),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((OrderEntity)null);

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.FinishOrder(id));

            // Assert
            Assert.Contains("La orden no existe.", exception.Message);
        }

        [Fact]
        public async Task FinishOrder_Order_StateCancel()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.CANCEL),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(orderEntity);

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.FinishOrder(id));

            // Assert
            Assert.Contains("La orden ya fue cancelada, no se puede finalizar", exception.Message);
        }

        [Fact]
        public async Task CancelOrder_Order_StateProcess()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.PROCESS),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(orderEntity);
            _mockIOrderRepository.Setup(x => x.Update(It.IsAny<OrderEntity>())).Returns(Task.CompletedTask);

            // Act
            await _orderUseCase.CancelOrder(id);

            //Assert
            // Assert
            _mockIOrderRepository.Verify(r => r.GetById(id), Times.Once);
            _mockIOrderRepository.Verify(r => r.Update(orderEntity), Times.Once);
        }

        [Fact]
        public async Task CancelOrder_Order_StateProcess_NotFound()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.PROCESS),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((OrderEntity)null);

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.CancelOrder(id));

            // Assert
            Assert.Contains("La orden no existe.", exception.Message);
        }

        [Fact]
        public async Task CancelOrder_Order_StateFinish()
        {
            // Arrange
            int id = 100;
            var orderEntity = new OrderEntity
            {
                Id = id,
                IdCustomer = 1,
                OrderDate = DateTime.Now,
                Address = "Calle 60",
                State = Helpers.GetDescription(StateOrder.FINISH),
                Total = 56000,
                OrderDetails =
                [
                    new() { ProductId = 1, Quantity = 2, UnitPrice = 25000 },
                    new() { ProductId = 2, Quantity = 1, UnitPrice = 6000 }
                ]
            };

            _mockIOrderRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(orderEntity);

            // Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _orderUseCase.CancelOrder(id));

            // Assert
            Assert.Contains("La orden ya fue finalizada, no se puede cancelar.", exception.Message);
        }
    }
}
