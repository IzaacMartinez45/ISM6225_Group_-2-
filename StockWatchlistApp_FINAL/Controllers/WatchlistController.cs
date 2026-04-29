using Microsoft.AspNetCore.Mvc;
using StockWatchlistApp.Models;
using StockWatchlistApp.Services;

namespace StockWatchlistApp.Controllers;

public class WatchlistController : Controller
{
    private readonly WatchlistService _watchlistService;
    private readonly StockApiService _stockApiService;

    public WatchlistController(
        WatchlistService watchlistService,
        StockApiService stockApiService)
    {
        _watchlistService = watchlistService;
        _stockApiService = stockApiService;
    }

    // 🔥 UPDATED INDEX WITH API DATA
    public async Task<IActionResult> Index()
    {
        var stocks = _watchlistService.GetAll();

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
    public IActionResult Create(WatchlistStock stock)
    {
        if (ModelState.IsValid)
        {
            _watchlistService.Add(stock);
            return RedirectToAction("Index");
        }

        return View(stock);
    }

    public IActionResult Edit(int id)
    {
        var stock = _watchlistService.GetById(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    [HttpPost]
    public IActionResult Edit(WatchlistStock stock)
    {
        if (ModelState.IsValid)
        {
            _watchlistService.Update(stock);
            return RedirectToAction("Index");
        }

        return View(stock);
    }

    public IActionResult Details(int id)
    {
        var stock = _watchlistService.GetById(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    public IActionResult Delete(int id)
    {
        var stock = _watchlistService.GetById(id);

        if (stock == null)
        {
            return NotFound();
        }

        return View(stock);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _watchlistService.Delete(id);
        return RedirectToAction("Index");
    }
}