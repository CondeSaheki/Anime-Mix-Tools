using System.Text.RegularExpressions;

public static partial class FFmpeg
{
    /// <summary>
    /// Analyzes the given audio file and returns the maximum volume in decibels.
    /// </summary>
    /// <param name="input">The path to the input audio file to analyze.</param>
    /// <returns>The maximum volume in decibels, or null if the analysis failed.</returns>
    public static float? AudioMaxVolume(string input)
    {
        var nullOutput = IsWindows() ? "NUL" : "/dev/null";
        var command = $"-i \"{input}\" -filter:a volumedetect -f null {nullOutput}";
        var output = Run(command);
        var pattern = @"max_volume:\s*(-?\d+(\.\d+)?)\s*dB";

        var match = Regex.Match(output, pattern);
        if (match.Success)
        {
            return float.Parse(match.Groups[1].Value);
        }

        return null;
    }
}