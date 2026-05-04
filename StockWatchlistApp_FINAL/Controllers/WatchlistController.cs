using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StockWatchlistApp.Data;
using StockWatchlistApp.Models;
using StockWatchlistApp.Services;

namespace StockWatchlistApp.Controllers;

public class WatchlistController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly StockApiService _stockApiService;

    public WatchlistController(
        ApplicationDbContext dbContext,
        StockApiService stockApiService)
    {
        _dbContext = dbContext;
        _stockApiService = stockApiService;
    }

    // 🔥 UPDATED INDEX WITH API DATA
    public async Task<IActionResult> Index()
    {
        var stocks = await _dbContext.WatchlistStocks.ToListAsync();

        var stockDataList = new List<(WatchlistStock Stock, StockApiData ApiData)>();

        foreach (var stock in stocks)
        {
            var apiData = await _stockApiService.GetPreviousDayDataAsync(stock.Ticker);
            stockDataList.Add((stock, apiData));
        }

        return View(stockDataList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(WatchlistStock stock)
    {
        if (ModelState.IsValid)
        {
            stock.Ticker = stock.Ticker.ToUpper().Trim();
            _dbContext.WatchlistStocks.Add(stock);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(stock);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var stock = await _dbContext.WatchlistStocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(WatchlistStock stock)
    {
        if (ModelState.IsValid)
        {
            var existingStock = await _dbContext.WatchlistStocks.FindAsync(stock.Id);

            if (existingStock == null)
            {
                return NotFound();
            }

            existingStock.Ticker = stock.Ticker.ToUpper().Trim();
            existingStock.CompanyName = stock.CompanyName;
            existingStock.TargetPrice = stock.TargetPrice;
            existingStock.Category = stock.Category;
            existingStock.Notes = stock.Notes;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(stock);
    }

    public async Task<IActionResult> Details(int id)
    {
        var stock = await _dbContext.WatchlistStocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var stock = await _dbContext.WatchlistStocks.FindAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var stock = await _dbContext.WatchlistStocks.FindAsync(id);

        if (stock != null)
        {
            _dbContext.WatchlistStocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}