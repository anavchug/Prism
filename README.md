# Prism

A full-stack e-commerce web app built with .NET + Blazor, featuring authentication/authorization, product catalog, shopping cart, and order management.

<!-- BADGES (optional)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)]()
[![Build](https://img.shields.io/github/actions/workflow/status/anavchug/Prism/build.yaml?label=build)]()
-->

## Table of Contents
- [Demo](#demo)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Screenshots](#screenshots)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Local Setup](#local-setup)
  - [Environment Variables](#environment-variables)
  - [Database & Migrations](#database--migrations)
  - [Seeding Data (optional)](#seeding-data-optional)
- [How It Works](#how-it-works)
- [Roadmap / Future Ideas](#roadmap--future-ideas)
- [Contributing](#contributing)
- [License](#license)

---

## Demo

[Demo]([docs/demo.gif](https://drive.google.com/file/d/11GJdx9GYTxnKm-uYCsG7qkUB8RLwsYEB/view?usp=drive_link)) 

---

## Features
- 🔐 **Auth & Roles**: Register/Login via ASP.NET Identity; role-based views (Admin & Customer).
- 🛍️ **Catalog**: Product listing, detail pages, categories.
- 🧺 **Cart & Checkout**: Add/remove/update quantities; order placement flow.
- 📦 **Orders**: Order history & order detail pages.
- 🖼️ **Image Uploads**: Product images with server-side validation.
- 🛠️ **Admin Portal**: Manage products, categories, pricing, and inventory.
- 📱 **Responsive UI**: Bootstrap-based, mobile friendly.

---

## Tech Stack
**Frontend**: Blazor (Server) + Bootstrap  
**Backend**: ASP.NET Core (.NET 8) + C#  
**Data**: Entity Framework Core + SQL Server (LocalDB/Express/Azure SQL)  
**Auth**: ASP.NET Core Identity (roles: Admin, Customer)  
**Tooling**: EF Core Migrations, .NET CLI / Visual Studio

---

## Screenshots

<img width="500" height="500" alt="1" src="https://github.com/user-attachments/assets/bfca6bf2-75ca-4216-a4f1-530e91ed7f9a" />
<img width="500" height="500" alt="2" src="https://github.com/user-attachments/assets/231ef427-35de-4a6c-96a3-69d27571497a" />
<img width="500" height="500" alt="3" src="https://github.com/user-attachments/assets/0ff80d94-3732-4bf1-8f19-8e13210925e3" />
<img width="500" height="500" alt="4" src="https://github.com/user-attachments/assets/d94c8d06-675a-4985-930d-891958dd8daa" />
<img width="500" height="500" alt="5" src="https://github.com/user-attachments/assets/e86bb171-c36c-4604-9de6-79f9e59d1dc3" />
<img width="500" height="500" alt="6" src="https://github.com/user-attachments/assets/ee47857b-aeb3-4698-8667-0c886a15fb80" />
<img width="500" height="500" alt="7" src="https://github.com/user-attachments/assets/716c0a61-018c-48fc-8680-29d2e7dee9e5" />
<img width="500" height="500" alt="8" src="https://github.com/user-attachments/assets/1f2d564a-4e22-46e5-84d8-7027e4faf605" />
<img width="500" height="500" alt="9" src="https://github.com/user-attachments/assets/4ba7ba2f-a8bb-42ca-8bd5-fd81e75b9431" />

---

## Architecture
High-level flow:
1. **Blazor Server** renders interactive components over SignalR.
2. **Application layer** coordinates domain models (Products, Orders, Categories, Cart, etc.).
3. **EF Core** persists data to SQL Server via repositories/DbContext.
4. **ASP.NET Identity** handles users/roles and protected routes.

<!-- PLACEHOLDER: Architecture Diagram -->
<!-- ![Architecture](docs/architecture/diagram.png) -->

---

## Getting Started

### Prerequisites
- **.NET SDK 8.0+**
- **SQL Server** (LocalDB/Express are fine)
- (Optional) **Visual Studio 2022/2025** with ASP.NET workload
- (Optional) **dotnet-ef** CLI:
  ```bash
  dotnet tool install --global dotnet-ef

---
  ### Local Setup
  1. **Clone**
       ```bash
       git clone https://github.com/anavchug/Prism.git
       cd Prism
---
2. **Open & Restore**

- **Open Prism.sln in Visual Studio or run:**
   ```bash
    dotnet restore

---
3. **Configure DB Connection**
- In appsettings.Development.json, set your connection string, e.g.:
  ```bash
    {
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PrismDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
  }
  
  
### Environment Variables
```bash
# From the project directory containing the .csproj:
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=PrismDb;..."
```
---

### Database & Migrations
- If the database hasn’t been created:
  ```bash
    # From the project directory containing the .csproj:
    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=PrismDb;..."
  ```

- If you’ve added new columns after an earlier migration (e.g., Price on OrderDetail):
 ```bash
dotnet ef migrations add AddPriceToOrderDetail
dotnet ef database update
```
---
### Seeding Data (optional)

You can seed basic data (roles, admin user, categories, sample products).
Add or update a seed routine (e.g., in Program.cs) and run the app once. Example outline:

```bash
await RoleManager.CreateAsync(new IdentityRole("Admin"));
await RoleManager.CreateAsync(new IdentityRole("Customer"));
```

### How It Works

**Authentication & Authorization**

- ASP.NET Identity manages users, passwords, roles.
- UI elements are conditionally rendered depending on role (Admin vs Customer).

**Catalog & Cart**

- Products and categories are EF entities.
- Cart state is tracked per user; add/remove/update count flows through a Cart service.

**Orders**

- On checkout, an OrderHeader and related OrderDetail rows are created from the cart.
- Users can view past orders; admins can manage order status.

**Images**

- Images are uploaded to wwwroot/images/....
- Server-side validation ensures file size/format.

### Roadmap / Future Ideas

- 🔎 Full-text search with filters.
- 💳 Payment integration (Stripe).
- 🧾 Invoice/PDF generation.
- 🧠 Recommendations (related products).
- 🧮 Discounts & coupons.
- 📈 Admin analytics.
- 🌐 Localization.
- 🧪 Automated testing (xUnit, bUnit).

### Contributing
- PRs welcome!
- Fork the repo
- Create a branch: git checkout -b feat/cool-thing
- Commit: git commit -m "feat: add cool thing"
- Push: git push origin feat/cool-thing
- Open a PR

### License

This project is licensed under the MIT License. See LICENSE for details.

### Credits / Acknowledgements

- Built by Anav Chug.
- Thanks to the .NET & Blazor community and all OSS dependencies.
