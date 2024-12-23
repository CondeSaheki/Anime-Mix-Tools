# PartNames

## Overview
PartNames is a simple C# utility to validate JSON files in a directory structure. It ensures that each subdirectory contains Entry.json and Parts.json files, and validates that the Name fields in Parts.json match the Mappers field in the corresponding Entry.json.

## Features

- Ensures each subdirectory contains both Entry.json and Parts.json files.
- Validates that every name in Parts.json is listed in the Mappers field of the corresponding Entry.json.
- Outputs discrepancies and errors for quick debugging.

## Requirements
- .NET SDK 9
- Newtonsoft.Json NuGet package for JSON serialization and deserialization.

## Usage
Run the application from the command line:
```
PartNames <folderPath>
```
### Parameters
- `<folderPath>`: folder path containing subdirectories with Entry.json and Parts.json files.

### Example

Given the following directory structure:
```
|-- AnimeProject
    |-- Subdirectory1
        |-- Entry.json
        |-- Parts.json
    |-- Subdirectory2
        |-- Entry.json
        |-- Parts.json
```
Run the program as follows:
```
PartNames AnimeProject
```
### Output

If discrepancies are found, such as a Name in Parts.json not listed in Mappers of Entry.json, they are logged to the console.

If a required file is missing, an exception is thrown with a descriptive error message.

### File Format

- Entry.json
    ```
    {
        "Number": 1,
        "Style": "Simple",
        "Popularity": 42,
        "Mappers": [
            "Mapper1",
            "Mapper2"
        ],
        "Offset": 0,
        "EntryTime": 2500,
        "StartTime": 2000,
        "EndTime": 3000
    }
    ```
- Parts.json
    ```
    [
        {
            "Name": "Mapper1",
            "StartTime": 1000,
            "EndTime": 2000
        },
        {
            "Name": "Mapper3",
            "StartTime": 2000,
            "EndTime": 3000
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
