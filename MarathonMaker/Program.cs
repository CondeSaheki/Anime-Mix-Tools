using MapWizard.BeatmapParser;
using Newtonsoft.Json;

class Program
{
    public class Entry
    {
        public string Beatmap { get; set; } = string.Empty;
        public int Offset { get; set; } = 0;
    }

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: app <path-to-file1> <path-to-file2>");
            return;
        }

        string beatmapPath = args[0];
        string jsonPath = args[1];

        if (!File.Exists(beatmapPath) || !File.Exists(jsonPath))
        {
            Console.WriteLine("One or both of the specified files do not exist.");
            return;
        }

        try
        {
            var target = Beatmap.Decode(new FileInfo(beatmapPath)) ?? throw new Exception("Failed to decode target beatmap");

            var entries = JsonConvert.DeserializeObject<List<Entry>>(File.ReadAllText(jsonPath)) ?? throw new Exception("Failed to deserialize entries");
            var beatmaps = entries.Select(entry => (Beatmap.Decode(new FileInfo(entry.Beatmap)) ?? throw new Exception("Failed to decode beatmap"), TimeSpan.FromMilliseconds(entry.Offset))).ToList();

            var result = MergeBeatmaps(target, beatmaps);
            result.Metadata.Version = "Compiled";

            File.WriteAllText($"{result.Metadata.Version}.osu", result.Encode());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Merges multiple beatmaps with the target beatmap metadata and configurations.
    /// </summary>
    /// <param name="target">The target beatmap to merge into.</param>
    /// <param name="beatmaps">A list of tuples containing the beatmap and offset to apply to the timing points and hit objects.</param>
    /// <returns>The target beatmap with the merged timing points and hit objects.</returns>
    public static Beatmap MergeBeatmaps(Beatmap target, List<(Beatmap Beatmap, TimeSpan Offset)> beatmaps)
    {
        var result = target;
        result.TimingPoints = new TimingPoints();
        result.HitObjects = new HitObjects();

        foreach (var entry in beatmaps)
        {
            var timingPoints = entry.Beatmap.TimingPoints?.TimingPointList ?? throw new Exception("Failed to get timing points");
            var hitObjects = entry.Beatmap.HitObjects?.Objects ?? throw new Exception("Failed to get hit objects");

            foreach (var timingPoint in timingPoints) timingPoint.Time += entry.Offset;

            foreach (var hitObject in hitObjects)
            {
                hitObject.Time += entry.Offset;
                if (hitObject.Type == HitObjectType.Spinner) ((Spinner)hitObject).End += entry.Offset;
            }

            result.TimingPoints.TimingPointList.AddRange(timingPoints);
            result.HitObjects.Objects.AddRange(hitObjects);
        }
        return result;
    }
}