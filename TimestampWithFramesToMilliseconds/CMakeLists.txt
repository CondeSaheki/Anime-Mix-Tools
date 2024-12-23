cmake_minimum_required(VERSION 3.15)
project(MyApp VERSION 1.0 LANGUAGES CXX)

# Set C++ standard
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED True)

# Add the executable
add_executable(TimestampWithFramesToMilliseconds main.cpp)
