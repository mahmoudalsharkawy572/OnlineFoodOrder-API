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

---

## 📁 Project Structure

The project follows **Onion Architecture** pattern with clearly separated layers:

```
OnlineFoodOrder-API/
│
├── 📦 Core/
│   ├── 🎯 Core.Entities/                    # Core domain entities (Business Objects)
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   ├── Basket.cs
│   │   ├── User.cs
│   │   ├── Address.cs
│   │   ├── DeliveryMethod.cs
│   │   └── OrderItem.cs
│   │
│   └── 🏗️ Core.Specifications/              # Specification pattern for filtering
│       ├── BaseSpecification.cs
│       ├── ProductSpecification.cs
│       ├── OrderSpecification.cs
│       └── ProductsWithBrandsAndTypesSpecification.cs
│
├── 📦 Application/
│   ├── 📋 Interfaces/                       # Repository & Service Abstractions
│   │   ├── IGenericRepository.cs
│   │   ├── IProductRepository.cs
│   │   ├── IOrderRepository.cs
│   │   ├── IBasketRepository.cs
│   │   ├── IUnitOfWork.cs
│   │   └── IPaymentService.cs
│   │
│   ├── 🔧 DTOs/                             # Data Transfer Objects
│   │   ├── ProductDTO.cs
│   │   ├── OrderDTO.cs
│   │   ├── BasketDTO.cs
│   │   ├── AddressDTO.cs
│   │   └── UserDTO.cs
│   │
│   └── 📊 Services/                         # Application Services
│       ├── PaymentService.cs
│       ├── OrderService.cs
│       ├── BasketService.cs
│       └── AuthenticationService.cs
│
├── 📦 Infrastructure/
│   ├── 💾 Data/                             # Database Layer
│   │   ├── Context/
│   │   │   └── AppDbContext.cs
│   │   ├── Configurations/                  # EF Core Configurations
│   │   │   ├── ProductConfiguration.cs
│   │   │   ├── OrderConfiguration.cs
│   │   │   └── BasketConfiguration.cs
│   │   └── Migrations/
│   │       └── [Generated Migrations]
│   │
│   ├── 🗄️ Repositories/                     # Repository Implementations
│   │   ├── GenericRepository.cs
│   │   ├── ProductRepository.cs
│   │   ├── OrderRepository.cs
│   │   ├── BasketRepository.cs
│   │   └── UnitOfWork.cs
│   │
│   ├── 🔴 Redis/                            # Cache Implementation
│   │   ├── BasketRepository.cs              # Redis Basket Cache
│   │   └── CacheService.cs
│   │
│   ├── 💳 ExternalServices/                 # Stripe Integration
│   │   ├── StripeService.cs
│   │   └── PaymentWebhookHandler.cs
│   │
│   └── 🔐 Identity/                         # User Authentication
│       ├── AppUser.cs                       # Extended Identity User
│       ├── Roles/
│       │   └── [Role Configurations]
│       └── TokenService.cs                  # JWT Token Generation
│
├── 📦 API/
│   ├── 🎮 Controllers/                      # API Endpoints
│   │   ├── AccountController.cs
│   │   ├── ProductsController.cs
│   │   ├── OrdersController.cs
│   │   ├── BasketController.cs
│   │   ├── PaymentsController.cs
│   │   └── DeliveryMethodsController.cs
│   │
│   ├── ⚙️ Middleware/                       # Custom Middleware
│   │   ├── ExceptionMiddleware.cs
│   │   └── ErrorHandlingMiddleware.cs
│   │
│   ├── 📝 Extensions/                       # DI Setup & Extensions
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── MiddlewareExtensions.cs
│   │
│   ├── 🔍 Helpers/                          # Utility & Helper Classes
│   │   ├── MappingProfiles.cs               # AutoMapper Configurations
│   │   ├── ValidationHelpers.cs
│   │   └── PaginationHelper.cs
│   │
│   ├── 📋 Requests/                         # Request Models
│   │   ├── CreateOrderRequest.cs
│   │   ├── UpdateAddressRequest.cs
│   │   └── RegisterRequest.cs
│   │
│   ├── 📤 Responses/                        # API Response Models
│   │   ├── ApiResponse.cs
│   │   ├── PaginatedResponse.cs
│   │   └── ErrorResponse.cs
│   │
│   └── Program.cs                           # ASP.NET Core Entry Point
│
└── 📄 Configuration Files
    ├── appsettings.json                     # App Configuration
    ├── appsettings.Development.json         # Dev Settings
    └── .env                                 # Environment Variables (Stripe keys, etc.)
```

### 🏛️ Layer Responsibilities

| Layer | Purpose | Key Components |
|-------|---------|-----------------|
| **Core.Entities** | Domain models with business logic | Entities, Value Objects |
| **Core.Specifications** | Query specifications | Specification classes for filtering |
| **Application** | Interfaces, DTOs, and business logic | Repositories interfaces, Services, DTOs |
| **Infrastructure** | Database, caching, external services | EF Core, Repositories, Redis, Stripe |
| **API** | HTTP endpoints and request handling | Controllers, Middleware, AutoMapper |

---

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
