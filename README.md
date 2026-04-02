# 🖥️ Byte&Buy

**Byte&Buy** is a modern C2C (Client-to-Client) marketplace for IT-related items, combining a web platform for users and a desktop application for company operations. Users can buy, sell, and rent items, while employees manage the marketplace through a dedicated admin panel.


##  Features

### 🌐 Web Application (User-Facing)
- Register & login
- Create & manage sales/rental offers
- Cart, checkout, and order management
- Returns & cancellations (14-day policy)
- Returns of already rented items
- PDF order confirmations
- Browse, filter, and paginate offers

### 🧑‍💼 Desktop Application (Admin/Employees)
- Employee login & role management
- Dashboard with KPI & GMV indicators
- Company & user administration
- Inventory and store management
- Overall shop management such categories, deliveries etc.
- Manage orders & rentals
- PDF confirmations & shipment handling
- Filtering & pagination for large datasets

### ⚙️ API / Backend
- Built with **ASP.NET Core** & **Clean Architecture**
- Built using Domain-Driven-Design Principles
- PostgreSQL + Entity Framework Core
- RESTful API with JWT authentication
- Role-based permissions & user authorization
- Automated tasks (Hangfire)
- PDF generation (QuestPDF)


## 🧰 Technologies

- **Backend:** ASP.NET Core, Entity Framework Core, PostgreSQL  
- **Web:** Angular, Bootstrap, SweetAlert2, ngx-toastr, Font Awesome  
- **Desktop:** Avalonia, MVVM, CommunityToolkit.MVVM, LiveChartsCore  
- **Utilities:** QuestPDF, Hangfire  

## 🏗️ Architecture
- Clean Architecture with Domain-Driven Design (DDD)
- Testable and scalable
- Independent development of components

## 🚀 Quick Start
1. Clone the repository
   - `git clone https://github.com/TheCrudClapper/Byte-Buy-Eccomerce.git`
   - Then `cd ByteBuy`
3. Setup the backend
   - Configure PostgreSQL connection
   - Run migrations
   - Start API Project
5. Launch web application
   - Navigate to Angular project `Byte&Buy.AngularSPA`
   - Open project and install depedencies via `npm install`
   - Run `ng serve` in the console
   - App will be avaliable at `http://localhost:4200`
6. Launch desktop application
   - Open in Avalonia-supported IDE such [VisualStudio](https://visualstudio.microsoft.com/pl/) or [JetBrains Rider](https://www.jetbrains.com/rider/)
   - Set `ByteBuy.Desktop` as startup project
   - Build and run

## Plans for future
1. Favourites system
2. Rating of sellers, offers
3. Messaging system
4. Stripe integration
5. Third-party delivery API's integration

## Useful Links
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [PostgreSQL](https://www.postgresql.org)
- [Angular](https://angular.io)
- [Bootstrap](https://getbootstrap.com)
- [Avalonia](https://avaloniaui.net)
- [CommunityToolkit.MVVM](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm)
- [LiveChartsCore.SkiaSharpView.Avalonia](https://livecharts.dev)
- [QuestPDF](https://www.questpdf.com)
- [Hangfire](https://www.hangfire.io)
- [SweetAlert2](https://sweetalert2.github.io)
- [ngx-toastr](https://www.npmjs.com/package/ngx-toastr)
- [Font Awesome](https://fontawesome.com)
