using Microsoft.AspNetCore.Mvc;
using BusinessNewsApp.Models;
using System.Text.Json;

namespace BusinessNewsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<NewsArticle> newsList = new List<NewsArticle>();

            string apiKey = _configuration["NewsApi:ApiKey"];
            string endpoint = $"https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    NewsApiResponse apiResponse = JsonSerializer.Deserialize<NewsApiResponse>(json, options);

                    if (apiResponse?.Articles != null)
                    {
                        newsList = apiResponse.Articles.Select(article => new NewsArticle
                        {
                            SourceName = article.Source?.Name,
                            Title = article.Title,
                            Url = article.Url
                        }).ToList();
                    }
                }
            }

            return View(newsList);
        }
    }
}