namespace OMS.Dtos
{
    public class CalculateDiscountResponse
    {
        public decimal FinalPrice { get; set; }
        public List<DiscountResultDto> Discounts { get; set; } = [];
    }
}
