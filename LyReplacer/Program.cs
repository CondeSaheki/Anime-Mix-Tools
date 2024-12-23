using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Ensure correct number of arguments
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Program <filePath> <commaSeparatedNumbers>");
            return;
        }

        string filePath = args[0];
        string inputNumbers = args[1];

        // Validate file existence
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: File not found. Please provide a valid file path.");
            return;
        }

        // Parse the input numbers
        int[] numbers;
        try
        {
            numbers = inputNumbers.Split(',')
                                  .Select(s => int.Parse(s.Trim()))
                                  .ToArray();
        }
        catch
        {
            Console.WriteLine("Error: Invalid number format. Please provide a valid comma-separated list of numbers.");
            return;
        }

        try
        {
            // Read the file content
            string content = File.ReadAllText(filePath);

            // Count the placeholders ("??")
            int placeholderCount = content.Split("??").Length - 1;

            if (placeholderCount != numbers.Length)
            {
                Console.WriteLine($"Error: File contains {placeholderCount} placeholders but {numbers.Length} numbers were provided.");
                return;
            }

            // Replace the placeholders with the numbers in order
            for (int i = 0; i < numbers.Length; i++)
            {
                content = content.ReplaceFirst("??", numbers[i].ToString());
            }

            // Save the changes back to the file
            File.WriteAllText(filePath, content);

            Console.WriteLine("Placeholders replaced and changes saved to the file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: An error occurred - {ex.Message}");
        }
    }
}

static class StringExtensions
{
    // Helper method to replace the first occurrence of a substring
    public static string ReplaceFirst(this string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
