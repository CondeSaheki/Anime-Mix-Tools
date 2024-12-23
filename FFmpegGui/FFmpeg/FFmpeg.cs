using System.Runtime.InteropServices;
using System.Diagnostics;

public static partial class FFmpeg
{
    private static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    private static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

    private static string? FFmpegBinary()
    {
        if (IsWindows() && File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe"))) return "ffmpeg.exe";
        if (IsLinux()) return "ffmpeg";
        return null;
    }

    private static string Run(string args)
    {
        const bool debug = true;

        var binary = FFmpegBinary() ?? throw new Exception("FFmpeg binary not found.");
        
        if (debug) File.AppendAllText("Debug.log", $"{binary} {args}\n\n");
        
        var config = new ProcessStartInfo
        {
            FileName = binary,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(config) ?? throw new Exception("Failed to start FFmpeg process.");

        string standardOutput = process.StandardOutput.ReadToEnd();
        string standardError = process.StandardError.ReadToEnd();

        process.WaitForExit();
        string combinedOutput = standardOutput + standardError + "\n";

        if (debug) File.AppendAllText("Debug.log", combinedOutput);

        return combinedOutput;
    }
}