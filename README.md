# 🧾 B2B Pricing System

## 📋 Overview
The **B2B Pricing System** is a scalable enterprise-grade application built with **.NET Core** and **Entity Framework Core**, designed to manage and automate business-to-business pricing operations.  
It follows the **Clean Architecture** pattern for maintainability, separation of concerns, and long-term scalability.

---

## 🏗️ Architecture
This project is structured using **Clean Architecture**, ensuring a clear separation between the core business logic and external concerns.

```
src/
├── B2B.Pricing.Domain         → Entities, Value Objects, Domain Events, Exceptions
├── B2B.Pricing.Application    → Business logic, CQRS (Commands & Queries), Validation
├── B2B.Pricing.Infrastructure → Database, EF Core Configurations, Repositories, Services
├── B2B.Pricing.WebAPI         → API Controllers, Dependency Injection, Swagger
```

**Core Principles**
- Domain layer is independent of frameworks  
- Infrastructure and UI depend on the Application layer  
- Uses **CQRS**, **MediatR**, and **Repository Pattern**  
- Promotes **DDD** (Domain-Driven Design)  

---

## ⚙️ Technologies Used
| Category | Technology |
|-----------|-------------|
| Framework | .NET 8 / .NET 7 |
| ORM | Entity Framework Core |
| Architecture | Clean Architecture, DDD, CQRS |
| Messaging | MediatR |
| Database | SQL Server |
| API | ASP.NET Core Web API |
| Logging | Serilog (optional) |
| Documentation | Swagger / OpenAPI |
| Containerization | Docker |


## 💼 Features
- Manage product and pricing data  
- Handle B2B-specific pricing rules and discount logic  
- Clean separation of layers for maintainability  
- EF Core configurations for Value Objects and IDs  
- Support for domain events and exception handling  
- Integrated Swagger UI for API exploration  


