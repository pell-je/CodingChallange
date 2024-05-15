using JobTargetCodingChallange.Controllers.Request;
using JobTargetCodingChallange.Entity.Sale;

namespace JobTargetCodingChallange.Services
{
    public interface IOrderService : IReadCreateService<Order>
    {
        Task<Order> CreateFromOrderRequest(OrderRequest orderRequest);

    }
}
