# EventEase - Venue Booking System

# 📋 Project Overview

## 📑 Table of Contents
- [Project Overview](#-project-overview)
- [Architecture](#-architecture)
- [Features](#-features)
- [Database Schema](#-database-schema)
- [Setup Instructions](#-setup-instructions)
- [Project Structure](#-project-structure)
- [API Endpoints](#-api-endpoints)
- [Validation Rules](#-validation-rules)
- [Usage Guide](#-usage-guide)
- [Troubleshooting](#-troubleshooting)
- [Future Enhancements](#-future-enhancements)
- [Support](#-support)
- [License](#-license)


## EventEase is a comprehensive venue booking and event management system built with ASP.NET Core MVC. It allows booking specialists to manage venues, events, clients, and bookings through an intuitive web interface.

# 🏗️ Architecture

Frontend: ASP.NET Core MVC with Razor Views
Backend: .NET 8 with Entity Framework Core
Database: Azure SQL Database
Authentication: Azure Active Directory (for production)
Deployment: Azure App Service

# 🚀 Features

Venue Management: Create, edit, and manage venues with image uploads
Event Management: Schedule events with start and end dates
Client Management: Manage client information
Booking System: Prevent double bookings and manage reservations
Dashboard: Overview of system statistics
Responsive Design: Works on desktop and mobile devices

# 📊 Database Schema

## Tables Structure

### 1. Clients Table

CREATE TABLE Clients (
    ClientId INT PRIMARY KEY IDENTITY(1,1),
    ClientName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL
);

### 2. Venues Table

CREATE TABLE Venues (
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl NVARCHAR(500) NULL
);

### 3. Events Table

CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    StartDate DATETIME2 NOT NULL,
    EndDate DATETIME2 NOT NULL,
    Description NVARCHAR(500) NULL
);

### 4. Bookings Table

CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    EventId INT NOT NULL FOREIGN KEY REFERENCES Events(EventId),
    VenueId INT NOT NULL FOREIGN KEY REFERENCES Venues(VenueId),
    ClientId INT NOT NULL FOREIGN KEY REFERENCES Clients(ClientId),
    BookingDate DATETIME2 NOT NULL DEFAULT GETDATE()
);

### 5. Migration History Table

CREATE TABLE [__EFMigrationsHistory] (
    [MigrationId] nvarchar(150) NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
);

## 🗃️ Sample Data

### Insert Sample Clients

INSERT INTO Clients (ClientName, Email, PhoneNumber) VALUES
('John Smith', 'john.smith@email.com', '555-0123'),
('Sarah Johnson', 'sarah.j@email.com', '555-0456'),
('Mike Wilson', 'mike.w@email.com', '555-0789'),
('Emily Davis', 'emily.d@email.com', '555-0912'),
('David Brown', 'david.b@email.com', '555-0345');

### Insert Sample Venues

INSERT INTO Venues (VenueName, Location, Capacity, ImageUrl) VALUES
('Grand Ballroom', '123 Main St, City Center', 500, 'https://via.placeholder.com/600x400?text=Grand+Ballroom'),
('Conference Center', '456 Oak Ave, Business District', 200, 'https://via.placeholder.com/600x400?text=Conference+Center'),
('Garden Pavilion', '789 Park Rd, Green Area', 150, 'https://via.placeholder.com/600x400?text=Garden+Pavilion'),
('Sports Arena', '321 Stadium Dr, Sports Complex', 1000, 'https://via.placeholder.com/600x400?text=Sports+Arena'),
('Hotel Convention Hall', '654 Resort Blvd, Hospitality Zone', 300, 'https://via.placeholder.com/600x400?text=Hotel+Hall');

### Insert Sample Events

INSERT INTO Events (EventName, StartDate, EndDate, Description) VALUES
('Tech Conference 2023', DATEADD(day, 5, GETDATE()), DATEADD(day, 7, GETDATE()), 'Annual technology conference featuring latest innovations'),
('Wedding Expo', DATEADD(day, 10, GETDATE()), DATEADD(day, 10, GETDATE()), 'Showcase of wedding services and vendors'),
('Corporate Gala', DATEADD(day, 15, GETDATE()), DATEADD(day, 15, GETDATE()), 'Year-end corporate celebration event'),
('Music Festival', DATEADD(day, 20, GETDATE()), DATEADD(day, 22, GETDATE()), '3-day outdoor music festival'),
('Business Workshop', DATEADD(day, 25, GETDATE()), DATEADD(day, 25, GETDATE()), 'Full-day business development workshop');

### Insert Sample Bookings

INSERT INTO Bookings (EventId, VenueId, ClientId, BookingDate) VALUES
(1, 1, 1, DATEADD(day, -10, GETDATE())),
(2, 2, 2, DATEADD(day, -8, GETDATE())),
(3, 3, 3, DATEADD(day, -5, GETDATE())),
(4, 4, 4, DATEADD(day, -3, GETDATE())),
(5, 5, 5, DATEADD(day, -1, GETDATE()));

## 🛠️ Setup Instructions

### Prerequisites

.NET 8 SDK
SQL Server (LocalDB or Azure SQL)
Visual Studio 2022 or VS Code
Azure account (for deployment)
Local Development Setup

Clone the repository
bash
git clone <repository-url>
cd EventEase
Restore dependencies
bash
dotnet restore
Configure database connection
Update appsettings.json with your local SQL connection string
For LocalDB: "Server=(localdb)\\mssqllocaldb;Database=EventEaseDB;Trusted_Connection=true;"
Apply database migrations
bash
dotnet ef database update
Run the application
bash
dotnet run
or
bash
dotnet watch run
Azure Deployment

Create Azure Resources
bash
## Create resource group
az group create --name EventEase-RG --location eastus

## Create App Service plan
az appservice plan create --name EventEasePlan --resource-group EventEase-RG --sku B1 --is-linux

## Create Web App
az webapp create --resource-group EventEase-RG --plan EventEasePlan --name eventease-app --runtime "DOTNET:8"

## Create SQL Database
az sql server create --location eastus --resource-group EventEase-RG --name eventease-sql --admin-user adminuser --admin-password StrongPassword123!

## Configure firewall
az sql server firewall-rule create --resource-group EventEase-RG --server eventease-sql --name AllowAzure --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

Deploy from Visual Studio
Right-click project → Publish
Select Azure → Azure App Service
Choose existing or create new App Service
Click Publish
Configure Application Settings in Azure
Go to Azure Portal → Your Web App → Configuration
Add connection string:

Name: DefaultConnection
Value: Server=tcp:eventease-sql.database.windows.net,1433;Initial Catalog=EventEaseDB;Persist Security Info=False;User ID=adminuser;Password=StrongPassword123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

## 📁 Project Structure

EventEase/
├── Controllers/
│   ├── HomeController.cs
│   ├── VenuesController.cs
│   ├── EventsController.cs
│   ├── ClientsController.cs
│   ├── BookingsController.cs
│   └── DiagnosticsController.cs
├── Models/
│   ├── ApplicationDbContext.cs
│   ├── Venue.cs
│   ├── Event.cs
│   ├── Client.cs
│   └── Booking.cs
├── Views/
│   ├── Home/
│   ├── Venues/
│   ├── Events/
│   ├── Clients/
│   ├── Bookings/
│   └── Shared/
├── Migrations/
├── wwwroot/
│   └── uploads/
│       └── venues/
└── Properties/
    └── launchSettings.json
    
## 🔧 API Endpoints

Endpoint	Method	Description
/	GET	Homepage with dashboard
/Venues	GET	List all venues
/Venues/Create	GET/POST	Create new venue
/Events	GET	List all events
/Events/Create	GET/POST	Create new event
/Clients	GET	List all clients
/Bookings	GET	List all bookings
/Diagnostics	GET	System health check

## 🚦 Validation Rules

Venues: Name and location required, capacity must be positive
Events: Name required, end date must be after start date
Clients: Name, email, and phone number required
Bookings: Prevent double bookings for same venue and event

## 📝 Usage Guide

Add Venues: Navigate to Venues → Create to add new venues
Create Events: Go to Events → Create to schedule events
Manage Clients: Use Clients section to add client information
Make Bookings: Book venues for specific events through Bookings
View Dashboard: Homepage shows system statistics and quick access

## 🐛 Troubleshooting

Common Issues:

Database Connection Errors
Check connection string in appsettings.json
Verify SQL Server is running
Check firewall settings for Azure SQL
Migration Issues
bash
## Remove old migrations
dotnet ef migrations remove

## Add new migration
dotnet ef migrations add InitialCreate

## Update database
dotnet ef database update
Image Upload Issues
Check wwwroot/uploads/venues folder exists
Verify write permissions on server
Diagnostic Tools:

Access /Diagnostics for system health check
Check Azure Application Insights for errors
Review server logs in Azure Portal

## 📈 Future Enhancements

User authentication and authorization
Email notifications for bookings
Calendar view for events
Payment integration
API for mobile applications
Advanced reporting and analytics
Multi-language support
Real-time availability updates

## 📞 Support

For issues and questions:

Check the diagnostics page at /Diagnostics
Review Azure application logs
Check database connectivity using the provided SQL scripts
Contact development team for assistance

## 📄 License

This project is proprietary software developed for EventEase. All rights reserved.

Last Updated: December 2023
Version: 1.0.0
Environment: Production Ready
