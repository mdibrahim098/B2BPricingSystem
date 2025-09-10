using B2B.Pricing.Application.Services;
using B2B.Pricing.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2B.Pricing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IPricingService _pricingService;

        public PricingController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet("resolve-price")]
        public async Task<IActionResult> ResolvePrice(int customerId, int productId, int quantity)
        {
            var price = await _pricingService.ResolvePriceAsync(customerId, productId, quantity, DateTime.UtcNow);
            return Ok(new { Price = price });
        }


    }
}
