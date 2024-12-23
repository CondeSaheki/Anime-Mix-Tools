# FFmpeg GUI Solution

A modular C# solution of projects that interacts with ffmpeg, ffplay and ffprobe.

## Features
- Modular design with reusable components.
- CLI for quick interactions.

## Project Structure

1. **FFmpeg**
   The core library that contains the FFmpeg interaction logic. This library is reusable across other applications.

2. **ExtractAmplifyAudio**
   A command-line application to do the extraction of track audio and its amplification to a max of `0 db`.

3. **BashEncode**
   A command-line application that perform a two pass encoding in multiple quality

4. **TwoPassEncode**
   A command-line application that perform a two pass encoding and them splits of audio and video tracks

### Prerequisites
- .NET SDK 9
- ffmpeg binaries

## Build
Run the following command to build the entire solution:
1. Clone this repository or download the project.
   ```bash
   git clone <repository-url>
   cd <repository-directory>
   ```
2. Build the project:
   ```bash
   dotnet build -c Release
   ```

# TODO
- GUI for user-friendly operations.
