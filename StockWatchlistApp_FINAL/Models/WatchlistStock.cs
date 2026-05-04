using System.ComponentModel.DataAnnotations;

namespace StockWatchlistApp.Models;

public class WatchlistStock
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ticker symbol is required")]
    public string Ticker { get; set; } = "";

    [Required(ErrorMessage = "Company name is required")]
    public string CompanyName { get; set; } = "";

    [Required(ErrorMessage = "Target price is required")]
    [Range(0.01, 1000000, ErrorMessage = "Target price must be a positive number")]
    public decimal TargetPrice { get; set; }

    [Required(ErrorMessage = "Please select a category")]
    public string Category { get; set; } = "";

    public string Notes { get; set; } = "";
}
