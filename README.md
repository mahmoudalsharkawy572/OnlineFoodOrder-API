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
