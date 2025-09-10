

namespace B2B.Pricing.Domain.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }

        // Relationships
        public List<ProductTierPricing> TierPricing { get; set; } = new List<ProductTierPricing>();
        public List<ProductInventory> Inventory { get; set; } = new List<ProductInventory>();
        public List<CustomerGroupPricing> CustomerGroupPricing { get; set; } = new List<CustomerGroupPricing>();
    }


}
