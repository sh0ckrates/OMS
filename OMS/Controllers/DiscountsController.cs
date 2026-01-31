using Microsoft.AspNetCore.Mvc;
using OMS.Dtos;
using OMS.Models;

[ApiController]
[Route("[controller]")]
public class DiscountsController(DiscountEngine engine) : ControllerBase
{
    private readonly DiscountEngine _engine = engine;

    [HttpPost("calculate")]
    public ActionResult<CalculateDiscountResponse> Calculate([FromBody] CalculateDiscountRequest request)
    {
        // Map request to domain
        var order = new Order(request.OrderId, request.CustomerId, request.BasePrice);

        // Apply discounts
        var summary = _engine.ApplyDiscount(order);

        // Map to response DTO
        var response = new CalculateDiscountResponse
        {
            FinalPrice = summary.FinalPrice,
            Discounts = summary.Discounts.Select(d => new DiscountResultDto
            {
                Name = d.Category.Name,
                Type = d.Category.Type.ToString(),
                Amount = d.Amount,
                PriceAfter = d.PriceAfter
            }).ToList()
        };

        return Ok(response);
    }
}
