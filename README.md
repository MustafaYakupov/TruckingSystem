# Trucking System

## Overview

The **Trucking System** is a web application created as a final project for the ASP.NET Advanced course in SoftUni. Developed with **ASP.NET Core 8**, **Entity Framework Core 8**, and **Razor Pages** using the **MVC (Model-View-Controller)** architecture. This application is designed to help manage and track trucking operations, including the management of loads, drivers, trucks, trailers, and broker companies.

## Features

### User Roles:
There are two roles within the application:

- **Admin**: Admins have full access to the system and can manage all entities, including drivers, trucks, trailers, and loads.
- **User**: Users can interact with loads, assign drivers to available loads, and mark loads as completed, but they cannot modify drivers, trucks, or trailers.

### Admin Features:
- **Manage Drivers**: Admins can create, edit, and delete drivers.
- **Manage Trucks**: Admins can create, edit, and delete trucks.
- **Manage Trailers**: Admins can create, edit, and delete trailers.
- **Assign Driver to Load**: Admins can assign a driver to an available load and mark the load as "In Progress".
  
**Admin Login Credentials:**
- Email: `admin@admin.com`
- Password: `123qwe`

### User Features:
- **Assign Driver to Available Load**: Users can assign drivers to available loads by clicking the "Assign Driver" button. They can select a driver from a dropdown menu of available drivers.
- **Mark Loads as Completed**: Users can mark loads that are in progress as completed.

**User Login Credentials:**
- Email: `user@user.com`
- Password: `123qwe`

### Data Seeding:
Upon first launch, the application will seed the following data into the database from JSON files:
- **Broker Companies**
- **Driver Managers**
- **Drivers**
- **Loads**
- **Trucks**
- **Trailers**

**Note**: When the application is first launched, there will be no loads in progress or completed loads. The user can assign a driver to a load and mark the load as in progress using the "Assign Driver" button on the Available Loads page.

### Driver Availability:
In order for a driver to be available and be assigned on a load, they must have:
- An **assigned truck**
- An **assigned trailer**
- An **assigned driver manager**

As an **Admin**, you can assign a truck, trailer, and driver manager to a driver by going to the Driver page, clicking the "Edit" button, and selecting the appropriate options from existing trucks, trailers, and driver managers.

## How the Application Works

### Available Loads Page:
- **Users** will be able to see a list of available loads. Each load has an "Assign Driver" button.
- By clicking the "Assign Driver" button, users can select a driver from the dropdown list of available drivers. If a driver is not available, they won't show up in the dropdown until they have been assigned a truck, trailer, and driver manager.
- Once a driver is assigned, the load's status will change to **In Progress**.

### In Progress Loads Page:
- Users can view all loads that are currently in progress.
- Users can **mark loads as completed** by clicking the "Mark as Completed" button next to each load.

### Admin Panel:
- The **Admin** can view, create, edit, and delete drivers, trucks, and trailers.
- The **Admin** can also assign trucks, trailers, and driver managers to drivers, making them available for load assignments.
  
## Getting Started

### Prerequisites:
- **.NET 8 SDK**
- **SQL Server** or another compatible database provider for EF Core.

### Database Seeding:
When the application starts for the first time, the necessary data (Broker Companies, Driver Managers, Drivers, Trucks, Trailers, and Loads) can be seeded from the provided JSON files. You can find these files in the `/SeedData` folder.

## Login Credentials:
- **Admin Login**:
  - Email: `admin@admin.com`
  - Password: `123qwe`

- **User Login**:
  - Email: `user@user.com`
  - Password: `123qwe`

## MVC Structure:
The application follows the **MVC (Model-View-Controller)** design pattern:
- **Models**: Represents the data structure, including entities like `Load`, `Truck`, `Trailer`, `Driver`, etc.
- **Views**: Razor Views that handle the UI and user interaction.
- **Controllers**: Handles user input, interactions, and business logic.

## Technologies Used:
- **ASP.NET Core 8**: The framework for building web applications.
- **Entity Framework Core 8**: ORM (Object-Relational Mapping) for interacting with the database.
- **Razor Views**: For rendering dynamic web pages with C# code and Bootstrap.
- **SQL Server**: Default database provider used for data storage.

## License:
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
