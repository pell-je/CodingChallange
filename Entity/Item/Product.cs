using JobTargetCodingChallange.Entity;
using System.ComponentModel.DataAnnotations;

namespace JobTargetCodingChallange.Domain.Item
{
    public class Product : BaseEntity
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required decimal Price { get; set; }
    }

}
