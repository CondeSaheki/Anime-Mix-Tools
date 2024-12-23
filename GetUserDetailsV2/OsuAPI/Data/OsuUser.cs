using Newtonsoft.Json;

namespace osuAPI.Data;

public class OsuUser
{
    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; } = string.Empty;
    [JsonProperty("country_code")]
    public string CountryCode { get; set; } = string.Empty;
    [JsonProperty("default_group")]
    public string DefaultGroup { get; set; } = string.Empty;
    [JsonProperty("id")]
    public long Id { get; set; } = 0;
    [JsonProperty("is_bot")]
    public bool IsBot { get; set; } = false;
    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; } = false;
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;
    [JsonProperty("cover_url")]
    public string CoverUrl { get; set; } = string.Empty;
    [JsonProperty("join_date")]
    public DateTime JoinDate { get; set; } = DateTime.MinValue;
    [JsonProperty("graveyard_beatmapset_count")]
    public int GraveyardBeatmapsetCount { get; set; } = 0;
    [JsonProperty("groups")]
    public List<Group> Groups { get; set; } = [];
    [JsonProperty("guest_beatmapset_count")]
    public int GuestBeatmapsetCount { get; set; } = 0;
    [JsonProperty("loved_beatmapset_count")]
    public int LovedBeatmapsetCount { get; set; } = 0;
    [JsonProperty("nominated_beatmapset_count")]
    public int NominatedBeatmapsetCount { get; set; } = 0;
    [JsonProperty("pending_beatmapset_count")]
    public int PendingBeatmapsetCount { get; set; } = 0;
    [JsonProperty("previous_usernames")]
    public List<string> PreviousUsernames { get; set; } = [];
    [JsonProperty("ranked_beatmapset_count")]
    public int RankedBeatmapsetCount { get; set; } = 0;
    [JsonProperty("ranked_and_approved_beatmapset_count")]
    public int RankedAndApprovedBeatmapsetCount { get; set; } = 0;
    [JsonProperty("unranked_beatmapset_count")]
    public int UnrankedBeatmapsetCount { get; set; } = 0;
}


public class Group
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    [JsonProperty("short_name")]
    public string ShortName { get; set; } = string.Empty;
}