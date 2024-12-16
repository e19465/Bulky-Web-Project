# Bulky Web - Project Documentation

## Overview

**Bulky Web** is a web application built using **ASP.NET Core 8 MVC**, **Entity Framework Core**, and styled with **Tailwind CSS**. The application supports **Role-based Authentication**, allowing users to have different levels of access based on their assigned roles. It includes key features such as user registration, authentication, role management, and dynamic content rendering. The project also implements **Repository Pattern** and follows **n-tier architecture** to separate concerns and ensure maintainability.

## Features

- **Role-based Authentication**: The application supports different roles like Admin, User, and Moderator, allowing personalized access and functionality.
- **ASP.NET Core 8 MVC**: The application follows the MVC architecture, separating concerns between the model, view, and controller.
- **Entity Framework Core**: The database is managed through Entity Framework Core, providing efficient database access and CRUD operations.
- **Tailwind CSS**: All styling is handled by Tailwind CSS, ensuring a responsive, modern, and customizable design.
- **Responsive Design**: The UI is designed to be responsive, ensuring compatibility across devices.
- **Repository Pattern**: The repository pattern is used to abstract the data access layer and provide a cleaner separation of concerns.
- **N-tier Architecture**: The project follows an n-tier architecture that divides the application into logical layers (Presentation, Business, Data) for better scalability, maintainability, and testability.

## Technologies Used

- **ASP.NET Core 8 MVC**
- **Entity Framework Core**
- **SQL Server**
- **Tailwind CSS** for modern and responsive styling
- **Role-based Authentication** for managing user permissions
- **Identity Framework** for managing users and roles
- **Repository Pattern** for abstracting data access
- **n-tier Architecture** for better separation of concerns and maintainability

## Project Setup

### Prerequisites

To run this project locally, you need the following tools:

- .NET 8 SDK or later
- SQL Server (or any supported database)
- Visual Studio or Visual Studio Code

### Installation Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/e19465/Bulky-Web-Project.git
