using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Domain.Shipping;

namespace JobTargetCodingChallange.Controllers.Request

{
    public class OrderRequest
    {
        public ShippingInfo? ShippingInfo { get; set; }
        public List<Product>? Products { get; set; }

    }
}
