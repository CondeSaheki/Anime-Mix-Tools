# Timestamp with frames to Milliseconds

This application converts a timestamp in the `HH:MM:SS:frames` format into milliseconds, assuming a frame rate of **23.976 FPS**.
For help, run the program without arguments or with the --help flag.

## Features

- Converts timestamps in `HH:MM:SS:Frames` format to milliseconds.
- Assumes a frame rate of **23.976 FPS**, commonly used in video editing.
- Converts and sort a list of timestamps

## Prerequisites

- **C++ Compiler:** Ensure you have a modern C++ compiler installed (e.g., GCC, Clang, or MSVC).
- **CMake:** A build system generator (minimum version X.X recommended).

## Usage

```bash
app <timestamp> [<timestamp>...]
```

### Parameters

- '<timestamp>' The timestamp in the `HH:MM:SS:frames` format

### Example

To convert `00:03:06:02` and `00:03:00:15`
```
app 00:03:06:02 00:03:00:15
```

### Output

```
180626
186083
```

## Build
1. Clone this repository or download the project.
    ```bash  
    git clone <repository-link>  
    cd <repository-folder>  
    ```
    
2. Create a `build` directory:
    ```bash  
    mkdir build  
    cd build  
    ```
    
3. Use CMake to configure and build the application:
    ```bash  
    cmake ..  
    make  
    ```

#### Cross-Compile for Windows  
For cross-compilation, use the appropriate toolchain file:

- **64-bit Windows**
    ```bash  
    cmake .. -DCMAKE_TOOLCHAIN_FILE=../windows-64-toolchain.cmake  
    ```

- **32-bit Windows**
    ```bash  
    cmake .. -DCMAKE_TOOLCHAIN_FILE=../windows-32-toolchain.cmake  
    ```
