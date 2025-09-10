using B2B.Pricing.Application.Services;
using B2B.Pricing.Application.Services.Interfaces;
using B2B.Pricing.WebAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Pricing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly IPricingService _pricingService;

        // Injecting the PricingService
        public CatalogController(IProductRepository productRepository, IPricingService pricingService)
        {
            _productRepository = productRepository;
            _pricingService = pricingService;
        }

        // GET: api/catalog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogProductViewModel>>> GetCatalog()
        {
            // Fetch a limited number of products
            var products = await _productRepository.GetAllAsync();

            var catalogViewModel = products.Select(p => new CatalogProductViewModel
            {
                ProductId = p.Id,
                Name = p.Name,
                Description = p.Description,
                BasePrice = p.BasePrice,
                Quantity = 1,  // Default to 1 quantity
                ResolvedPrice = p.BasePrice, // Default resolved price based on base price
                Source = "Base Price" // Example source
            }).ToList();

            return catalogViewModel;
        }

        // POST: api/catalog/preview
        [HttpPost("preview")]
        public async Task<ActionResult<CatalogProductViewModel>> PreviewPrice([FromBody] CatalogProductViewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.ProductId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Use PricingService to resolve the price
            decimal resolvedPrice = await _pricingService.ResolvePriceAsync(
                customerId: 1,  // Example customer ID (replace with real value from request)
                productId: model.ProductId,
                quantity: model.Quantity,
                now: DateTime.Now
            );

            // Set resolved price and return the model
            model.ResolvedPrice = resolvedPrice;

            return model;
        }
    }

}
