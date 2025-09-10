namespace B2B.Pricing.WebAPI.ViewModel
{
    public class CatalogProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int Quantity { get; set; }
        public decimal ResolvedPrice { get; set; }
        public string Source { get; set; }

    }
}
