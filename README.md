# E-commerce System

## Overview

This project is an **e-commerce system** that manages products, customer shopping baskets, discount application, and order processing. The system is designed using **Domain-Driven Design (DDD)** principles and implements key patterns such as the **Repository Pattern**, **Domain Events**, and **Dependency Injection** to ensure scalability, maintainability, and flexibility.

## Features

- **Product Management**: Manage product details, stock levels, and pricing.
- **Basket Management**: Handle customer baskets, allowing items to be added, removed, and managed.
- **Discount Application**: Apply customer-specific discounts with rules like "5% off next purchase."
- **Order Processing**: Complete purchases, manage stock updates, and process customer orders.
- **Event-Driven Architecture**: Use **RabbitMQ** and **MassTransit** for event-driven communication.

## Technologies

- **.NET 8**: The application is built using **ASP.NET Core** for the API.
- **Entity Framework Core (EF Core)**: Used for data access and database management.
- **MassTransit**: Integrated with **RabbitMQ** to handle domain events and messaging.
- **RabbitMQ**: For event-driven communication between various services.
- **XUnit**: Unit and integration tests to ensure system functionality.
- **Moq**: Mocking framework used in unit tests for services and repositories.

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server or any database](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or InMemory provider for testing)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [Docker](https://www.docker.com/) (optional, if you want to run services like RabbitMQ and SQL in containers)

### Installation

1. **Clone the repository**:

    ```bash
    git clone https://github.com/paradisehuman/Shop.git
    cd shop
    ```

2. **Restore the .NET packages**:

    ```bash
    dotnet restore
    ```

3. **Set up the database**:

    - Update the `appsettings.json` or `appsettings.Development.json` with your SQL Server or database connection string.

    - Run the migrations to create the database:

    ```bash
    dotnet ef database update
    ```

4. **Run SQL Server using Docker** (optional):

    If you want to run SQL Server in a Docker container, you can use the following command to run **Azure SQL Edge**:

    ```bash
    docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MyPass@word" -p 1433:1433 --memory="2g" --cpus="2" -v sqlvolume:/var/opt/mssql -d --name=sql mcr.microsoft.com/azure-sql-edge
    ```

    This will start a SQL Server container using **Azure SQL Edge** with 2GB of memory and 2 CPUs, and store the data in a Docker volume named `sqlvolume`.

5. **Run RabbitMQ**:

    You can either run RabbitMQ locally or use Docker. Here’s a Docker command to run RabbitMQ:

    ```bash
    docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    ```

6. **Run the application**:

    ```bash
    dotnet run --project Shop/Shop.csproj
    ```

7. **Access the API**:

    The API will be running at:

    ```text
    http://localhost:5239
    ```

## Running Tests

To run the unit and integration tests:

```bash
dotnet test
```

This will run all tests, including integration tests that verify key functionality like product addition, basket management, and checkout flow.

### API Usage Guidance

Follow these steps to interact with the e-commerce system using the provided API endpoints. This guide will walk you through adding a product, creating a customer, creating a basket, adding items to the basket, and completing a purchase with a discount.

### 1. **Add Product**

To add a product, use the `/api/Product` endpoint with a `POST` request. Provide the product details such as picture, title, description, price, and stock.

**Endpoint:**

```
POST /api/Product
```

**Parameters (query):**

- `picture`: The product's picture link (string)
- `title`: The product's title (string)
- `description`: The product's description (string)
- `price`: The product's price (number, double)
- `stock`: The available stock for the product (integer, int32)

**Example Request:**

```bash
curl -X POST "http://localhost:5239/api/Product?picture=https://example.com/image.jpg&title=ProductTitle&description=Description&price=100&stock=10"
```

### 2. **Get the ProductId from the Database**

Once the product is added, retrieve the `ProductId` by querying the database directly.

### 3. **Create a Customer**

To create a new customer, use the `/api/Customer` endpoint with a `POST` request. Provide the customer’s first name as a parameter.

**Endpoint:**

```
POST /api/Customer
```

**Parameters (query):**

- `firstName`: The customer's first name (string)

**Example Request:**

```bash
curl -X POST "http://localhost:5239/api/Customer?firstName=John"
```

### 4. **Get the CustomerId from the Database**

After creating the customer, retrieve the `CustomerId` by querying the database.

### 5. **Create a Basket for the Customer**

To create a basket for the customer, use the `/api/Basket` endpoint with a `POST` request. Pass the `CustomerId` as a query parameter.

**Endpoint:**

```
POST /api/Basket
```

**Parameters (query):**

- `customerId`: The unique ID of the customer (UUID)

**Example Request:**

```bash
curl -X POST "http://localhost:5239/api/Basket?customerId=<CustomerId>"
```

### 6. **Get the BasketId from the Database**

After creating the basket, retrieve the `BasketId` from the database.

### 7. **Add BasketItem**

To add an item to the basket, use the `/api/Basket` endpoint with a `PUT` request. Provide the `BasketId`, `ProductId`, and `quantity` as query parameters.

**Endpoint:**

```
PUT /api/Basket
```

**Parameters (query):**

- `basketId`: The unique ID of the basket (UUID)
- `productId`: The unique ID of the product (UUID)
- `quantity`: The number of units of the product to add (integer, int32)

**Example Request:**

```bash
curl -X PUT "http://localhost:5239/api/Basket?basketId=<BasketId>&productId=<ProductId>&quantity=2"
```

### 8. **Checkout the Basket by BasketId and CustomerId**

To checkout the basket and complete the purchase, use the `/checkout` endpoint with a `POST` request. Provide the `BasketId` and `CustomerId` as query parameters.

**Endpoint:**

```
POST /checkout
```

**Parameters (query):**

- `basketId`: The unique ID of the basket (UUID)
- `customerId`: The unique ID of the customer (UUID)

**Example Request:**

```bash
curl -X POST "http://localhost:5239/checkout?basketId=<BasketId>&customerId=<CustomerId>"
```

### 9. **Repeat the Steps to Get a 5% Discount**

Repeat steps 1 through 8 for a second purchase using the same customer to receive a **5% discount** on the next purchase. Ensure that you follow the same flow and use the same `CustomerId` to accumulate the discount.

### 10. **Get the Sum of Discounted Prices for All Customers**

To retrieve the sum of all discounted prices for all customers, use the `/api/Basket/get-all-discounted-prices` endpoint with a `GET` request.

**Endpoint:**

```
GET /api/Basket/get-all-discounted-prices
```

**Example Request:**

```bash
curl -X GET "http://localhost:5239/api/Basket/get-all-discounted-prices"
```

This will return the sum of all the discounted prices applied across all customer purchases.

## Key Design Patterns

### Repository Pattern

This pattern is used to abstract the database operations for the core entities, such as `Product`, `Basket`, and `Discount`, making the application more modular and testable.

### Domain Event Pattern

The system uses **Domain Events** to handle side effects such as sending notifications, applying discounts, and managing stock asynchronously using RabbitMQ and MassTransit.

### Dependency Injection

The project leverages **Dependency Injection (DI)** to promote loose coupling between components, making the system easier to extend and test.

## Project Structure

```
├── Shop
│   ├── Application
│   │   ├── Contracts               # Interfaces for services
│   │   ├── Services                # Business logic and services
│
│   ├── Controllers                 # API Controllers
│   │   ├── BasketController.cs
│   │   ├── CheckoutController.cs
│   │   ├── CustomerController.cs
│   │   ├── ProductController.cs
│
│   ├── Domain
│   │   ├── Contracts               # Domain interfaces and abstractions
│   │   ├── Entities                # Core domain entities (Product, Basket, Discount, Customer, etc.)
│   │   ├── Enums                   # Domain-specific enums
│   │   ├── Events                  # Domain events (BasketCompletedEvent, DiscountAppliedEvent)
│   │   │   ├── Basket              # Basket-related events
│   │   │   ├── Customer            # Customer-related events
│   │   │   ├── Discount            # Discount-related events
│   │   │   ├── Product             # Product-related events
│   │   ├── ValueObjects            # Value objects like Price
│
│   ├── Infrastructure
│   │   ├── Contracts               # Infrastructure interfaces for repositories and services
│   │   ├── DataAccess              # Data access and EF Core configurations
│   │       ├── Migrations          # EF Core migrations for database
│   │   ├── Options                 # Configuration options (e.g., RabbitMQ)
│   │   ├── Repositories            # Repository implementations for entities
│   │   ├── RabbitMqDomainEventDispatcher.cs # RabbitMQ-based domain event dispatcher
│
│   ├── Tests
│   │   ├── Unit Tests              # Unit tests for services and domain logic
│   │   ├── Integration Tests       # Integration tests for key business flows
│
│   └── Shop.csproj                 # Main project file
└── README.md                       # Project documentation
```
### Sprint History

This project follows an agile development process using **sprints** to break down and manage work. Below is a summary of the completed sprints:

1. **Sprint 1**: 
   - Implemented the project’s infrastructure.
   - Set up a **Swagger** documentation for the API.
   - Initialized the GitHub repository with **GitHub Actions** for CI/CD.

2. **Sprint 2**: 
   - Designed the data models and database schema.
   - Implemented the database for all microservices.

3. **Sprint 3**: 
   - Developed the core functionality for **Product** and **Customer**.
   - **Bonus**: Added stock management for products.

4. **Sprint 4**: 
   - Implemented the **Basket** functionality, allowing customers to manage their shopping baskets.

5. **Sprint 5**: 
   - Developed the **Discount** functionality, allowing discounts to be applied to baskets.
   - Added querying capabilities to retrieve discounts for baskets.

6. **Sprint 6**: 
   - Focused on writing **unit tests** and **integration tests** to ensure system reliability.

7. **Sprint 7**: 
   - Created documentation for the project, including this **README.md** and architecture decision records (ADRs).

8. **Sprint 8**: 
   - Implemented the **Order** functionality, enabling customers to complete purchases and process orders.

## Integration Tests

The project includes integration tests for verifying end-to-end functionality such as:

- Adding products to the basket
- Applying discounts on the second purchase
- Completing a purchase and updating stock

Example integration test for checkout flow:

```csharp
[Fact]
public async Task CheckoutFlow_Should_Create_Order_From_Basket()
{
     // 1. Add Product
     // 2. Create a Customer
     // 3. Create a Basket for the Customer
     // 4. Checkout the Basket
     // 5. Verify the Results
}
```

## Contributing

Feel free to fork this project and create pull requests. Contributions are always welcome!

### Branching Strategy

We follow the **Feature Branch Strategy** for managing code in this repository. This strategy ensures that development happens in isolation from the main branch and that features are merged back into the main branch once they are complete and tested.

#### Workflow:

1. **Create a new branch for each feature or fix:**

   - When working on a new feature or fixing a bug, create a new branch from the `main` branch.
   - Branch names should follow this convention: `feature/feature-name` or `fix/bug-fix-description`.

   **Example:**

   ```bash
   git checkout -b feature/add-product-api
   ```

2. **Develop the feature:**

   - Commit your changes to the feature branch. Ensure that each commit is atomic and meaningful.

   **Example:**

   ```bash
   git commit -m "Add POST API for product creation"
   ```

3. **Push the feature branch:**

   Once the feature is complete and has passed local testing, push the branch to the remote repository.

   **Example:**

   ```bash
   git push origin feature/add-product-api
   ```

4. **Create a pull request (PR):**

   - Open a pull request against the `main` branch once your feature is ready.
   - Include a meaningful description and link to any related issues if applicable.
   - Ensure the PR is reviewed and approved by at least one team member before merging.

5. **Merge the pull request:**

   After the pull request is approved and all tests pass, merge it into the `main` branch. The branch can then be deleted if it’s no longer needed.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
