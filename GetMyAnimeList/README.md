# Get MyAnimeList

This application retrieves detailed information about anime from MyAnimeList using their public API. It saves the information as a JSON file and download anime cover image.
Useful information is extracted such as titles, genres, studios, and tags them aggregated and processed in a tag syle format suitable searching engines.

## Features

- Fetches anime details such as title, synopsis, genres, studios, ratings, and more.
- Downloads anime cover images (medium and large sizes).
- Organizes data in a directory for each anime.
- Extracts, process and aggregate:
  - Titles (main, English, Japanese, and synonyms).
  - Genres.
  - Studios.
  - Tags (unique words from titles, genres, and studio names).
  Outputs the data to `output.txt`.

## Prerequisites
- .NET SDK 9
- Newtonsoft.Json NuGet package for JSON serialization and deserialization.
- MyAnimeList credentials
    - `MAL_CLIENT_ID`

## Usage

1. Set the required environment variables:
    ```bash
        export MAL_CLIENT_ID=<your_client_id>
    ```
2. Run the program with one or more anime IDs as arguments:
    ```bash
    app <animeId1> [<animeId2> ...] 
    ```

### Example

To retrieve details for anime with IDs 12345:
    ```
    app 12345
    ```
### Output
A folder with the anime ID and name will be created containing the cover images and MyAnimeList information JSON.
```
.
|-- Hand\ Maid\ Mai
|   |-- Anime.json
|   `-- Cover.png

```

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

