# Sales API Documentation

## Table of Contents
1. [Overview](#overview)
2. [How to Run](#how-to-run)
3. [Technologies Used](#technologies-used)
4. [Key Features](#key-features)
5. [Architecture](#architecture)
6. [Endpoints](#endpoints)
7. [Testing](#testing)

---

## Overview

The **Sales API** is a robust, scalable solution designed for managing product sales. Built using the **Clean Architecture** approach, it ensures a clear separation of concerns across multiple layers:

- **API Layer**: Handles HTTP requests and responses, acting as the entry point for external interactions.
- **Application Layer**: Implements business logic using **CQRS** and **MediatR**, facilitating separation between read and write operations.
- **Infrastructure Layer**: Manages database interactions, external services, and other cross-cutting concerns.
- **Domain Layer**: Encapsulates core business rules and domain models.
- **Tests Layer**: Provides unit and integration tests to ensure the system's reliability and correctness.

---

## How to Run

### Prerequisites
Before running the application, ensure you have the following installed:
- **Docker**: Required for containerized deployment ([Download Docker](https://www.docker.com/get-started)).
- **.NET 8 SDK** (optional for local development outside of Docker).

---

### **Steps to Run** 🚀  

1. **Clone the repository**:  
   ```bash
   git clone https://github.com/TiagoEsdras/SalesApi.git
   ```

2. **Navigate to the project directory**:  
   ```bash
   cd SalesApi
   ```
3. **Ensure the external Docker network exists**:  
   The application requires the `evaluation-network` Docker network. Check if it already exists:  
   ```bash
   docker network ls
   ```  
   If **it does not exist**, create it manually:  
   ```bash
   docker network create evaluation-network
   ```
4. **Build and start the application** using Docker:  
   ```bash
   docker-compose up --build
   ```  

Once the containers are up and running, your application will be ready to use! 🚀🔥

Agora a aplicação estará rodando e pronta para uso! 🚀🔥

5. **Wait for the services to initialize**. The following services will be available:
   - **Sales API**: [http://localhost:8081](http://localhost:8081)
   - **API Gateway**: [http://localhost:7777](http://localhost:7777)
   - **Database (MySQL)**: Available at `localhost:3307`
   - **RabbitMQ**: [http://localhost:15672](http://localhost:15672)

Use **Postman** or **Swagger UI** to interact with the API.

---

## Technologies Used

### Core Platform
- **.NET 8**: High-performance framework for scalable applications.
- **Entity Framework Core 8**: ORM for database interactions.
- **MySQL**: Relational database for persistent storage.
- **Docker**: Containerization for environment consistency.

### Libraries & Tools
- **FluentValidation**: Ensures model validation.
- **AutoMapper**: Simplifies object-to-object mapping.
- **MediatR**: Implements the **CQRS** pattern.
- **Swashbuckle.AspNetCore**: Generates **Swagger** API documentation.
- **RabbitMQ**: Implements asynchronous messaging.
- **Ocelot**: API Gateway for routing and load balancing.

### Testing Frameworks
- **xUnit**: Unit testing framework.
- **FluentAssertions**: Improves test assertions readability.
- **Moq**: Mocks dependencies in unit tests.
- **Bogus**: Generates realistic fake data for testing.

---

## Key Features

### Product Management
- **Create a product**
- **Retrieve a product by ID**
- **List all products**

### Sale Management
- **Create a new sale**
- **Retrieve a sale by ID**
- **List all sales**
- **Cancel a sale**

---

## Architecture

### Clean Architecture Principles
- **Separation of Concerns**: Layers have distinct responsibilities.
- **Dependency Inversion**: Infrastructure depends on domain logic, not vice versa.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations.

### Controllers
- **ProductsController**: Manages product-related operations.
- **SalesController**: Handles sale transactions, retrieval, and cancellations.

---

## Endpoints

### Product Endpoints
- **`POST /api/products`** - Create a new product.
- **`GET /api/products/{id}`** - Retrieve product by ID.
- **`GET /api/products`** - List all products.

### Sales Endpoints
- **`POST /api/sales`** - Create a new sale.
- **`GET /api/sales/{id}`** - Retrieve sale by ID.
- **`GET /api/sales`** - List all sales.
- **`DELETE /api/sales/{id}`** - Cancel a sale.
---

## Testing

### Test Coverage
- **Handlers**: Ensures MediatR handlers function as expected.
- **Validators**: Confirms request models meet business rules.
- **Repositories**: Verifies database interactions.

Run tests with:
```bash
   cd src
   dotnet test
```

