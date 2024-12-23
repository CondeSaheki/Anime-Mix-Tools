# Parts Offset

## Overview
This application modifies a JSON file containing a list of parts by adjusting their `StartTime` and `EndTime` fields by a specified number. The updated entries are saved back to the same file.

## Features
- Reads and parses JSON files.
- Adjusts the `StartTime` and `EndTime` fields of each entry by a given integer.
- Ensures the JSON file remains well-formatted after updates.
- Handles errors like missing files or invalid inputs gracefully.

## Requirements
- .NET SDK 9
- Newtonsoft.Json NuGet package for JSON serialization and deserialization.

## Usage
Run the application from the command line:

```bash
app <jsonFilePath> <number>
```

### Parameters
- `<jsonFilePath>`: The path to the JSON file containing the entries.
- `<number>`: An integer value to adjust the `StartTime` and `EndTime` fields.

### Example
```bash
app data.json 10
```
This command adds `10` to the `StartTime` and `EndTime` of all entries in `data.json`.

#### JSON File Format
The input JSON file must contain a list of entries in the following format:

```json
[
  {
    "Name": "Sample Entry",
    "StartTime": 100,
    "EndTime": 200
  },
  {
    "Name": "Another Entry",
    "StartTime": 300
  }
]
```

### Output Example
After running the application with `10` as the adjustment number, the JSON file will be updated as:

```json
[
  {
    "Name": "Sample Entry",
    "StartTime": 110,
    "EndTime": 210
  },
  {
    "Name": "Another Entry",
    "StartTime": 310
  }
]
```

## Installation
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
