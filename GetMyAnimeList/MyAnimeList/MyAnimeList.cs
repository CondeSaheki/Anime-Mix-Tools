using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace MyAnimeList;

public class MyAnimeListAPI
{
    private readonly HttpClient client;
    private readonly string token;

    public MyAnimeListAPI(string ClientId)
    {
        token = ClientId;
        client = new();
        client.DefaultRequestHeaders.Add("X-MAL-CLIENT-ID", $"{token}");
    }

    public async Task<Anime> GetAnimeDetails(int animeId)
    {
        string url = $"https://api.myanimelist.net/v2/anime/{animeId}?fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,rank,popularity,num_list_users,num_scoring_users,nsfw,genres,created_at,updated_at,media_type,status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,studios,statistics";

        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception($"Request failed with status code: {response.StatusCode}");

        string responseBody = await response.Content.ReadAsStringAsync();
        
        var animeDetails = JsonConvert.DeserializeObject<Anime>(responseBody) ?? throw new Exception("Failed to deserialize JSON");

        return animeDetails;
    }

    public async Task DownloadImages(Anime animeDetails, string saveDirectory)
    {
        // string mediumUrl = animeDetails.MainPicture.Medium;
        string largeUrl = animeDetails.MainPicture.Large;
        
        Directory.CreateDirectory(saveDirectory);

        // await DownloadImage(mediumUrl, Path.Combine(saveDirectory, "medium_image.png"));
        await DownloadImage(largeUrl, Path.Combine(saveDirectory, "Cover.png"));
    }

    private async Task DownloadImage(string url, string filePath)
    {
        HttpResponseMessage response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode) throw new Exception($"Failed to download image from {url}");
        
        using var imageStream = new MemoryStream(await response.Content.ReadAsByteArrayAsync());
        using Image image = Image.Load(imageStream);

        await image.SaveAsPngAsync(filePath);
    }
}
