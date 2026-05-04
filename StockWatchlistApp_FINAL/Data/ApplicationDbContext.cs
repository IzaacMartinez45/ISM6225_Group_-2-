using Microsoft.EntityFrameworkCore;
using StockWatchlistApp.Models;

namespace StockWatchlistApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<WatchlistStock> WatchlistStocks => Set<WatchlistStock>();
}
