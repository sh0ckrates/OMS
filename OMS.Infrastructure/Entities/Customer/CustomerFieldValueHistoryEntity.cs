namespace OMS.Infrastructure.Entities.Customer
{
    public class CustomerFieldValueHistoryEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public Guid CustomFieldDefinitionId { get; set; }
        public CustomFieldEntity CustomFieldDefinition { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string ChangedBy { get; set; }
    }
}
