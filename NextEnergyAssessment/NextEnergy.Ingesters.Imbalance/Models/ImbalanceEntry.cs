using System;
using System.Text.Json.Serialization;

namespace NextEnergy.Ingesters.Imbalance.Models;

public sealed record ImbalanceEntry
{
    [JsonPropertyName("IntervalStart")]
    public DateTime IntervalStart { get; set; }

    [JsonPropertyName("IntervalEnd")]
    public DateTime IntervalEnd { get; set; }

    [JsonPropertyName("MinPrice")]
    public double MinPrice { get; set; }

    [JsonPropertyName("MidPrice")]
    public double MidPrice { get; set; }

    [JsonPropertyName("MaxPrice")]
    public double MaxPrice { get; set; }
}
