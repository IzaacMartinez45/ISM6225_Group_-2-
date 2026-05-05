# ISM6225 Group 2

## Stock Watchlist Application

`Stock Watchlist Application` is an ASP.NET MVC Stock Watchlist app. It supports full CRUD operations (Create, Read, Update, Delete) for watchlist stocks and uses SQLite with Entity Framework Core for persistent storage.

### Database Persistence

- Local database file: `StockWatchlistApp_FINAL/watchlist.db`
- This file is intentionally not committed to GitHub.
- The database schema is recreated from EF Core migrations.

Apply migrations locally:

```bash
dotnet ef database update
```

## Entity Relationship Diagram (ERD)

The current version of the application uses one main database table: `WatchlistStocks`. This table stores each stock entry added by the user to the watchlist.

```text
WatchlistStocks
---------------
Id (PK)
Ticker
CompanyName
TargetPrice
Category
Notes
```

### API Integration

- The app integrates with the Polygon API (`/v2/aggs/ticker/{ticker}/prev`) for previous-day stock data.
- API keys should not be hardcoded or committed.
- For local testing, developers may temporarily add their own key in `StockWatchlistApp_FINAL/appsettings.json`, but it must be removed before committing.

### Azure Deployment

In Azure App Service, configure the API key in:

- **App Service -> Configuration -> Application Settings**
- Key name: `MassiveApi__ApiKey`

### Azure Database Note

The application currently uses SQLite with Entity Framework Core. Azure deployment does not require committing the local `watchlist.db` file. The deployed environment can create its own database from the EF Core migrations.

For this class project, SQLite can be used in Azure as long as the app has a writable database path and migrations are applied.

If the team chooses to use Azure SQL Database instead, the Entity Framework Core structure can still be reused, but the database provider and connection string would need to be updated.
