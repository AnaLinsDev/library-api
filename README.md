# Library API

A RESTful Web API built with **C# and ASP.NET Core** for managing a library of books.

This project was developed to practice **ASP.NET Core Web API, layered architecture, dependency injection, DTOs, business rules, validation, exception handling, and RESTful API development**.

## Technologies

- C#
- .NET
- ASP.NET Core Web API
- Swagger / OpenAPI
- Visual Studio

## Features

- List all books
- Get a book by ID
- Create a book
- Update a book
- Delete a book
- Input validation
- Business rule validation
- Genre validation and conversion
- Duplicate book validation
- `CreatedAt` and `UpdatedAt` timestamps
- HTTP status code handling

## Business Rules

- Book title and author combination cannot be duplicated.
- Price cannot be negative.
- Stock cannot be negative.
- Genre must be valid.
- `CreatedAt` is set when a book is created.
- `UpdatedAt` is updated when a book is modified.

## Architecture

### Controller
Responsible for handling HTTP requests and responses, route parameters, and HTTP status codes.

### Service
Responsible for business rules such as price, stock, genre, and duplicate book validation.

### DTOs
Used to separate API request/response models from the internal `Book` model.

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/books` | Get all books |
| GET | `/api/books/{id}` | Get a book by ID |
| POST | `/api/books` | Create a book |
| PUT | `/api/books/{id}` | Update a book |
| DELETE | `/api/books/{id}` | Delete a book |

## What I Practiced

Through this project, I practiced:

- ASP.NET Core Web API fundamentals
- RESTful API design
- HTTP methods and status codes
- Dependency Injection
- Layered Architecture
- DTOs
- Business rule implementation
- Input validation
- Exception handling
- LINQ
- Debugging with Visual Studio

## How to Run

### Prerequisites

Make sure you have installed:

- .NET SDK
- Visual Studio 2022 or another C#/.NET IDE

### 1. Clone the repository

```bash
git clone https://github.com/AnaLinsDev/library-api.git
```

### 2. Navigate to the project

```bash
cd LibraryAPI
```

### 3. Restore dependencies

```bash
dotnet restore
```

### 4. Build the project

```bash
dotnet build
```

### 5. Run the API

```bash
dotnet run
```

The terminal will display the URL where the API is running.

### 6. Open Swagger

Open the Swagger URL displayed by the application in your browser.

Swagger can be used to test the available API endpoints without requiring Postman or another API client.
