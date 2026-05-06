# StockTracker — ISM6225 Group 2

A full-stack **ASP.NET Core MVC** stock watchlist application built for the ISM6225 Application Development for Analytics final project (Spring 2026). The app allows users to manage a personal stock watchlist with full CRUD functionality, live market data from Polygon.io, persistent SQLite storage, and Azure deployment.

---

## Features

- **Full CRUD Operations** — Create, Read, Update, and Delete stocks in your watchlist
- **Live Market Data** — Previous-day OHLCV data fetched from the Polygon.io API for every tracked stock
- **Persistent Storage** — SQLite database with Entity Framework Core for data persistence across sessions
- **MVC Architecture** — Clean separation of concerns with Controllers, Models, Views, and Services
- **Dark-Mode UI** — Professional trading terminal interface built with Tailwind CSS
- **Azure Deployment** — Fully deployed and accessible on Azure App Service

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core MVC (.NET 10) |
| Database | SQLite via Entity Framework Core |
| API | Polygon.io REST API |
| Frontend | Razor Views, Tailwind CSS, Google Material Symbols |
| Deployment | Azure App Service |

---

## Project Structure

```
StockWatchlistApp_FINAL/
├── Program.cs                        # Entry point — configures services and middleware
├── appsettings.json                  # DB connection string and API key config
│
├── Models/
│   ├── WatchlistStock.cs             # Persistent entity (saved to DB)
│   └── StockApiData.cs              # In-memory model (from API, not stored)
│
├── Data/
│   └── ApplicationDbContext.cs       # EF Core DbContext — bridge to SQLite
│
├── Services/
│   └── StockApiService.cs            # HTTP client for Polygon.io API
│
├── Controllers/
│   ├── HomeController.cs             # Dashboard, About Us, Privacy
│   └── WatchlistController.cs        # Full CRUD for watchlist stocks
│
├── Views/
│   ├── Shared/_Layout.cshtml         # Master layout (sidebar, header, footer)
│   ├── Home/                         # Landing page, About Us
│   └── Watchlist/                    # Index, Create, Edit, Details, Delete
│
└── wwwroot/                          # Static assets (CSS, JS, images)
```

---

## Entity Relationship Diagram (ERD)

The application uses one persistent database table and one in-memory model for API data:

```
┌──────────────────────────────────┐
│        WatchlistStocks           │
│          (SQLite DB)             │
├──────────────────────────────────┤
│  Id           INT  (PK, Auto)   │
│  Ticker       TEXT (Required)    │
│  CompanyName  TEXT (Required)    │
│  TargetPrice  REAL (Required)   │
│  Category     TEXT (Required)    │
│  Notes        TEXT               │
└──────────────┬───────────────────┘
               │
               │  Ticker is used to fetch live data
               ▼
┌──────────────────────────────────┐
│     StockApiData (in-memory)     │
│     (from Polygon.io API)        │
├──────────────────────────────────┤
│  Ticker         string           │
│  Open           decimal          │
│  High           decimal          │
│  Low            decimal          │
│  PreviousClose  decimal          │
│  Volume         long             │
│  IsAvailable    bool             │
└──────────────────────────────────┘
```

> `WatchlistStocks` is persisted in SQLite. `StockApiData` is fetched on each request and never stored.

---

## API Integration

| Detail | Value |
|--------|-------|
| Provider | [Polygon.io](https://polygon.io/) |
| Endpoint | `GET /v2/aggs/ticker/{TICKER}/prev` |
| Data returned | Open, High, Low, Close, Volume for previous trading day |
| Authentication | API key passed as query parameter |

The API is used for **data display only** — fetched data is not stored in the database. Each time a user views the Watchlist or Dashboard, the app makes live API calls for current market data.

**API key configuration:**
- **Local development:** Add your key to `appsettings.json` under `MassiveApi:ApiKey` (do not commit)
- **Azure:** Configure as App Setting with key name `MassiveApi__ApiKey`

---

## CRUD Implementation

| Operation | HTTP Method | Route | Controller Action | Description |
|-----------|-------------|-------|-------------------|-------------|
| **Create** | GET | `/Watchlist/Create` | `Create()` | Display the add stock form |
| **Create** | POST | `/Watchlist/Create` | `Create(stock)` | Validate and save new stock to DB |
| **Read** | GET | `/Watchlist` | `Index()` | List all stocks with live API data |
| **Read** | GET | `/Watchlist/Details/{id}` | `Details(id)` | View a single stock's details |
| **Update** | GET | `/Watchlist/Edit/{id}` | `Edit(id)` | Display edit form with current values |
| **Update** | POST | `/Watchlist/Edit/{id}` | `Edit(stock)` | Validate and update stock in DB |
| **Delete** | GET | `/Watchlist/Delete/{id}` | `Delete(id)` | Show delete confirmation page |
| **Delete** | POST | `/Watchlist/DeleteConfirmed/{id}` | `DeleteConfirmed(id)` | Remove stock from DB |

All CRUD changes are immediately reflected across the entire application (Dashboard preview, Watchlist table, etc.).

---

## Database Persistence

- **Engine:** SQLite via Entity Framework Core
- **File:** `watchlist.db` (auto-created at startup via `EnsureCreated()`)
- **Not committed to Git** — each environment generates its own database from the EF Core model

To apply migrations locally:
```bash
dotnet ef database update
```

---

## Azure Deployment

The application is deployed on **Azure App Service**. Key configuration:

1. **API Key:** Set as Application Setting → `MassiveApi__ApiKey`
2. **Database:** SQLite runs on Azure with the `watchlist.db` file created automatically
3. **Runtime:** .NET 10 on Linux

> Remember to pause Azure resources when not in use to avoid unnecessary charges.

---

## Team Members

| Name | Role |
|------|------|
| **Santiago Vela** | Frontend Structure & Navigation — Landing page, About Us, site navigation |
| **Aitemir Kermaliev** | Backend Validation & Database Testing — Migrations, SQLite, CRUD workflow |
| **Izaac Martinez** | Azure Deployment & API Configuration — App Service, Polygon.io setup |
| **Ismail Jhaveri** | UI Refinement & Presentation — Styling, layout consistency, presentation |

---

## How to Run Locally

```bash
cd StockWatchlistApp_FINAL
dotnet run
```

The app will start at `http://localhost:5238` (or the port shown in console).

---

## Notable Technical Challenges & Solutions

| Challenge | Solution |
|-----------|----------|
| .NET version mismatch (project targeted net8.0 but SDK was net10.0) | Updated `TargetFramework` and EF Core packages to match installed SDK |
| API key security — avoiding hardcoded keys in source | Used `appsettings.json` locally + Azure App Settings in production |
| Polygon.io volume field returned as double, not long | Cast with `(long)first.GetProperty("v").GetDouble()` |
| Database not existing on first run | `EnsureCreated()` in `Program.cs` auto-creates DB and schema at startup |
