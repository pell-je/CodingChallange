using JobTargetCodingChallange.Controllers.Request;
using JobTargetCodingChallange.Entity.Sale;
using JobTargetCodingChallange.Services;
using Microsoft.Extensions.Logging;
using Moq;
using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Domain.Shipping;
using Moq.EntityFrameworkCore;

namespace JobTargetCodingChallange.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<ILogger<OrderService>> _loggerMock;
        private Mock<AppDbContext> _dbContextMock;
        private OrderService _orderService;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<OrderService>>();
            _dbContextMock = new Mock<AppDbContext>();
            _orderService = new OrderService(_dbContextMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task CreateFromOrderRequest_ShouldCreateOrder()
        {
            var orderRequest = new OrderRequest
            {
                ShippingInfo = new ShippingInfo { Address = "123 Test St", City = "Testville", PostalCode = "12345" },
                Products = new List<Product> { new Product { Name = "Product1", Price = 10.0M } }
            };
            var entities = new List<Order>();
            _dbContextMock.Setup(x => x.Set<Order>()).ReturnsDbSet(entities);


            var order = await _orderService.CreateFromOrderRequest(orderRequest);

            Assert.IsNotNull(order);
            Assert.AreEqual("123 Test St", order.ShippingInfo.Address);
            Assert.AreEqual(1, order.Products.Count);
            Assert.AreEqual("Product1", order.Products[0].Name);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnOrder_WhenOrderExists()
        {
            var order = new Order
            {
                Id = 1,
                ShippingInfo = new ShippingInfo { Address = "123 Test St", City = "Testville", PostalCode = "12345" },
                Products = [new() { Name = "Product1", Price = 10.0M }]
            };
            var entities = new List<Order> { order };
            _dbContextMock.Setup(x => x.Set<Order>()).ReturnsDbSet(entities);

            var result = await _orderService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("123 Test St", result.ShippingInfo.Address);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnNull_WhenOrderDoesNotExist()
        {
 
            var entities = new List<Order> {};
            _dbContextMock.Setup(x => x.Set<Order>()).ReturnsDbSet(entities);

            var result = await _orderService.GetById(1);

            Assert.IsNull(result);
        }
    }
}
