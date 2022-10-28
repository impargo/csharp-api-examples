using System.Text.Json.Serialization;

namespace Impargo.GraphQLClient.Queries;

public record GetCompanyResponseType
{
    public Company Company { get; set; }
}

public record Company
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("company")]
    public string CompanyName { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
}
