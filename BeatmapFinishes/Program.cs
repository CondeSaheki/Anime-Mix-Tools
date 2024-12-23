using MapWizard.BeatmapParser;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("app <path-to-file>");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Files do not exist.");
            return;
        }

        try
        {
            var target = Beatmap.Decode(new FileInfo(filePath)) ?? throw new Exception("Failed to decode target beatmap");
            
            var finishes = GetFinishes(target);
            Console.WriteLine($"count: {finishes.Count}\nfinishers: {string.Join(", ", finishes)}");
            var kiais = GetKiais(target);

            List<TimeSpan> finishersKiai = [];

            foreach (var (start, end) in kiais)
            {
                foreach (var finisher in finishes)
                {
                    if(finisher >= start && finisher <= end) finishersKiai.Add(finisher);
                }
            }

            Console.WriteLine($"count: {finishersKiai.Count}\nfinishers in kiai: {string.Join(", ", finishersKiai)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Returns a list of timestamps where finishers are placed in the given beatmap.
    /// </summary>
    /// <param name="beatmap">The beatmap to retrieve finishers from.</param>
    /// <returns>A list of timestamps where finishers are placed.</returns>
    /// <exception cref="Exception">Thrown if objects cannot be retrieved from the beatmap.</exception>
    public static List<TimeSpan> GetFinishes(Beatmap beatmap)
    {
        var objects = beatmap.HitObjects.Objects ?? [];

        List<TimeSpan> timeStamps = [];

        foreach (var obj in objects)
        {
            if (obj.Type == HitObjectType.Circle && IsFinisher(obj.HitSounds)) timeStamps.Add(obj.Time);

            if (obj.Type == HitObjectType.Slider)
            {
                var slider = obj as Slider ?? throw new Exception("Failed to get slider");
                
                if(IsFinisher(slider.HeadSounds)) timeStamps.Add(slider.Time);

                if(slider.RepeatSounds != null)
                {
                    for (int i = 0; i < slider.RepeatSounds.Count; i++)
                    {
                        var repeatSound = slider.RepeatSounds[i];
                        var delta = slider.EndTime - slider.Time;
                        var time = slider.Time + (delta / slider.Slides) * i;

                        if (IsFinisher(repeatSound)) timeStamps.Add(time);
                    }
                }

                if(IsFinisher(slider.TailSounds)) timeStamps.Add(slider.EndTime);
            }

            if (obj.Type == HitObjectType.Spinner)
            {
                var spinner = obj as Spinner ?? throw new Exception("Failed to get spinner");
                if (IsFinisher(spinner.HitSounds)) timeStamps.Add(spinner.End);
            }
        }
        return timeStamps;
    }

    /// <summary>
    /// Gets a list of kiais in the beatmap.
    /// </summary>
    /// <param name="beatmap">The beatmap to get the kiai ranges from.</param>
    /// <returns>A list of (start, end) kiais.</returns>
    public static List<(TimeSpan start, TimeSpan end)> GetKiais(Beatmap beatmap)
    {
        List<(TimeSpan start, TimeSpan end)> ranges = [];
        TimeSpan? LastKiai = null;
        foreach (var timingPoint in beatmap.TimingPoints?.TimingPointList ?? throw new Exception("Failed to get timing points"))
        {
            if (timingPoint.Effects.Contains(Effect.Kiai))
            {
                if(LastKiai == null) LastKiai = timingPoint.Time;
            }
            else if (LastKiai != null) ranges.Add(((TimeSpan)LastKiai, timingPoint.Time));
        }

        return ranges;
    }

    /// <summary>
    /// Checks if a hit object has a finisher histound, excluding drum finish.
    /// </summary>
    /// <param name="HitSounds"></param>
    /// <returns></returns>
    public static bool IsFinisher((HitSample SampleData, List<HitSound> Sounds) HitSounds)
    {
        if (!(HitSounds.SampleData.NormalSet == SampleSet.Drum && HitSounds.SampleData.AdditionSet == SampleSet.Default) &&
            HitSounds.SampleData.AdditionSet != SampleSet.Drum && HitSounds.Sounds.Contains(HitSound.Finish))
        {
            return true;
        }

        return false;
    }
}