using JobTargetCodingChallange.Domain.Item;
using JobTargetCodingChallange.Domain.Shipping;
using System.ComponentModel.DataAnnotations;


namespace JobTargetCodingChallange.Entity.Sale
{
    public class Order : BaseEntity
    {
        [Required]
        public required ShippingInfo ShippingInfo { get; set; }

        [Required]
        public required List<Product> Products { get; set; }
    }
}
