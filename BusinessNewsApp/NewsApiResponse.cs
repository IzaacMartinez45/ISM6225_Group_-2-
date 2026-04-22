using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace BusinessNewsApp.Models
{
    public class NewsApiResponse
    {
        [JsonPropertyName("articles")]
        public List<ArticleDto> Articles { get; set; }
    }

    public class ArticleDto
    {
        [JsonPropertyName("source")]
        public SourceDto Source { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class SourceDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}