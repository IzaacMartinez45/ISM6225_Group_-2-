namespace StockWatchlistApp.Models;

public class WatchlistStock
{
    public int Id { get; set; }
    public string Ticker { get; set; } = "";
    public string CompanyName { get; set; } = "";
    public decimal TargetPrice { get; set; }
    public string Category { get; set; } = "";
    public string Notes { get; set; } = "";
}