# Dog-Api
A simple .NET 8 Web API that caches dog breed photos from the dog.ceo API into a SQL Server database to reduce repeated external calls.

## Prerequisites
- Requires setup of SQL Server database connection in appsettings.json
- SQL script for database setup provided in init-db.sql

## Usage - (2 terminals)
```dotnet run --project DogApi```

```dotnet run --project DogPhotoClient```
