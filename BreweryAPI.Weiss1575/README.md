# BreweryAPI

This ReadMe provides an overview of the BreweryAPI project, which is designed to manage beer-related operations for breweries and wholesalers. The project is built using .NET Core Web API and follows a RESTful API architecture.

## TODO
- Error handler for 500 responses
- Add Serilog
- Implement brewery inventory similar to wholesaler
- Implement beer stock type (4-pack, 6-pack, 12-pack, etc) with unit per stock
- Implement allowing different discounts per wholesaler with endpoints to update them
- Authorization roles for breweries, clients, and wholesalers
- Add more unit tests

## Table of Contents

- [Project Structure](#project-structure)
  - [Presentation Layer](#presentation-layer)
  - [Business Logic Layer (BLL)](#business-logic-layer-bll)
  - [Data Access Layer (DAL)](#data-access-layer-dal)
- [Controllers](#controllers)
  - [BreweriesController](#breweriescontroller)
    - [API Endpoints](#api-endpoints)
  - [WholesalersController](#wholesalerscontroller)
    - [API Endpoints](#api-endpoints-1)
- [Services](#services)
  - [BreweryService](#breweryservice)
  - [DiscountService](#discountservice)
  - [WholesalerService](#wholesalerservice)
- [Data Transfer Objects (DTOs)](#data-transfer-objects-dtos)
- [Data Models and Relationships](#data-models-and-relationships)
- [Database Pre-filling](#database-pre-filling)
- [Getting Started](#getting-started)
- [Contributing](#contributing)
- [License](#license)

## Project Structure

The BreweryAPI project is structured according to a layered architecture pattern, separating different concerns and responsibilities into distinct layers. These layers help maintain code organization, modularity, and scalability. The primary layers of the project include:

### Presentation Layer

- **Controllers**: Located in the `Controllers` directory, these handle incoming HTTP requests and route them to the appropriate service methods.

### Business Logic Layer (BLL)

- **Services**: Business logic and data processing are encapsulated in services, located in the `BLL.Services` directory.
  - `BreweryService`: Manages brewery-related operations such as adding, deleting, updating, and retrieving beers. Also handles beer sales to wholesalers.
  - `DiscountService`: Calculates discounts on beer orders based on quantity, determining discount percentages and discounted prices.
  - `WholesalerService`: Manages wholesaler-related operations, including generating quotes for beer orders and checking for item duplicates and stock availability.

### Data Access Layer (DAL)

- The data access layer is responsible for interacting with the database.
- The `BreweryRepository` and `WholesalerRepository` are used to perform database operations related to breweries and wholesalers.
- Entity Framework Core is commonly used in this layer for database communication.

This layered structure promotes separation of concerns, making the codebase more maintainable and extensible. It also enables easier unit testing and improves overall code organization. Each layer has its own well-defined responsibilities, contributing to the overall architecture and functionality of the BreweryAPI project.

## Controllers

### `BreweriesController`

- Manages operations related to breweries and beers.
- Provides endpoints for adding, deleting, updating, and retrieving beers.
- Handles the processing of beer sales to wholesalers.

#### API Endpoints

- **Endpoint**: `POST /api/breweries/{id}/beers`
  - **Description**: Adds a new beer to a brewery.

- **Endpoint**: `DELETE /api/breweries/{id}/beers/{beerId}`
  - **Description**: Deletes a beer from a brewery.

- **Endpoint**: `GET /api/breweries/{id}/beers/{beerId}`
  - **Description**: Retrieves a specific beer by its ID.

- **Endpoint**: `GET /api/breweries/{id}/beers`
  - **Description**: Retrieves all beers associated with a brewery.

- **Endpoint**: `PUT /api/breweries/{id}/beers/{beerId}`
  - **Description**: Updates an existing beer in a brewery.

- **Endpoint**: `POST /api/breweries/{id}/sales/{wholesalerId}`
  - **Description**: Processes a sale of beers from a brewery to a wholesaler.

### `WholesalersController`

- Manages operations related to wholesalers and quote generation.
- Offers an endpoint for generating quotes for beer orders from wholesalers.

#### API Endpoints

- **Endpoint**: `POST /api/wholesalers/{id}/quotes`
  - **Description**: Allows a client to request a quote from a wholesaler.

These endpoints provide the functionality for clients to interact with wholesalers and request quotes within the BreweryAPI project.

## Services

### `BreweryService`

- Implements the `IBreweryService` interface to manage brewery-related operations.
- Handles adding, deleting, updating, and retrieving beers for a brewery.
- Manages the processing of beer sales to wholesalers.

### `DiscountService`

- Implements the `IDiscountService` interface to calculate discounts on beer orders based on quantity.
- Determines the discount percentage and the discounted price for beer orders.

### `WholesalerService`

- Implements the `IWholesalerService` interface to manage wholesaler-related operations.
- Generates quotes for beer orders from wholesalers.
- Checks for duplicate items and available stock before generating quotes.

## Data Transfer Objects (DTOs)

DTOs are used to transfer data between the API and clients. They help maintain a separation between the data models and what is exposed to external clients.
- Located in the `BLL.DataTransferObjects` directory, these DTOs facilitate data exchange between the API and clients. Key DTOs include:
    - `BeerCreateDto`: Used for creating new beers.
    - `BeerUpdateDto`: Used for updating existing beers.
    - `BeerDto`: Represents beer data sent to clients.
    - `BrewerySaleRequestDto`: Represents requests for beer sales to wholesalers.
    - `BrewerySaleRequestItemDto`: Represents individual items in sale requests.
    - `WholesalerQuoteRequestDto`: Represents requests for quotes from wholesalers.
    - `WholesalerQuoteRequestItemDto`: Represents items in quote requests.
    - `WholesalerQuoteResponseDto`: Contains quote details sent as responses.
    - `QuoteResponseItemDto`: Represents items in quotes, including beer name, quantity, unit price, and subtotal.

## Data Models and Relationships

- **`Beer`**: Represents a type of beer produced by a brewery.
  - **Relationships**:
    - Many-to-One with `Brewery`: Many beers can belong to one brewery.

- **`Brewery`**: Represents a brewery that produces beers.
  - **Relationships**:
    - One-to-Many with `Beer`: One brewery can produce many beers.

- **`BrewerySale`**: Represents a sale transaction from a brewery to a wholesaler.
  - **Relationships**:
    - Many-to-One with `Brewery`: Many sales can belong to one brewery.
    - Many-to-One with `Wholesaler`: Many sales can belong to one wholesaler.
    - Many-to-One with `Beer`: Many sales can involve one type of beer.

- **`Wholesaler`**: Represents a wholesaler that

 sells beers to clients.
  - **Relationships**:
    - One-to-Many with `WholesalerBeer`: One wholesaler can have many types of beers in stock.

- **`WholesalerBeer`**: Represents the quantity of a specific beer available in a wholesaler's stock.
  - **Relationships**:
    - Many-to-One with `Wholesaler`: Many wholesaler beers can belong to one wholesaler.
    - Many-to-One with `Beer`: Many wholesaler beers can represent one type of beer.

These data models and their relationships form the core of the BreweryAPI project, enabling the management of breweries, beers, sales, and wholesalers. The relationships reflect how these entities are connected in the database schema and how they interact within the application.

## Database Pre-filling

The BreweryAPI project comes with a pre-filled database to provide sample data for testing and development purposes. This pre-filling is achieved through Entity Framework Core's data seeding functionality in the `BreweryAPIContext`.
This pre-filled data will help you get started with testing and using the BreweryAPI project.

## Getting Started

To set up and run the BreweryAPI project, follow these steps:

1. Clone the repository to your local machine.
2. Ensure you have the required dependencies installed, including .NET Core.
3. Configure the database connection in the `appsettings.json` file. Make sure the connection string is accurate and points to the desired database server.
4. Run the database migrations to create the required tables.
5. Build and run the application using `dotnet run`.
6. Access the API endpoints using the Swagger UI, a tool like [Postman](https://www.postman.com/), or through your preferred client.

## Contributing

Contributions to the BreweryAPI project are welcome! If you have improvements, bug fixes, or new features to suggest, please create a pull request with a clear description of the changes.

## License

The BreweryAPI project is open-source and available under the [MIT License](LICENSE). You are free to use, modify, and distribute it according to the terms of the license.
