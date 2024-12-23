using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuAPI.Data;

namespace osuAPI;

public class OsuApi
{
    private readonly string Token;
    private readonly string ClientId;
    private readonly string ClientSecret;
    private readonly HttpClient HttpClient = new();
    private const string BaseApiUrl = "https://osu.ppy.sh/api/v2";

    public OsuApi(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        Token = GetAccessToken().Result;
    }

    private async Task<string> GetAccessToken()
    {
        const string TokenUrl = "https://osu.ppy.sh/oauth/token";

        var requestBody = new StringContent(
            $"client_id={ClientId}&client_secret={ClientSecret}&grant_type=client_credentials&scope=public",
            Encoding.UTF8,
            "application/x-www-form-urlencoded"
        );

        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await HttpClient.PostAsync(TokenUrl, requestBody);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        

        var tokenResponse = JsonConvert.DeserializeObject<JObject>(responseBody) ?? throw new Exception("Failed to deserialize response");

        return tokenResponse["access_token"]?.ToString() ?? throw new Exception("Failed to get access token");
    }

    public async Task<OsuUser> GetUser(int userId, string? mode = null)
    {
        // Construct the URL
        string url = $"{BaseApiUrl}/users/{userId}";
        if (!string.IsNullOrEmpty(mode))
        {
            url += $"/{mode}";
        }

        // Set up headers
        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Make the GET request
        HttpResponseMessage response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        
        var userResponse = JsonConvert.DeserializeObject<OsuUser>(responseBody) ?? throw new Exception("Failed to deserialize response");

        return userResponse;
    }

    public async Task<List<OsuBeatmapset>> GetUserBeatmaps(int userId, BeatmapType type, int? limit = null, int? offset = null)
    {
        string url = $"{BaseApiUrl}/users/{userId}/beatmapsets/{type}";

        if (limit.HasValue || offset.HasValue)
        {
            List<string> queryParameters = [];
            if (limit.HasValue) queryParameters.Add($"limit={limit}");
            if (offset.HasValue) queryParameters.Add($"offset={offset}");
            url += "?" + string.Join("&", queryParameters);
        }

        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        
        var beatmaps = JsonConvert.DeserializeObject<List<OsuBeatmapset>>(responseBody) ?? throw new Exception("Failed to deserialize response");

        return beatmaps;
    }

    public enum BeatmapType
    {
        favourite, 	
        graveyard, 	
        guest, 	
        loved, 	
        most_played, 	
        nominated, 	
        pending,
        ranked,
    };

}
