# WeatherForecastingRestAPI


# Overview
The Weather Forecasting API is designed to provide weather data for specific cities on demand. It uses a public API to fetch current weather data, with additional functionalities such as caching previously searched cities to optimize API usage and minimize external requests. The API is built using the N-Tier architecture and follows the MVC pattern, ensuring a clear separation of concerns and maintainability. Key components include the Data Access Layer (DAL) utilizing the Repository Design Pattern, an in-memory database for caching, and comprehensive logging and testing mechanisms.

# Architecture
- N-Tier and MVC Architecture: The API is organized into distinct layers, adhering to the N-Tier architecture. The MVC pattern is employed for the API's structure, ensuring a clean separation between the data (Model), user interface (View), and business logic (Controller).

- Data Access Layer (DAL): The DAL is responsible for interacting with the in-memory database that stores city data. This layer uses the Repository Design Pattern to abstract the data operations, promoting code reuse and making the system more modular and testable.

- In-Memory Database: To avoid unnecessary API requests and optimize the limited number of calls (10,000 requests), the application caches previously searched cities in memory using DbContext. This approach prevents redundant external API calls, improving efficiency.

- Geocoding Service: This service is responsible for converting city names into geographic coordinates, which are then used to retrieve weather data from the external API. It ensures that the API can handle requests for weather information based on city names.

# Key Features
- Weather Data Retrieval: Clients can request weather data for a specific city, with optional parameters such as the date, time, and temperature type (Celsius or Fahrenheit). The API supports timezone adjustments to provide accurate local weather information.

- Swagger Documentation: Swagger is integrated into the API for comprehensive documentation. Data annotations are used extensively to provide clear descriptions of each API endpoint and parameter, improving developer experience and understanding.

- Logging: Serilog is implemented for logging, capturing detailed information about the API's operations, including errors. Logs are stored in files, allowing for easier debugging and monitoring of the API's behavior.

- Unit Testing: The API is thoroughly tested using xUnit. Moq is used to mock dependencies, isolating the controller for unit testing. Tests cover various scenarios, including successful requests (200), bad requests (400), and not found responses (404). A custom BeforeAfterAttribute is used to reset mocks before and after each test, ensuring test reliability.

# API Endpoints
- Get Weather Data
Endpoint: GET /weather/{cityName}
Description: Retrieves weather data for a specified city.
- Parameters:
cityName (required): The name of the city.
date (optional): The specific date for which weather data is requested (default is the current date).
timezones (optional): The timezone for which the weather data should be adjusted.
degreeType (optional): The temperature scale (Celsius or Fahrenheit, default is Celsius).
- Responses:
200 OK: Successfully returns the weather data.
400 Bad Request: If the city name is missing or invalid.
404 Not Found: If no weather data is available for the requested city.

- Custom Test Attributes: The use of the BeforeAfterAttribute ensures that mocks are reset before and after each test, maintaining a clean test environment and preventing cross-test contamination.
