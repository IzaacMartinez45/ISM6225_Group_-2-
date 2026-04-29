namespace StockWatchlistApp.Models;

public class StockApiData
{
    public string Ticker { get; set; } = "";
    public decimal PreviousClose { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Open { get; set; }
    public long Volume { get; set; }
    public bool IsAvailable { get; set; }
}