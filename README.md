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
