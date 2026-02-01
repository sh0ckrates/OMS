namespace OMS.Infrastructure.Entities.Customer
{
    public class CustomFieldOptionEntity
    {
        public Guid Id { get; set; }
        public Guid CustomFieldDefinitionId { get; set; }
        public CustomFieldEntity CustomFieldDefinition { get; set; }
        public string Value { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
