# Image Compression Utility

This application is a command-line tool for compressing image files within a specified folder, targeting files named **"Background"** and **"Cover"**. It ensures that images meet specified resolutions and compresses them to target sizes.

The application check Backgrounds, must match the resolution of **1920x1080** before compression.

The application targets files with specific names and compress them to target sizes:
- **"Background"**: Compressed to a maximum size of 768 KB.
- **"Cover"**: Compressed to a maximum size of 256 KB.

## Features

- **Compression to JPEG format**: Converts images to JPEG with adjustable quality.
- **Target size compression**: Compresses images to a specific file size by adjusting JPEG quality.
- **Resolution check**: Validates that images match the required resolution (default: 1920x1080).
- **Batch processing**: Processes all images in a folder and its subdirectories.

## Requirements

- .NET 9 SDK
- ImageSharp library.

## Usage

To run the application, use the following command:
```bash
app <folder>
```

### Parameters
- `<folder>`: The path to the folder containing images to be processed.

### Example

```bash
app "/sb/animes"
```
This will process all images in the specified folder and its subdirectories.

## Build

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/image-compression-utility.git
   cd image-compression-utility
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```
