using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OMS.Dtos;
using OMS.Models;

namespace OMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class DiscountsController(IDiscountEngine engine) : ControllerBase
    {
        [HttpPost("calculate")]
        public async Task<ActionResult<CalculateDiscountResponse>> Calculate([FromBody] CalculateDiscountRequest request, CancellationToken ct)
        {
            var order = new Order(request.OrderId, request.CustomerId, request.BasePrice);
            var summary = await engine.ApplyDiscountAsync(order, ct);

            var response = new CalculateDiscountResponse
            {
                FinalPrice = summary.FinalPrice,
                Discounts = summary.Discounts.Select(d => new DiscountResultDto
                {
                    Name = d.Name,
                    Type = d.Kind.ToString(),
                    Amount = d.AmountApplied,
                    PriceAfter = d.PriceAfter
                }).ToList()
            };

            return Ok(response);
        }
    }
}