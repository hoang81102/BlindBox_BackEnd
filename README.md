# BlindBoxSS-BE

## Overview

BlindBoxSS-BE is the backend service for the BlindBoxSS application. It provides APIs and handles data processing for the application.

## Features

- User authentication and authorization
- Data storage and retrieval
- API endpoints for various functionalities
- Error handling and logging

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or later

### Setup

1. Clone the repository:

   ```sh
   git clone <repository-url>
   cd BlindBoxSS.API
   ```

2. Restore the dependencies:

   ```sh
   dotnet restore
   ```

3. Build the project:

   ```sh
   dotnet build
   ```

4. Run the project:
   ```sh
   dotnet run
   ```

### Running the API

The API will be available at `https://localhost:7166` or `http://localhost:5134`. You can access the Swagger UI at `https://localhost:7166/swagger` or `http://localhost:5134/swagger`.

### API Endpoints

- `GET /weatherforecast` - Retrieves a list of weather forecasts.

## Project References

- [DAO](http://_vscodecontentref_/11) - Data Access Layer
- [Repository](http://_vscodecontentref_/12) - Repository Layer
- [Services](http://_vscodecontentref_/13) - Service Layer

## Configuration

Configuration files:

- [appsettings.json](http://_vscodecontentref_/14)
- [appsettings.Development.json](http://_vscodecontentref_/15)

## License

This project is licensed under the MIT License.
