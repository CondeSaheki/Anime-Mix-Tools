# Ly Replacer

## Overview
This application replaces placeholders ("??") in a Ly file with a list of timestamps provided by the user.

## Features
- Validates input file path and timestamps.
- Ensures placeholders match the number of values provided.
- Replaces placeholders in order and saves changes back to the file.

## Requirements
- .NET SDK 9

## Usage
Run the application from the command line:

```bash
app <filePath> <commaSeparatedNumbers>
```

### Parameters
- `<filePath>`: The path to the ly file.
- `<commaSeparatedNumbers>`: An comma separated list of numbers replacements

### Example
```bash
app "Lyrics.ly" "1, 2, 3"
```

## Installation
1. Clone this repository or download the project.
    ```bash
    git clone <link>
    ```
2. Build the application:
   ```bash
   dotnet build -c Release
   ```
