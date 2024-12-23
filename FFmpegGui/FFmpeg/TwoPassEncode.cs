public static partial class FFmpeg
{
    /// <summary>
    /// Performs a two-pass encoding process on the given input media file.
    /// </summary>
    /// <param name="input">The path to the input media file to be encoded.</param>
    /// <param name="output">The path to the output file where the encoded media will be saved.</param>
    /// <param name="bitrate">The video bitrate in kilobits per second. Default is 2327 kbps.</param>
    /// <param name="resolution">The target resolution for the video encoding, specified as a string (e.g., "1280x720"). Default is "1280x720".</param>
    public static void TwoPassEncode(string input, string output, int bitrate = 2327, string resolution = "1280x720")
    {
        var nullOutput = IsWindows() ? "NUL" : "/dev/null";

        const string fps = "23.976";

        const int audioqscale = 6;
        const int audioChannels = 2;
        const int audioSampleRate = 48000;

        string pass1 = $"-hide_banner -y -i \"{input}\" -map 0:1 -c:a libvorbis -qscale:a {audioqscale} -ac {audioChannels} -ar {audioSampleRate} -map 0:0 -c:v libx264 -s {resolution} -r {fps} -b:v {bitrate}k -maxrate {bitrate * 2}k -tune fastdecode -profile:v high -bufsize {bitrate * 2}k -pix_fmt yuv420p -map_metadata -1 -sn -dn -map_chapters -1 -pass 1 -f null {nullOutput}";
        string pass2 = $"-hide_banner -y -i \"{input}\" -map 0:1 -c:a libvorbis -qscale:a {audioqscale} -ac {audioChannels} -ar {audioSampleRate} -map 0:0 -c:v libx264 -s {resolution} -r {fps} -b:v {bitrate}k -maxrate {bitrate * 2}k -tune fastdecode -profile:v high -bufsize {bitrate * 2}k -pix_fmt yuv420p -map_metadata -1 -sn -dn -map_chapters -1 -pass 2 \"{output}\"";

        Run(pass1);
        Run(pass2);
    }
}