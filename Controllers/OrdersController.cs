using JobTargetCodingChallange.Controllers.Request;
using JobTargetCodingChallange.Entity.Sale;
using JobTargetCodingChallange.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderRequest orderRequest)
        {
            Order order = await _orderService.CreateFromOrderRequest(orderRequest);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {

            if (!long.TryParse(id, out long longId))
            {
                return BadRequest("id is not a valid long");
            }

            var order = await _orderService.GetById(longId);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
    }

}