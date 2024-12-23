public static partial class FFmpeg
{
    /// <summary>
    /// Extracts the audio stream from a given media file, amplifies it to the specified volume, and saves it to the specified output file.
    /// </summary>
    /// <param name="input">The path to the input media file from which to extract audio.</param>
    /// <param name="output">The path to the output file where the extracted audio will be saved.</param>
    /// <param name="volume">The volume adjustment in decibels to apply to the audio stream.</param>
    public static void ExtractAudioAmplify(string input, string output, float volume)
    {
        const int audioChannels = 2;
        const int audioSampleRate = 48000;

        var command = $"-hide_banner -y -i \"{input}\" -filter:a volume={volume}dB -ac {audioChannels} -ar {audioSampleRate} -vn -sn -dn -map_chapters -1 -map_metadata -1 -c:a flac -f flac \"{output}\"";
        Run(command);
    }
}