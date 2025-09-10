

namespace B2B.Pricing.Domain.Models
{
    public class ProductTierPricing
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal TierPrice { get; set; }

        // Relationship
        public Product Product { get; set; }

        // Checks if the pricing tier is applicable for a specific quantity
        public bool IsApplicable(int quantity)
        {
            return quantity >= MinQuantity && quantity <= MaxQuantity;
        }


    }
}
