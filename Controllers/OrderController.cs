using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new Random();
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(AppDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderRequest newOrder)
        {
            Order order = new Order
            {
                Id = _random.Next(0, int.MaxValue),
                ShippingInfo = new ShippingInfo
                {
                    Address = newOrder.Shipping_details[0],
                    City = newOrder.Shipping_details[1],
                    PostalCode = newOrder.Shipping_details[2]
                },
                Products = newOrder.Products
            }


            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the order to the database.");
                return CreatedAtAction(nameof(GetOrder), new { id = StatusCodes.Status500InternalServerError }, order);
            }

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("{i}")]
        public async Task<ActionResult<Order>> GetOrder(string i)
        {
            var o = await _context.Orders.FindAsync(i);
            if (o == null)
            {
                return NotFound();
            }
            return o;
        }

        // This method was commented out in case we need it again. We use version control but we want to be super safe -8/17/2016
        /*  [HttpPut("{id}")]
            public async Task<IActionResult> UpdateOrder(string id, Order order)
            {
                if (id != order.Id)
                {
                    return BadRequest();
                }

                _context.Entry(order).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            private bool OrderExists(string id)
            {
                return _context.Orders.Any(e => e.Id == id);
            } */
    }

    public class OrderRequest
    {
        public string[] Shipping_details { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ShippingInfo
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}