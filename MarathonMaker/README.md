
# Beatmap Merger

This application merges multiple osu! beatmaps into a single target beatmap, adjusting timing points and hit objects with offsets. The result is a compiled beatmap that combines all the provided beatmaps.

## Features

- Merge multiple beatmaps with specified offsets.
- Adjust timing points and hit objects based on the given offsets.
- Outputs a merged beatmap file in `.osu` format with the name set to "Compiled".

## Prerequisites

- .NET SDK 9
- Newtonsoft.Json NuGet package for JSON serialization and deserialization.
- MapWizard.BeatmapParser dll

## Usage

To use the program, provide two arguments:
```bash
app <path_file_osu> <path_file_json>
```

### Parameters
- '<path_file_osu>' The path to the target beatmap (the one that other beatmaps will be merged into).
- '<path_file_json>' The path to a JSON file containing a list of beatmap paths and their respective offsets.

### Example

```bash
app /path/to/target.osu /path/to/entries.json
```

Where `entries.json` contains an array of `Entry` objects, each with:

- `Beatmap`: Path to an osu! beatmap file.
- `Offset`: Time offset to apply to the beatmap's timing points and hit objects (in milliseconds).

**Example** `entries.json`:

```json
[
    {
        "Beatmap": "/path/to/beatmap1.osu",
        "Offset": 3000
    },
    {
        "Beatmap": "/path/to/beatmap2.osu",
        "Offset": 94000
    }
]
```

### Output

The program will create a merged beatmap with the name `Compiled.osu` in the current directory.

## Build

1. Clone this repository or download the project.
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   ```
2. Restore dependencies
   ```bash
   dotnet restore
   ``` 
3. Build the project:
   ```bash
   dotnet build -c Release
   ```
