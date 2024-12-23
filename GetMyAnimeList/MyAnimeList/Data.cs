using Newtonsoft.Json;

namespace MyAnimeList;

public class MainPicture
{
    [JsonProperty("medium")]
    public string Medium { get; set; } = string.Empty;

    [JsonProperty("large")]
    public string Large { get; set; } = string.Empty;
}

public class AlternativeTitles
{
    [JsonProperty("synonyms")]
    public List<string> Synonyms { get; set; } = [];

    [JsonProperty("en")]
    public string En { get; set; } = string.Empty;

    [JsonProperty("ja")]
    public string Ja { get; set; } = string.Empty;
}

public class Genre
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}

public class StartSeason
{
    [JsonProperty("year")]
    public int Year { get; set; }

    [JsonProperty("season")]
    public string Season { get; set; } = string.Empty;
}

public class Broadcast
{
    [JsonProperty("day_of_the_week")]
    public string DayOfTheWeek { get; set; } = string.Empty;

    [JsonProperty("start_time")]
    public string StartTime { get; set; } = string.Empty;
}

public class Studio
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}

public class Status
{
    [JsonProperty("watching")]
    public int Watching { get; set; } = 0;

    [JsonProperty("completed")]
    public int Completed { get; set; } = 0;

    [JsonProperty("on_hold")]
    public int OnHold { get; set; } = 0;

    [JsonProperty("dropped")]
    public int Dropped { get; set; } = 0;

    [JsonProperty("plan_to_watch")]
    public int PlanToWatch { get; set; } = 0;
}

public class Statistics
{
    [JsonProperty("status")]
    public Status Status { get; set; } = new();

    [JsonProperty("num_list_users")]
    public int NumListUsers { get; set; } = 0;
}


public class Anime
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("main_picture")]
    public MainPicture MainPicture { get; set; } = new();

    [JsonProperty("alternative_titles")]
    public AlternativeTitles AlternativeTitles { get; set; } = new();

    [JsonProperty("start_date")]
    public string StartDate { get; set; } = string.Empty;

    [JsonProperty("end_date")]
    public string EndDate { get; set; } = string.Empty;

    [JsonProperty("synopsis")]
    public string Synopsis { get; set; } = string.Empty;

    [JsonProperty("mean")]
    public double Mean { get; set; } = 0;

    [JsonProperty("rank")]
    public int Rank { get; set; } = 0;

    [JsonProperty("popularity")]
    public int Popularity { get; set; } = 0;

    [JsonProperty("num_list_users")]
    public int NumListUsers { get; set; } = 0;

    [JsonProperty("num_scoring_users")]
    public int NumScoringUsers { get; set; } = 0;
    public List<Genre> Genres { get; set; } = [];

    [JsonProperty("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    [JsonProperty("updated_at")]
    public string UpdatedAt { get; set; } = string.Empty;

    [JsonProperty("media_type")]
    public string MediaType { get; set; } = string.Empty;

    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("num_episodes")]
    public int NumEpisodes { get; set; } = 0;

    [JsonProperty("start_season")]
    public StartSeason StartSeason { get; set; } = new();

    [JsonProperty("broadcast")]
    public Broadcast Broadcast { get; set; } = new Broadcast();

    [JsonProperty("source")]
    public string Source { get; set; } = string.Empty;

    [JsonProperty("average_episode_duration")]
    public int AverageEpisodeDuration { get; set; } = 0;

    [JsonProperty("rating")]
    public string Rating { get; set; } = string.Empty;

    [JsonProperty("studios")]
    public List<Studio> Studios { get; set; } = [];

    [JsonProperty("statistics")]
    public Statistics Statistics { get; set; } = new();
}

