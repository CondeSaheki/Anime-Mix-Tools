public static partial class FFmpeg
{
    /// <summary>
    /// Extracts the audio stream from a given media file and saves it to the specified output file.
    /// </summary>
    /// <param name="input">The path to the input media file from which to extract audio.</param>
    /// <param name="output">The path to the output file where the extracted audio will be saved.</param>
    public static void ExtractAudio(string input, string output)
    {
        string command = $"-hide_banner -y -i \"{input}\" -vn -sn -dn -map_chapters -1 -map_metadata -1 -c:a copy \"{output}\"";
        Run(command);
    }
}