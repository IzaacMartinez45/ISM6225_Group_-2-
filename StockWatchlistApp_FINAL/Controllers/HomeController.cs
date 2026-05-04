using Microsoft.AspNetCore.Mvc;
using StockWatchlistApp.Models;
using StockWatchlistApp.Services;

namespace StockWatchlistApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WatchlistService _watchlistService;
        private readonly StockApiService _stockApiService;

        public HomeController(WatchlistService watchlistService, StockApiService stockApiService)
        {
            _watchlistService = watchlistService;
            _stockApiService = stockApiService;
        }

        public async Task<IActionResult> Index()
        {
            // Take up to 3 stocks from the watchlist and fetch their live data
            var stocks = _watchlistService.GetAll().Take(3).ToList();

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
