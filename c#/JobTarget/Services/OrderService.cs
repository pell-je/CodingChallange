using JobTargetCodingChallange.Controllers.Request;
using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Entity.Sale;
using JobTargetCodingChallange.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace JobTargetCodingChallange.Services
{
    public class OrderService : BaseEntityReadCreateService<Order>, IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(AppDbContext context, ILogger<OrderService> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order> CreateFromOrderRequest(OrderRequest orderRequest)
        {

            Order order = new()
            {
                ShippingInfo = orderRequest.ShippingInfo,
                Products = orderRequest.Products
            };

            return await Create(order);

            }


        public override async Task<Order?> GetById(long id)
        {
            var entity = await _context.Set<Order>()
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (entity == null)
            {
                _logger.LogInformation($"Did not find {typeof(Order)} with id of {id}");
            }
            else
            {
                _logger.LogInformation($"Found {entity.GetType()} {JsonSerializer.Serialize(entity)}");
            }

            return entity;
        }

    }





}
