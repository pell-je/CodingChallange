using System.ComponentModel.DataAnnotations;

namespace JobTargetCodingChallange.Domain.Shipping
{
    public class ShippingInfo
    {
        [Required]
        public required string Address { get; set; }

        [Required]
        public required string City { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid postal code. It must be exactly 5 digits.")]
        public required string PostalCode { get; set; }

    }

}
