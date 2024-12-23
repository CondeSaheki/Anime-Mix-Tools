using Newtonsoft.Json;

namespace osuAPI.Data;

public class OsuBeatmapset
{
    [JsonProperty("covers")]
    public Dictionary<string, string> Covers { get; set; } = [];
    [JsonProperty("creator")]
    public string Creator { get; set; } = string.Empty;
    [JsonProperty("id")]
    public long Id { get; set; } = 0;
    [JsonProperty("user_id")]
    public long UserId { get; set; } = 0;
    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; } = DateTime.MinValue;
    [JsonProperty("ranked_date")]
    public DateTime RankedDate { get; set; } = DateTime.MinValue;
    [JsonProperty("submitted_date")]
    public DateTime SubmittedDate { get; set; } = DateTime.MinValue;
    [JsonProperty("tags")]
    public string Tags { get; set; } = string.Empty;
    [JsonProperty("beatmaps")]
    public List<OsuBeatmap> Beatmaps { get; set; } = [];
}