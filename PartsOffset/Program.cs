using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    class Part
    {
        public string Name { get; set; } = "";
        public int StartTime { get; set; } = 0;
        public int? EndTime { get; set; } = null;
    }

    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: app <jsonFilePath> <number>");
            return;
        }

        string jsonFilePath = args[0];
        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine("Error: The file does not exist.");
            return;
        }

        if (!int.TryParse(args[1], out int number))
        {
            Console.WriteLine("Error: The second argument must be a valid integer.");
            return;
        }

        try
        {
            string json = File.ReadAllText(jsonFilePath);
            var entries = JsonConvert.DeserializeObject<List<Part>>(json) ?? throw new Exception("Failed to deserialize JSON.");

            entries = entries.OrderBy(e => e.StartTime).ToList();

            foreach (var entry in entries)
            {
                entry.StartTime += number;
                if (entry.EndTime != null)
                {
                    entry.EndTime += number;
                }
            }

            string modifiedJson = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(jsonFilePath, modifiedJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
