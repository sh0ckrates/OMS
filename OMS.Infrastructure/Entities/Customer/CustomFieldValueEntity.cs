namespace OMS.Infrastructure.Entities.Customer
{
    public class CustomFieldValueEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public Guid CustomFieldDefinitionId { get; set; }
        public CustomFieldEntity CustomFieldDefinition { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
