using JobTargetCodingChallange.Entity;

namespace JobTargetCodingChallange.Domain.Item
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }

}
