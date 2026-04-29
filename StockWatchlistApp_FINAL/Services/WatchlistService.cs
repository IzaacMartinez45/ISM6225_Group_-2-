using StockWatchlistApp.Models;

namespace StockWatchlistApp.Services;

public class WatchlistService
{
    private readonly List<WatchlistStock> _stocks = new();
    private int _nextId = 1;

    public List<WatchlistStock> GetAll()
    {
        return _stocks;
    }

    public WatchlistStock? GetById(int id)
    {
        return _stocks.FirstOrDefault(s => s.Id == id);
    }

    public void Add(WatchlistStock stock)
    {
        stock.Id = _nextId++;
        stock.Ticker = stock.Ticker.ToUpper();
        _stocks.Add(stock);
    }

    public void Update(WatchlistStock updatedStock)
    {
        var stock = GetById(updatedStock.Id);

        if (stock == null)
        {
            return;
        }

        stock.Ticker = updatedStock.Ticker.ToUpper();
        stock.CompanyName = updatedStock.CompanyName;
        stock.TargetPrice = updatedStock.TargetPrice;
        stock.Category = updatedStock.Category;
        stock.Notes = updatedStock.Notes;
    }

    public void Delete(int id)
    {
        var stock = GetById(id);

        if (stock != null)
        {
            _stocks.Remove(stock);
        }
    }
}