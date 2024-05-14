using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Domain.Shipping;


namespace JobTargetCodingChallange.Entity.Sale
{
    public class Order : BaseEntity
    {
        public required ShippingInfo ShippingInfo { get; set; }
        public required List<Product> Products { get; set; }
    }
}
