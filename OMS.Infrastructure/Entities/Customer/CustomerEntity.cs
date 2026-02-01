namespace OMS.Infrastructure.Entities.Customer
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<CustomFieldValueEntity> CustomFieldValues { get; set; } = [];
        public ICollection<CustomerFieldValueHistoryEntity> FieldValueHistory { get; set; } = [];
    }
}
