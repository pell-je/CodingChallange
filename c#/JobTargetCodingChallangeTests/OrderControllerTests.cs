using ApiNamespace.Controllers;
using JobTargetCodingChallange.Controllers.Request;
using JobTargetCodingChallange.Entity.Sale;
using JobTargetCodingChallange.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Domain.Shipping;

namespace JobTargetCodingChallange.Tests
{
    [TestClass]
    public class OrdersControllerTests
    {
        private Mock<IOrderService> _orderServiceMock;
        private Mock<ILogger<OrdersController>> _loggerMock;
        private OrdersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _loggerMock = new Mock<ILogger<OrdersController>>();
            _controller = new OrdersController(_orderServiceMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnCreatedOrder()
        {
            var orderRequest = new OrderRequest
            {
                ShippingInfo = new ShippingInfo { Address = "123 Test St", City = "Testville", PostalCode = "12345" },
                Products = [new Product { Name = "Product1", Price = 10.0M }]
            };

            var order = new Order
            {
                Id = 1,
                ShippingInfo = orderRequest.ShippingInfo,
                Products = orderRequest.Products
            };
            _orderServiceMock.Setup(s => s.CreateFromOrderRequest(orderRequest)).ReturnsAsync(order);

            var result = await _controller.CreateOrder(orderRequest);
            var createdAtActionResult = result.Result as CreatedAtActionResult;

            Assert.IsNotNull(createdAtActionResult);
            var createdOrder = createdAtActionResult.Value as Order;
            Assert.AreEqual(order.Id, createdOrder?.Id);
            Assert.AreEqual(orderRequest.ShippingInfo, createdOrder?.ShippingInfo);
            Assert.AreEqual(orderRequest.Products, createdOrder?.Products);
        }


        [TestMethod]
        public async Task GetOrder_InvalidId_ReturnsBadRequest()
        {
            string invalidId = "abc";

            var result = await _controller.GetOrder(invalidId);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("id is not a valid long", badRequestResult?.Value);
        }

        [TestMethod]
        public async Task GetOrder_OrderNotFound_ReturnsNotFound()
        {
            string validId = "123";
            _orderServiceMock.Setup(service => service.GetById(It.IsAny<long>()))
                             .ReturnsAsync(null as Order);

            var result = await _controller.GetOrder(validId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetOrder_OrderFound_ReturnsOrder()
        {
            string validId = "123";
            var expectedOrder = new Order
            {
                Id = 123,
                ShippingInfo = new ShippingInfo
                {
                    Address = "123 Main St",
                    City = "Metropolis",
                    PostalCode = "12345"
                },
                Products =
            [
                new() { Id = 1, Name = "Product1", Price = 10.00M },
                new() { Id = 2, Name = "Product2", Price = 20.00M }
            ]
            };
            _orderServiceMock.Setup(service => service.GetById(It.IsAny<long>()))
                             .ReturnsAsync(expectedOrder);


            var result = await _controller.GetOrder(validId);
            var order = result.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOrder.Id, order?.Id);
            Assert.AreEqual(expectedOrder.ShippingInfo.Address, order?.ShippingInfo.Address);
            Assert.AreEqual(expectedOrder.ShippingInfo.City, order?.ShippingInfo.City);
            Assert.AreEqual(expectedOrder.ShippingInfo.PostalCode, order?.ShippingInfo.PostalCode);
            Assert.AreEqual(expectedOrder.Products.Count, order?.Products.Count);
            for (int i = 0; i < expectedOrder.Products.Count; i++)
            {
                Assert.AreEqual(expectedOrder.Products[i].Id, order?.Products[i].Id);
                Assert.AreEqual(expectedOrder.Products[i].Name, order?.Products[i].Name);
                Assert.AreEqual(expectedOrder.Products[i].Price, order?.Products[i].Price);
            }
        }

    }
}
