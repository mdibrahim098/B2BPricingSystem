using B2B.Pricing.Application.Services.Interfaces;

namespace B2B.Pricing.Application.Services
{
    public class PricingService : IPricingService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerGroupRepository _customerGroupRepository;
        private readonly ICustomerContractPricingRepository _contractPricingRepository;
        private readonly IProductTierPricingRepository _productTierPricingRepository;
        private readonly ICustomerGroupPricingRepository _customerGroupPricingRepository;

        public PricingService(
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            ICustomerGroupRepository customerGroupRepository,
            ICustomerContractPricingRepository contractPricingRepository,
            IProductTierPricingRepository productTierPricingRepository,
            ICustomerGroupPricingRepository customerGroupPricingRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _customerGroupRepository = customerGroupRepository;
            _contractPricingRepository = contractPricingRepository;
            _productTierPricingRepository = productTierPricingRepository;
            _customerGroupPricingRepository = customerGroupPricingRepository;
        }

        public async Task<decimal> ResolvePriceAsync(int customerId, int productId, int quantity, DateTime now)
        {
            // Step 1: Check for Contract Price
            var contract = await _contractPricingRepository.GetActiveContractAsync(customerId, productId, now);

            if (contract != null)
            {
                return contract.Price;
            }

            // Step 2: Check Customer Group and Product Tier Pricing
            var groupPricing = await _customerGroupPricingRepository.GetBestPricingAsync(customerId, productId, quantity);
            var productTierPricing = await _productTierPricingRepository.GetBestPricingAsync(productId, quantity);

            // Step 3: Compare Group Tier and Product Tier prices
            decimal resolvedPrice = Math.Min(groupPricing?.GroupPrice ?? decimal.MaxValue, productTierPricing?.TierPrice ?? decimal.MaxValue);

            // Step 4: Fallback to Base Price
            if (resolvedPrice == decimal.MaxValue)
            {
                var product = await _productRepository.GetByIdAsync(productId);
                resolvedPrice = product?.BasePrice ?? 0m;
            }

            return resolvedPrice;

        }
    }
}
