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

        var video480p = Path.Combine(fileDir, $"{fileName}_480p.flv");
        var video360p = Path.Combine(fileDir, $"{fileName}_360p.flv");
        var video240p = Path.Combine(fileDir, $"{fileName}_240p.flv");
        var video180p = Path.Combine(fileDir, $"{fileName}_180p.flv");

        // 480p
        TwoPassEncode(file, mixed, 1082, "854x480");
        ExtractVideo(mixed, video480p);

        // 360p
        TwoPassEncode(file, mixed, 608, "640x360");
        ExtractVideo(mixed, video360p);

        // 240p
        TwoPassEncode(file, mixed, 270, "426x240");
        ExtractVideo(mixed, video240p);

        // 180p
        TwoPassEncode(file, mixed, 152, "320x180");
        ExtractVideo(mixed, video180p);

        if (File.Exists(mixed)) File.Delete(mixed);
    }
}