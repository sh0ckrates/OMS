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

        /// <summary>
        /// Calculates and returns the discount breakdown and final price.
        /// </summary>
        [HttpPost("calculate")]
        public async Task<ActionResult<CalculateDiscountResponse>> Calculate(
            [FromBody] CalculateDiscountRequest request,
            CancellationToken ct)
        {
            // Map request to domain
            var order = new Order(request.OrderId, request.CustomerId, request.BasePrice);

            // Apply discounts
            var summary = await engine.ApplyDiscountAsync(order, ct);

            // Map domain summary to response DTO
            var response = new CalculateDiscountResponse
            {
                FinalPrice = summary.FinalPrice,
                Discounts = summary.Discounts.Select(d => new DiscountResultDto
                {
                    Name = d.Name,
                    Type = d.Kind.ToString(),   // "Percentage" | "Fixed"
                    Amount = d.AmountApplied,
                    PriceAfter = d.PriceAfter
                }).ToList()
            };

            return Ok(response);
        }
    }
}