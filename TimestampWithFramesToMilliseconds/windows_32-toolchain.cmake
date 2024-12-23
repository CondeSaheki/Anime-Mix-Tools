set(CMAKE_SYSTEM_NAME Windows)

# Specify the target architecture
# Change to i686-w64-mingw32 for 32-bit
set(CMAKE_C_COMPILER i686-w64-mingw32-gcc)
set(CMAKE_CXX_COMPILER i686-w64-mingw32-g++)

# Specify the tool for linker and other utilities
set(CMAKE_RC_COMPILER i686-w64-mingw32-windres)

# Optional: Add custom flags if needed
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -static")
set(CMAKE_EXE_LINKER_FLAGS "${CMAKE_EXE_LINKER_FLAGS} -static")
