# Get User Details V2

This application retrieves information about osu! users, including their ranked, loved, and guest beatmaps, processes usernames according to osu! Ranking Criteria (RC), and generates two output files: `output.csv` to easy exel or google sheet inport and `output.txt` with osu username tags.

## Features

- Fetch user information from the osu! API, including beatmap statuses and history.
- Outputs:
  - **`output.csv`**: Contains user details such as ID, status, username, and previous usernames.
  - **`output.txt`**: Contains processed tags for beatmap creators.
- Process usernames to generate tags according to specific formatting rules.

### Username Processing

Usernames are processed according to these rules:
1. Multiple spaces are replaced with underscores.
2. Single-character parts are combined with adjacent parts using underscores.
3. The result is converted to lowercase.

Example:
- Input: `A B C D`
- Output: `a_b_c_d`


## Requirements

- .NET SDK 9
- osu! API credentials:
  - `OSU_CLIENT_ID`
  - `OSU_CLIENT_SECRET`
- Newtonsoft.Json NuGet package for JSON serialization and deserialization.

## Usage

1. Set up your environment variables:
   ```bash
   export OSU_CLIENT_ID=<your_client_id>
   export OSU_CLIENT_SECRET=<your_client_secret>
   ```
2. Run the application with user IDs as arguments:
   ```bash
   ./app <user_id> [<user_id> ...]
   ```

### Example

```bash
./app 5099768 12253421
```

### Output Files

1. **`output.csv`**
   - CSV format with the following columns:
     - `Id`: User ID
     - `Status`: Community status, user groups
     - `Name`: Current username
     - `PreviousNames`: Comma-separated list of previous usernames
2. **`output.txt`**
   - Plain text file listing processed tags for osu! beatmap creators.

## Build

1. Clone this repository or download the project.
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ``` 
3. Build the project:
   ```bash
   dotnet build -c Release
   ```
