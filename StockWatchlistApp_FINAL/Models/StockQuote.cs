namespace StockWatchlistApp.Models;

public class StockQuote
{
    public string Ticker { get; set; } = "";
    public decimal CurrentPrice { get; set; }
    public decimal Change { get; set; }
    public decimal PercentChange { get; set; }
}