using Newtonsoft.Json;

namespace osuAPI.Data;

public class OsuBeatmap
{
    [JsonProperty("beatmapset_id")]
    public long BeatmapsetId { get; set; } = 0;
    [JsonProperty("id")]
    public long Id { get; set; } = 0;
    [JsonProperty("user_id")]
    public long UserId { get; set; } = 0;
    [JsonProperty("version")]
    public string Version { get; set; } = "";
}