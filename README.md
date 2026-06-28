# Customer Support Ticket Management API

## Overview

The **Customer Support Ticket Management API** is a RESTful backend application developed using **ASP.NET Core 10**, **Entity Framework Core (Code First)**, and **SQL Server**. The system provides a centralized platform for managing customer support requests through secure authentication, role-based authorization, ticket management, and conversation tracking.

The API supports three user roles:

* **Admin** – Manages users, assigns tickets, monitors support operations, and views dashboard statistics.
* **Agent** – Handles assigned support tickets, updates ticket status, and responds to customer queries.
* **Customer** – Creates support tickets, views their own tickets, replies to conversations, and manages their profile.

---

## Features

### Authentication & Authorization

* JWT-based authentication
* ASP.NET Core Identity integration
* Role-based authorization (Admin, Agent, Customer)
* Secure password management

### User Management

* Customer registration
* Agent registration
* User login
* View profile
* Update profile

### Ticket Management

* Create support tickets
* View tickets
* Update ticket information
* Assign tickets to agents
* Update ticket status
* Close and reopen tickets
* Ticket ownership and role-based access control

### Ticket Responses

* Add replies to tickets
* View ticket conversation history
* Automatic response timestamps

### Dashboard

* Total tickets
* Open tickets
* In Progress tickets
* Resolved tickets
* Closed tickets
* Active agents

### Search & Filtering

* Search by ticket title
* Search by customer name
* Filter by status
* Filter by creation date
* Filter by assigned agent

### Security

* JWT Authentication
* Role-based authorization
* Input validation using Data Annotations
* Global Exception Handling Middleware
* Secure API endpoints

### API Documentation

* Swagger/OpenAPI support
* HTTP request collection for testing

---

## Technologies Used

* ASP.NET Core 10
* C#
* Entity Framework Core (Code First)
* SQL Server
* ASP.NET Core Identity
* JWT Authentication
* Swagger/OpenAPI

---

## Project Structure

```
Controllers/
Data/
DTOs/
Entities/
Enums/
Extensions/
Helpers/
Middleware/
Migrations/
Seed/
Services/
    ├── Interfaces/
    └── Implementations/
```

---

## Database

The project uses **Entity Framework Core Code First** with SQL Server.

Main tables include:

* AspNetUsers
* AspNetRoles
* AspNetUserRoles
* Tickets
* TicketReplies

---

## Ticket Status Workflow

```
Open
   ↓
In Progress
   ↓
Resolved
   ↓
Closed
```

---

## API Endpoints

### Authentication

| Method | Endpoint                      |
| ------ | ----------------------------- |
| POST   | `/api/auth/register-customer` |
| POST   | `/api/auth/register-agent`    |
| POST   | `/api/auth/login`             |

### User

| Method | Endpoint            |
| ------ | ------------------- |
| GET    | `/api/user/profile` |
| PUT    | `/api/user/profile` |

### Tickets

| Method | Endpoint              |
| ------ | --------------------- |
| POST   | `/api/tickets`        |
| GET    | `/api/tickets`        |
| GET    | `/api/tickets/{id}`   |
| PUT    | `/api/tickets/{id}`   |
| PUT    | `/api/tickets/assign` |
| PUT    | `/api/tickets/status` |
| GET    | `/api/tickets/search` |

### Replies

| Method | Endpoint                  |
| ------ | ------------------------- |
| POST   | `/api/replies`            |
| GET    | `/api/replies/{ticketId}` |

### Dashboard

| Method | Endpoint         |
| ------ | ---------------- |
| GET    | `/api/dashboard` |

---

## Setup Instructions

1. Clone the repository.

2. Update the SQL Server connection string in `appsettings.json`.

3. Apply Entity Framework migrations:

```bash
dotnet ef database update
```

4. Run the project:

```bash
dotnet run
```

5. Open Swagger to test the API.

---

## Default Admin Account

The application seeds a default administrator account.

**Email**

```
admin@support.com
```

**Password**

```
Admin@123
```

---

## Testing

The project includes:

* Swagger UI for API testing
* HTTP request collection (`Task2-Internship.http`)

---

## Author

**Talal Tariq**

Backend Web Development Internship – Task 2
