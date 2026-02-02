# OMS
Order Management System – discount calculation and customer custom fields.

## Structure
| Project | Role |
|---------|------|
| **OMS** | Web API (controllers, DTOs) |
| **OMS.Application** | Discount engine, policies, repository interfaces |
| **OMS.Domain** | Order, discount and customer models, enums |
| **OMS.Infrastructure** | EF Core, SQLite, repositories |
| **OMS.Tests** | Unit tests |

## Run
dotnet run --project OMS

## Database
SQLite, file: `OMS.Infrastructure/oms.db`.

Apply migrations:
dotnet ef database update --project OMS.Infrastructure --startup-project OMS

## Docs
- `docs/ER-Diagram-1.md` – Orders & discounts (Part 1)
- `docs/ER-Diagram-2.md` – Customer & custom fields (Part 2)
- `docs/Part1-Architecture-Diagram.md` – Discount subsystem architecture

## Tests
dotnet test OMS.Tests
