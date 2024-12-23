# Beatmap Finishes

This application processes an osu! beatmap file to extract timestamps for Finishes hitsounds and finishes hitosunds in kiais. It outputs the count of finishers, a list of their timestamps, and the count and list of finishers that fall within kiai periods.

## Prerequisites
- .NET SDK 9
- MapWizard.BeatmapParser dll


## Usage

The application takes a single argument, which is the file path to an osu! `.osu` beatmap file.

### Example

To run the application with a beatmap file, use the following command:

```bash
app <path-to-beatmap-file>
```
### Parameters

- `<path-to-beatmap-file>`: the path to the `.osu` file you want to process.

### Output

The program will output the following information:

- The total number of finishers found in the beatmap.
- A list of timestamps where finishers are located.
- The total number of finishers that occur during kiai periods.
- A list of timestamps where finishers occur within kiai periods.

#### Example output

```
count: 3
finishers: 00:01:23, 00:02:34, 00:04:05
count: 2
finishers in kiai: 00:02:34, 00:04:05
```

## Build

1. Clone this repository or download the project.
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   ```
2. Build the project using the following command:
   ```bash
   dotnet build -c Release
   ```

