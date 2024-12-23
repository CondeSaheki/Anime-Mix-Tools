public static partial class FFmpeg
{
    /// <summary>
    /// Extracts the video stream from a given media file and saves it to the specified output file.
    /// </summary>
    /// <param name="input">The path to the input media file from which to extract video.</param>
    /// <param name="output">The path to the output file where the extracted video will be saved.</param>
    public static void ExtractVideo(string input, string output)
    {
        string command = $"-hide_banner -y -i \"{input}\" -an -sn -dn -map_chapters -1 -map_metadata -1 -c:v copy \"{output}\"";
        Run(command);
    }
}