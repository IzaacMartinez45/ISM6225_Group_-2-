using System.Text.Json;
using StockWatchlistApp.Models;

namespace StockWatchlistApp.Services;

public class StockApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public StockApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<StockApiData> GetPreviousDayDataAsync(string ticker)
    {
        ticker = ticker.ToUpper().Trim();

        var apiKey = _configuration["MassiveApi:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            Console.WriteLine("API key is missing from appsettings.json");

            return new StockApiData
            {
                Ticker = ticker,
                IsAvailable = false
            };
        }

        var url = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/prev?apiKey={apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API Response:");
            Console.WriteLine(json);

            if (!response.IsSuccessStatusCode)
            {
                return new StockApiData
                {
                    Ticker = ticker,
                    IsAvailable = false
                };
            }

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("results", out var results) || results.GetArrayLength() == 0)
            {
                return new StockApiData
                {
                    Ticker = ticker,
                    IsAvailable = false
                };
            }

            var first = results[0];

            return new StockApiData
            {
                Ticker = ticker,
                Open = first.GetProperty("o").GetDecimal(),
                High = first.GetProperty("h").GetDecimal(),
                Low = first.GetProperty("l").GetDecimal(),
                PreviousClose = first.GetProperty("c").GetDecimal(),
                Volume = (long)first.GetProperty("v").GetDouble(), // FIXED LINE
                IsAvailable = true
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("API error:");
            Console.WriteLine(ex.Message);

            return new StockApiData
            {
                Ticker = ticker,
                IsAvailable = false
            };
        }
    }
}