using OMS.Enums;

namespace OMS.Infrastructure.Entities.Customer
{
    public class CustomFieldEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CustomFieldType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        public ICollection<CustomFieldOptionEntity> Options { get; set; } = [];
        public ICollection<CustomFieldValueEntity> CustomerValues { get; set; } = [];
        public ICollection<CustomerFieldValueHistoryEntity> ValueHistory { get; set; } = [];
    }
}
