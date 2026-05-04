using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockWatchlistApp.Data;
using StockWatchlistApp.Models;
using StockWatchlistApp.Services;

namespace StockWatchlistApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly StockApiService _stockApiService;

        public HomeController(ApplicationDbContext dbContext, StockApiService stockApiService)
        {
            _dbContext = dbContext;
            _stockApiService = stockApiService;
        }

        public async Task<IActionResult> Index()
        {
            // Take up to 3 stocks from the database and fetch their live data
            var stocks = await _dbContext.WatchlistStocks.Take(3).ToListAsync();

            var preview = new List<(WatchlistStock Stock, StockApiData ApiData)>();

            foreach (var stock in stocks)
            {
                var apiData = await _stockApiService.GetPreviousDayDataAsync(stock.Ticker);
                preview.Add((stock, apiData));
            }

            return View(preview);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
