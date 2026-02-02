erDiagram
    Order {
        Guid Id PK
        Guid CustomerId
        decimal BasePrice
        decimal FinalPrice
        DateTime CreatedAt
    }

    AppliedDiscount {
        Guid Id PK
        Guid OrderId FK
        Guid DiscountCategoryId FK
        string DiscountName
        decimal Amount
        decimal PriceAfter
        DateTime AppliedAt
    }

    DiscountCategory {
        Guid Id PK
        string Name
        int Type
        decimal Value
        int Priority
        bool IsActive
    }

    Customer {
        Guid Id PK
        string Name
        DateTime CreatedAt
    }

    CustomFieldDefinition {
        Guid Id PK
        string Name
        int Type
        DateTime CreatedAt
        string CreatedBy
    }

    CustomFieldOption {
        Guid Id PK
        Guid CustomFieldDefinitionId FK
        string Value
        int SortOrder
    }

    CustomFieldValue {
        Guid Id PK
        Guid CustomerId FK
        Guid CustomFieldDefinitionId FK
        string Value
    }

    CustomerFieldValueHistory {
        Guid Id PK
        Guid CustomerId FK
        Guid CustomFieldDefinitionId FK
        string OldValue
        string NewValue
        DateTime ChangedAt
        string ChangedBy
    }

    Order ||--o{ AppliedDiscount : "has"
    DiscountCategory ||--o{ AppliedDiscount : "categorizes"
    Order ||--o{ Customer : "CustomerId references"

    Customer ||--o{ CustomFieldValue : "has"
    CustomFieldDefinition ||--o{ CustomFieldValue : "defines"
    CustomFieldDefinition ||--o{ CustomFieldOption : "has options"

    Customer ||--o{ CustomerFieldValueHistory : "has history"
    CustomFieldDefinition ||--o{ CustomerFieldValueHistory : "for field"