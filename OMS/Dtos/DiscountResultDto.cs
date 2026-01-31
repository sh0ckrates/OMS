namespace OMS.Dtos
{
    public class DiscountResultDto
    {
        public string Name { get; set; }       // Discount name
        public string Type { get; set; }       // Discount type, e.g., "Percentage" or "FixedAmount"
        public decimal Amount { get; set; }    // Amount discounted
        public decimal PriceAfter { get; set; } // Price after applying this discount
    }
}
