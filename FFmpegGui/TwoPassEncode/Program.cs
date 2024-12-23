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

        var mixed = Path.Combine(fileDir, $"{fileName}_mixed.mkv");
        var audio = Path.Combine(fileDir, $"{fileName}_audio.ogg");
        var video = Path.Combine(fileDir, $"{fileName}_video.flv");

        TwoPassEncode(file, mixed);
        ExtractAudio(mixed, audio);
        ExtractVideo(mixed, video);

        if (File.Exists(mixed)) File.Delete(mixed);
    }
}