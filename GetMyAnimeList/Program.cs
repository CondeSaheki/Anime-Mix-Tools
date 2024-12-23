using System.Text;
using System.Text.RegularExpressions;
using MyAnimeList;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string? token = Environment.GetEnvironmentVariable("MAL_CLIENT_ID");
        if (token == null)
        {
            Console.WriteLine("MAL_CLIENT_ID environment variable is not set");
            return;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("app <animeId> [<animeId> ...]");
            return;
        }

        List<int> animeIds = [];

        foreach (var arg in args)
        {
            if (int.TryParse(arg, out int animeId))
            {
                animeIds.Add(animeId);
                continue;
            }
            Console.WriteLine($"Invalid animeId: {arg}");
            return;
        }

        MyAnimeListAPI myAnimeListAPI = new(token);

        var animes = GetAnimes(myAnimeListAPI, animeIds);

        ProcessAnimeData(animes);
    }

    public static List<Anime> GetAnimes(MyAnimeListAPI myAnimeListAPI, List<int> animeIds)
    {
        List<Anime> animes = [];
        foreach (var animeId in animeIds)
        {
            var anime = myAnimeListAPI.GetAnimeDetails(animeId).Result ?? throw new Exception($"Failed to get anime, {animeId}, information.");
            
            var path = $"{LegalizeString(anime.Title)}";

            Directory.CreateDirectory(path);

            var json = JsonConvert.SerializeObject(anime, Formatting.Indented);
            File.WriteAllText(Path.Combine(path, $"Anime.json"), json.ToString());

            myAnimeListAPI.DownloadImages(anime, path).Wait();

            animes.Add(anime);
        }
        return animes;
    }

    public static void ProcessAnimeData(List<Anime> animes)
    {
        var titles = new HashSet<string>();
        var genres = new HashSet<string>();
        var studios = new HashSet<string>();
        StringBuilder sb = new();

        foreach (var anime in animes)
        {
            var currentTitles = new HashSet<string>();
            var currentGenres = new HashSet<string>();
            var currentStudios = new HashSet<string>();

            // titles
            ProcessTag(anime.Title).ForEach(tag => currentTitles.Add(tag));
            ProcessTag(anime.AlternativeTitles.En).ForEach(tag => currentTitles.Add(tag));
            ProcessTag(anime.AlternativeTitles.Ja).ForEach(tag => currentTitles.Add(tag));
            anime.AlternativeTitles.Synonyms.ForEach(synonym => ProcessTag(synonym).ForEach(tag => currentTitles.Add(tag)));

            // genres
            anime.Genres.ForEach(genre => ProcessTag(genre.Name).ForEach(tag => currentGenres.Add(tag)));

            // studios
            anime.Studios.ForEach(studio => ProcessTag(studio.Name).ForEach(tag => currentStudios.Add(tag)));

            sb.AppendLine("titles: " + string.Join(' ', currentTitles));
            sb.AppendLine("genres: " + string.Join(' ', currentGenres));
            sb.AppendLine("studios: " + string.Join(' ', currentStudios));
            sb.AppendLine("-----");

            titles.UnionWith(currentTitles);
            genres.UnionWith(currentGenres);
            studios.UnionWith(currentStudios);
        }

        var tags = new HashSet<string>();
        tags.UnionWith(titles);
        tags.UnionWith(genres);
        tags.UnionWith(studios);

        sb.AppendLine("titles: " + string.Join(' ', titles));
        sb.AppendLine("genres: " + string.Join(' ', genres));
        sb.AppendLine("studios: " + string.Join(' ', studios));
        sb.AppendLine("tags: " + string.Join(' ', tags));

        // Write to output file
        File.WriteAllText("output.txt", sb.ToString());
    }

    /// <summary>
    /// Processes tags with RC requirements
    /// Multiple spaces are replaced by underscores.
    /// Single character parts are combined with adjacent parts with an underscore.
    /// The result is converted to lowercase.
    /// </summary>
    /// <param name="username">The username to process.</param>
    /// <returns>A list of tags.</returns>
    public static List<string> ProcessTag(string username)
    {
        var result = Regex.Replace(username.Trim(), @"\s{2,}", static match => new string('_', match.Length));

        var parts = new List<string>(result.Split(' '));

        for (int i = 0; i < parts.Count - 1;)
        {
            if (parts[i].Length == 1 || parts[i + 1].Length == 1)
            {
                parts[i] += $"_{parts[i + 1]}";
                parts.RemoveAt(i + 1);
                i = 0;
                continue;
            }

            i++;
        }
        return parts.Select(static part => part.ToLowerInvariant()).ToList();
    }

    public static string LegalizeString(string input)
    {
        // ':', '?', '\"', '<', '>', '|', '*', '½'
        var allowedChars = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-+[](){}".ToCharArray();
        return new string(input.Where(c => allowedChars.Contains(c)).ToArray());
    }

}

