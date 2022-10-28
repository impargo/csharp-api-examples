using System.Text.Json.Serialization;

namespace Impargo.GraphQLClient.Mutations;

public record ImportOrderResponseType
{
    public CompanyOrder ImportOrder { get; set; }
}

public record CompanyOrder
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    public Order Order { get; set; }
}

public record Order
{
    public string Reference { get; set; }

    public Route Route { get; set; }
}

public record Route
{

    public float Distance { get; set; }

    public float Time { get; set; }

    public RouteDetails RouteDetails { get; set; }
}

public record RouteDetails
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }
    public Toll Tolls { get; set; }
}

public record Toll
{
    public TollSummary Summary { get; set; }
    public IEnumerable<TollDetails> Details { get; set; }
    public IEnumerable<TollSystem> ByCountryAndTollSystem { get; set; }
}

public record TollSystem
{
    public string Name { get; set; }
    public string Country { get; set; }
    public decimal Amount { get; set; }
    public string TollType { get; set; }
}

public record TollDetails
{
    public decimal Amount { get; set; }
    public decimal DistanceCharged { get; set; }
    public decimal DistanceFree { get; set; }
    public string Country { get; set; }
    public string TollType { get; set; }
}

public record TollSummary
{
    public decimal Amount { get; set; }
    public decimal? DistanceCharged { get; set; }
    public decimal? DistanceFree { get; set; }
}