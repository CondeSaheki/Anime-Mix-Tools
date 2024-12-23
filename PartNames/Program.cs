using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PartNames
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                Console.WriteLine("Please provide a valid folder path as a command line argument.");
                return;
            }

            string folderPath = args[0];

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Invalid folder path.");
                return;
            }

            var directories = Directory.GetDirectories(folderPath);

            foreach (var directory in directories)
            {
                string entryFile = Path.Combine(directory, "Entry.json");
                string partsFile = Path.Combine(directory, "Parts.json");

                if (!File.Exists(entryFile) || !File.Exists(partsFile))
                {
                    throw new Exception($"Both Entry.json and Parts.json must exist in directory: {directory}");
                }

                try
                {
                    // entryFile

                    string entryContent = File.ReadAllText(entryFile);
                    var entry = JsonConvert.DeserializeObject<Entry>(entryContent) ?? throw new Exception($"Invalid JSON format in file: {entryFile}");
                    if (entry.Mappers.Count == 0) throw new Exception($"Invalid JSON format in file: {entryFile}");

                    // partsFile

                    string partsContent = File.ReadAllText(partsFile);
                    var parts = JsonConvert.DeserializeObject<List<Part>>(partsContent) ?? throw new Exception($"Invalid JSON format in file: {partsFile}");

                    // Check each Part in Parts.json against the Mappers in Entry.json

                    foreach (var part in parts)
                    {
                        if (!entry.Mappers.Contains(part.Name))
                        {
                            Console.WriteLine($"{part.Name} | {partsFile}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing files in directory {directory}: {ex.Message}");
                }
            }
        }

        public class Part
        {
            public string Name { get; set; } = string.Empty;
            public int StartTime { get; set; } = 0;
            public int? EndTime { get; set; } = null;
        }

        public class Entry
        {
            public int Number { get; set; } = 0;
            public string AnimeName { get; set; } = string.Empty;
            public string Style { get; set; } = string.Empty;
            public int Popularity { get; set; } = 0;
            public List<string> Mappers { get; set; } = [];
            public int Offset { get; set; } = 0;
            public int EntryTime { get; set; } = 0;
            public int StartTime { get; set; } = 0;
            public int EndTime { get; set; } = 0;
        }
    }
}
