# 🍔 Online Food Order API

A RESTful API built with ASP.NET for managing and processing online food orders.

---

## 📖 Overview

**Online Food Order API** is a backend REST API developed using ASP.NET that enables users to browse food items, place orders, and manage the ordering process seamlessly.

It is designed to serve as the backend for any food ordering platform — whether a web app or mobile app — providing clean and structured API endpoints for handling food menus, orders, and more.
## ✨ Features

- 🔐 **User Authentication** — Register, login, and role-based access control
- 🛒 **Basket Management** — Add, view, and remove items from the basket
- 📦 **Order Processing** — Place orders and view order history with delivery methods
- 💳 **Payments** — Payment intent creation and webhook handling
- 🍽️ **Product Catalog** — Browse products, filter by brands and types

---

## 🔗 API Endpoints

### Account
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Account/register` | Register a new user |
| POST | `/api/Account/login` | Login and get token |
| GET | `/api/Account` | Get current user info |
| GET | `/api/Account/EmailExists` | Check if email is taken |
| GET | `/api/Account/address` | Get user address |
| PUT | `/api/Account/address` | Update user address |
| GET | `/api/Account/roles` | Get user roles |

### Basket
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Basket` | Get basket |
| POST | `/api/Basket` | Add/update basket |
| DELETE | `/api/Basket/{key}` | Delete basket |

### Order
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Order` | Create a new order |
| GET | `/api/Order` | Get all user orders |
| GET | `/api/Order/{id}` | Get order by ID |
| GET | `/api/Order/DeliveryMethods` | Get delivery options |

### Payments
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Payments/{basketId}` | Create payment intent |
| POST | `/api/Payments/webhook` | Handle payment webhook |

### Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products` | Get all products |
| GET | `/api/Products/{id}` | Get product by ID |
| GET | `/api/Products/Brands` | Get all brands |
| GET | `/api/Products/Types` | Get all types |

## 🛠️ Technologies Used

### Backend
| Technology | Details |
|------------|---------|
| **Framework** | ASP.NET Core 9 Web API |
| **Database** | SQL Server |
| **Cache / In-Memory DB** | Redis |
| **Authentication** | ASP.NET Core Identity |
| **Architecture** | Onion Architecture |
| **Design Patterns** | Repository Pattern, Unit of Work, Specification Pattern |

### External Services
- **Payment**: Stripe API

### Development Tools
- **ORM**: Entity Framework Core
- **Database Migrations**: Code-First approach
- **Dependency Injection**: Built-in ASP.NET Core DI container

---

## 📁 Project Structure

```
OnlineFoodOrder-API/
│
├── 💾 Core/
│   ├── 📊 DomainLayer/ — Entity models, interfaces, and exceptions
│   │   ├── 📝 Contracts/ — Repository interfaces (IGenericRepository, IUnitOfWork, ISpecifications)
│   │   ├── ⚠️ Exceptions/ — Custom exceptions (NotFoundException, ValidationException, UnAuthorizedException)
│   │   └── 🏗️ Models/ — Domain entities (Product, Order, Basket, Identity models)
│   │
│   ├── 🔧 Service/ — Business logic implementation
│   │   ├── 🗺️ MappingProfiles/ — AutoMapper profiles for DTOs
│   │   ├── 🔍 Specifications/ — Query specifications for filtering/sorting
│   │   ├── 🔐 AuthenticationService.cs — User registration, login, JWT token generation
│   │   ├── 🛒 BasketService.cs — Add/remove items from basket
│   │   ├── 📦 OrderService.cs — Create and manage orders
│   │   ├── 💳 PaymentService.cs — Stripe payment integration
│   │   ├── 🍽️ ProductService.cs — Product catalog operations
│   │   └── 🔌 ServiceManager.cs — Facade pattern for service coordination
│   │
│   └── 📑 ServiceAbstraction/ — Service interfaces
│       ├── 🔐 IAuthenticationService.cs
│       ├── 🛒 IBasketService.cs
│       ├── 📦 IOrderService.cs
│       ├── 💳 IPaymentService.cs
│       ├── 🍽️ IProductService.cs
│       └── 🔌 IServiceManager.cs
│
├── 🏢 Infrastructre/
│   ├── 💾 Persistence/ — Data access layer
│   │   ├── 🗄️ Data/ — DbContext and Entity Framework configuration
│   │   ├── 👤 Identity/ — ASP.NET Core Identity setup and user management
│   │   ├── 🔄 Repositories/ — Repository pattern implementations
│   │   ├── 🌱 DataSeeding.cs — Initial database seed data
│   │   └── 📊 SpecificationEvaluator.cs — Applies specifications to queries
│   │
│   └── 🎨 Presentation/ — API layer
│       └── 🎛️ Controllers/ — API endpoints for Account, Basket, Order, Products, Payments
│
├── 📦 Shared/ — Shared models and utilities
│   ├── 💬 DataTransferObjects/ — DTOs for request/response
│   ├── ❌ ErrorModels/ — Error response models
│   ├── 📮 OrderModels/ — Order-related DTOs
│   ├── 🔐 JwtOptions.cs — JWT configuration
│   ├── 📄 PaginatedResult.cs — Generic pagination wrapper
│   ├── 🔎 ProductQueryParams.cs — Product filtering parameters
│   └── 📊 ProductSortingOptions.cs — Sorting options for products
│
├── 🌐 E-commerce.Web/ — Main API project
│   ├── 🛠️ CustomMiddleWares/ — Custom middleware (error handling, logging)
│   ├── ⚡ Extensions/ — Extension methods for service registration
│   ├── 🏭 Factories/ — Factory pattern implementations
│   ├── 🔧 Properties/ — Project settings and configurations
│   ├── 🌍 wwwroot/ — Static files
│   └── ⚙️ Program.cs — Application startup configuration
│
├── 📋 E-commerce.Web.sln — Solution file
├── 📖 README.md — Project documentation
└── 🚫 .gitignore — Git ignore rules
```

---
## 🔑 Key Concepts

### 🔐 JWT (JSON Web Tokens)
Authentication is handled using JWT Bearer tokens. Upon a successful login, the server issues a signed token containing the user's claims and roles. This token must be included in the `Authorization` header of every protected request. It allows stateless authentication without the need to store session data on the server.

---

### 📦 Redis (In-Memory Database)
Redis is used as a fast in-memory store for managing the **user's basket**. Instead of persisting basket data in SQL Server, basket items are stored in Redis as key-value pairs, which allows quick access and automatic expiration. This reduces unnecessary load on the main database for temporary data.

---

### 📐 Specification Pattern
The Specification Pattern is used to encapsulate query logic into dedicated, reusable specification classes. Rather than writing filtering, sorting, and pagination logic directly in repositories, each query is wrapped in its own specification. This keeps the codebase clean, testable, and easy to extend without modifying the core repository logic.

---
## 🚀 Getting Started

### Prerequisites
- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB or full SQL Server)
- **Visual Studio 2022** or **Visual Studio Code** (recommended)
- **Git** for version control

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/mahmoudalsharkawy572/OnlineFoodOrder-API.git
   cd OnlineFoodOrder-API
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   
   Edit `E-commerce.appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True",
       "DefaultConnection": "Server=.;Database=ECommerceIdenitit;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
     📌 **Note:** I Used two databases one for Data and one for identity with different Contexts.
     📌 **Note:** You must add migrations and update data base before running!  
   
   ```

4. **Configure Stripe (Optional)**
   
   For payment functionality, add your Stripe keys to `appsettings.json`:
   ```json
   {
     "stripe": {
       "PublishableKey": "your_publishable_key_here",
       "secretKey": "your_secret_key_here"
     }
   }
   ```

### 🔑 Default Admin Account

The application will automatically create a default admin account on first run:
- **Email**: admin@gmail.com
- **Password**: Admin123*
- **Role**: Administrator

## 🔧 Configuration

### Database Configuration
- The application uses Entity Framework Core with SQL Server
- Connection strings are configured in `appsettings.json`
- Database initialization and seeding happen automatically on startup

### Authentication Configuration
- ASP.NET Core Identity is configured for user management
- Password requirements and lockout policies can be adjusted in `Program.cs`

### Payment Configuration
- Stripe integration requires valid API keys
- Test keys are included for development purposes
- Production deployment requires live Stripe keys

---
## 🏗️ Architecture

This API is built using **Onion Architecture**, which organizes the codebase into concentric layers where dependencies always point inward — keeping the core business logic completely independent from external concerns like databases or frameworks.

---

### 🔄 Request Flow

```
Client Request
    ↓
Controller  (API Layer)
    ↓
Service / Repository  (Infrastructure Layer)
    ↓
Specification  (Core Layer — builds the query)
    ↓
EF Core / Redis  (Database)
    ↓
Response back to Client
```

---

> 💡 **Why Onion Architecture?**  
> It ensures the **Core layer has zero dependencies** on frameworks or databases, making the business logic easy to test, maintain, and scale independently.

---
## 🧩 Design Patterns

### 📁 Repository Pattern
Abstracts the data access layer by providing a clean interface between the business logic and the database. Instead of writing EF Core queries directly in controllers or services, all database operations go through repository interfaces defined in the **Core layer** and implemented in the **Infrastructure layer**.

```
IGenericRepository<T>  →  Defined in Core
GenericRepository<T>   →  Implemented in Infrastructure
```

---

### 🔄 Unit of Work Pattern
Coordinates multiple repository operations under a **single transaction**. Instead of saving changes in each repository separately, the Unit of Work ensures that all changes are committed together — or rolled back if something fails — maintaining data consistency.

```
IUnitOfWork   →  Defined in Core
UnitOfWork    →  Wraps DbContext, manages all repositories
```

---

### 📐 Specification Pattern
Encapsulates query logic (filtering, sorting, pagination, includes) into dedicated **Specification classes** instead of polluting repositories with complex queries. Each use case has its own specification that the generic repository evaluates against EF Core.

```
BaseSpecification<T>         →  Base class with criteria, includes, ordering
ProductWithFiltersSpec       →  Example: filter by brand, type, sort, paginate
OrdersForUserWithItemsSpec   →  Example: get orders with related items for a user
```

---


## 👨‍💻 Developer

**Mahmoud Alsharkawy**
- GitHub: [@mahmoudalsharkawy572](https://github.com/mahmoudalsharkawy572)
```
**Happy coding! 🚀**
