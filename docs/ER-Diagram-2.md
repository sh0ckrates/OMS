erDiagram
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

    Customer ||--o{ CustomFieldValue : "1 customer, many values"
    CustomFieldDefinition ||--o{ CustomFieldValue : "1 field, many customer values"
    CustomFieldDefinition ||--o{ CustomFieldOption : "1 field, many options"
    Customer ||--o{ CustomerFieldValueHistory : "1 customer, many history rows"
    CustomFieldDefinition ||--o{ CustomerFieldValueHistory : "1 field, many history rows"