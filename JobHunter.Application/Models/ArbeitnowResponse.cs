using System.Text.Json.Serialization;

namespace JobHunter.Application.Models;
public class ArbeitnowResponse
{
    [JsonPropertyName("data")]
    public List<ArbeitnowJob> Data { get; set; } = new();
}
