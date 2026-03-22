using System.Text.Json.Serialization;

namespace JobHunter.Application.Models;
public class ArbeitnowJob
{
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty; // We will use this as the ExternalId

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("company_name")]
    public string CompanyName { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("location")]
    public string Location { get; set; } = string.Empty;
}
