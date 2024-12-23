using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("app <folder>");
            return;
        }

        string folder = args[0];

        var ok = true;
        foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
        {
            if (Path.GetFileNameWithoutExtension(file) == "Background" &&
                MatchResolution(file))
            {
                ok = false;
                Console.WriteLine($"{file}");
            }
        }
        if (!ok) return;

        foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
        {
            if (Path.GetFileNameWithoutExtension(file) == "Background") CompressToSize(file, 768000);
            if (Path.GetFileNameWithoutExtension(file) == "Cover") CompressToSize(file, 256000);
        }
    }

    /// <summary>
    /// Compresses an image file to JPEG format with a specified compression level.
    /// </summary>
    /// <param name="input">The path to the image file to be compressed.</param>
    /// <param name="quality">The JPEG quality level for compression, ranging from 0 to 100. Default is 99.</param>
    public static void Compress(string input, int quality = 99, string? output = null)
    {
        if (quality < 0 || quality > 100) throw new ArgumentOutOfRangeException(nameof(quality));

        using Image image = Image.Load(input);


        var jpegOptions = new JpegEncoder
        {
            Quality = quality
        };
        image.Save(output ?? Path.ChangeExtension(input, ".jpg"), jpegOptions);
    }

    /// <summary>
    /// Attempts to compress an image to a specified target file size by adjusting the JPEG quality.
    /// </summary>
    /// <param name="file">The path to the image file to be compressed.</param>
    /// <param name="size">The target file size in bytes.</param>
    /// <param name="target">The path to save the compressed file. Defaults to replacing the input file's extension with ".jpg".</param>
    /// <exception cref="Exception">Thrown if the image cannot be compressed to the target size.</exception>
    public static void CompressToSize(string file, long size = 2500000, string? target = null)
    {
        var output = target ??= Path.ChangeExtension(file, ".jpg");

        if (Path.GetExtension(file) == ".jpg" && new FileInfo(file).Length <= size)
        {
            if (target == null) return; // already compressed

            File.Replace(file, output, null);
            if (file != output) File.Delete(file);
        }

        var quality = 99;
        var step = 1;

        var tempFile = Path.Combine(
            Path.GetDirectoryName(output) ?? string.Empty,
            Path.GetFileNameWithoutExtension(output) + "_temp.jpg");

        while (quality > 0)
        {
            Compress(file, quality, tempFile);

            if (new FileInfo(tempFile).Length <= size)
            {
                File.Replace(tempFile, output, null);
                File.Delete(tempFile);
                return;
            }

            quality -= step;
        }

        File.Delete(tempFile);

        throw new Exception($"CompressToSize, file {file}, size {size}: Failed to optimize image to target size.");
    }

    /// <summary>
    /// Checks if the given image matches the given resolution or the default 1920x1080.
    /// </summary>
    /// <param name="file">The path to the image file.</param>
    /// <param name="target">The target resolution to compare to. If not specified, the default is (1920, 1080).</param>
    /// <returns>True if the image match the given or default resolution, false otherwise.</returns>
    public static bool MatchResolution(string file, (int width, int height)? target = null)
    {
        using Image image = Image.Load(file);

        return image.Width == (target?.width ?? 1920) && image.Height == (target?.height ?? 1080);
    }
}
