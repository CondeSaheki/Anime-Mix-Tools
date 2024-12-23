using static FFmpeg;

class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: app <path-to-file>");
            return;
        }

        var file = args[0];
        if (!File.Exists(file))
        {
            Console.WriteLine($"file \"{file}\", Does not exist");
            return;
        }

        var fileName = Path.GetFileNameWithoutExtension(file);
        var fileDir = Path.GetDirectoryName(file) ?? string.Empty;

        var audio = Path.Combine(fileDir, $"{fileName}_audio.flac");

        var volume = AudioMaxVolume(file) ?? throw new Exception("Failed to get audio volume");
        ExtractAudioAmplify(file, audio, Math.Abs(volume));
    }
}