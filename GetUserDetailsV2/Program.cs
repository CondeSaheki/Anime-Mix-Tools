using System.Text;
using System.Text.RegularExpressions;
using osuAPI;
using static osuAPI.OsuApi;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("app <user_id> [<user_id> ...]");
            return;
        }

        var clientId = Environment.GetEnvironmentVariable("OSU_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("OSU_CLIENT_SECRET");

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            Console.WriteLine("Error: Environment variables OSU_CLIENT_ID or OSU_CLIENT_SECRET are not set.");
            return;
        }
        
        List<int> userIds = [];

        try
        {
            userIds = args.Select(int.Parse).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing user IDs: {ex.Message}");
            return;
        }
        
        var OsuApi = new OsuApi(clientId, clientSecret);

        HashSet<string> beatmapTags = [];
        StringBuilder sb = new();
        sb.AppendLine($"\"Id\",\"Status\",\"Name\",\"PreviousNames\"");

        foreach (var userId in userIds)
        {
            var user = OsuApi.GetUser(userId).Result;

            var status = "Unranked";
            if (user.GuestBeatmapsetCount != 0) status = "Guest";
            if (user.LovedBeatmapsetCount != 0) status = "Loved";
            if (user.RankedBeatmapsetCount != 0) status = "Ranked";
            if (user.Groups.Count != 0) status = string.Join(", ", user.Groups.Select(g => g.Name));

            beatmapTags.Add(user.Username);

            sb.AppendLine($"\"{userId}\",\"{status}\",\"{user.Username}\",\"{string.Join(", ", user.PreviousUsernames)}\"");


            if (user.PreviousUsernames == null) continue; // do not need to check user history

            if (user.RankedBeatmapsetCount != 0)
            {
                var ranked = OsuApi.GetUserBeatmaps(userId, BeatmapType.ranked).Result;
                foreach (var beatmapset in ranked)
                {
                    if (user.PreviousUsernames.Contains(beatmapset.Creator))
                    {
                        beatmapTags.Add(beatmapset.Creator);
                    }
                }
            }

            if (user.LovedBeatmapsetCount != 0)
            {
                var loved = OsuApi.GetUserBeatmaps(userId, BeatmapType.loved).Result;
                foreach (var beatmapset in loved)
                {
                    if (user.PreviousUsernames.Contains(beatmapset.Creator))
                    {
                        beatmapTags.Add(beatmapset.Creator);
                    }
                }
            }

            if (user.GuestBeatmapsetCount != 0)
            {
                var guest = OsuApi.GetUserBeatmaps(userId, BeatmapType.guest).Result;
                foreach (var beatmapset in guest)
                {
                    foreach (var username in user.PreviousUsernames)
                    {
                        if (beatmapset.Tags.Contains(username))
                        {
                            beatmapTags.Add(username);
                        }
                    }

                }
            }
        }

        beatmapTags = beatmapTags.SelectMany(ProcessName).ToHashSet();

        File.WriteAllText("output.csv", sb.ToString());
        File.WriteAllText("output.txt", $"OsuUser Tags: {string.Join(' ', beatmapTags)}");
    }
    
    /// <summary>
    /// Processes an osu username with RC requirements
    /// Multiple spaces are replaced by underscores.
    /// Single character parts are combined with adjacent parts with an underscore.
    /// The result is converted to lowercase.
    /// </summary>
    /// <param name="username">The username to process.</param>
    /// <returns>A list of tags.</returns>
    public static List<string> ProcessName(string username)
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
}