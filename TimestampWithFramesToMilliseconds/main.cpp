#include <iostream>
#include <chrono>
#include <type_traits>
#include <filesystem>

/**
 * Rounds a given number to the nearest integer.
 * If the decimal part is exactly 0.5, the number is rounded up.
 *
 * @tparam T The type of the number to be rounded.
 * @param num The number to be rounded.
 * @return The rounded integer value.
 */
template <typename framerate>
[[nodiscard]] constexpr int round(const framerate &num) noexcept
{
	int temp = static_cast<int>(num);

	if (num - temp >= 0.5)
	{
		return temp + 1;
	}

	return temp;
}

namespace sch = std::chrono;

using fps23_976 = sch::duration<int, std::ratio<1001, 24000>>;
using fps24 = sch::duration<int, std::ratio<1000, 24000>>;
using fps25 = sch::duration<int, std::ratio<1000, 25000>>;
using fps29_97 = sch::duration<int, std::ratio<1001, 30000>>;
using fps30 = sch::duration<int, std::ratio<1000, 30000>>;
using fps48 = sch::duration<int, std::ratio<1000, 48000>>;
using fps50 = sch::duration<int, std::ratio<1000, 50000>>;
using fps59_94 = sch::duration<int, std::ratio<1001, 60000>>;
using fps60 = sch::duration<int, std::ratio<1000, 60000>>;
using fps120 = sch::duration<int, std::ratio<1000, 120000>>;

using milliseconds_double = sch::duration<double, std::ratio<1, 1000>>;

// this concept is broken for some reason 
template <typename type>
concept sch_duration = std::is_base_of_v<std::chrono::duration<typename type::rep, typename type::period>, type>;

/**
 * Converts a given number of frames to milliseconds based on the specified framerate.
 *
 * @tparam framerate The duration type representing the framerate (e.g., fps24, fps30).
 * @param value The number of frames to convert.
 * @return The equivalent duration in milliseconds.
 */
template <typename framerate> // typename -> sch_duration 
[[nodiscard]] constexpr sch::milliseconds frames_to_milliseconds(const int &value) noexcept
{
	auto frames = framerate(value);
	milliseconds_double frames_milliseconds_double = static_cast<milliseconds_double>(frames);
	sch::milliseconds milliseconds(round(frames_milliseconds_double.count()));
	return milliseconds;
}

/**
 * Converts a given duration in milliseconds to the equivalent number of frames based on the specified framerate.
 *
 * @tparam framerate The duration type representing the framerate (e.g., fps24, fps30).
 * @param value The duration in milliseconds to convert.
 * @return The equivalent duration in frames as the specified framerate.
 */
template <typename framerate> // typename -> sch_duration 
[[nodiscard]] constexpr framerate milliseconds_to_frames(const sch::milliseconds &milliseconds) noexcept
{
	return static_cast<framerate>(milliseconds);
}

/**
 * Converts a timestamp in HH:MM:SS:FF format to milliseconds, given 23.976 FPS.
 * @param timestamp The timestamp to convert.
 * @return The equivalent time in milliseconds.
 */
sch::milliseconds timestamp_to_milliseconds(const std::string &timestamp)
{
    int hours, minutes, seconds, frames;
    char separator;

    std::stringstream timestampStream(timestamp);
    timestampStream >> hours >> separator >> minutes >> separator >> seconds >> separator >> frames;

    return sch::hours(hours) + sch::minutes(minutes) + sch::seconds(seconds) + frames_to_milliseconds<fps23_976>(frames);
}

int main(int argc, char* argv[])
{
    // Extract program name (file name only)
    std::string program_name = std::filesystem::path(argv[0]).filename().string();

    if (argc < 2 || std::string(argv[1]) == "--help")
    {
        std::cout << "Usage: " << program_name << " <timestamp> [<timestamp>...]" << std::endl
                  << "Converts timestamps in HH:MM:SS:Frames format (23.976 fps) to milliseconds." << std::endl
                  << "Example: " << program_name << " 00:03:06:02 00:01:15:12" << std::endl;
        return 0;
    }

    std::vector<std::chrono::milliseconds> results;
    results.reserve(argc - 1);

    try
    {
        for (int i = 1; i != argc; ++i)
        {
            std::string timestamp(argv[i]);
            auto milliseconds = timestamp_to_milliseconds(timestamp);
            results.emplace_back(milliseconds);
        }

        std::sort(results.begin(), results.end());

        for (const auto& result : results) std::cout << result.count() << std::endl;
    }
    catch (const std::exception& e)
    {
        std::cerr << "Error: " << e.what() << std::endl;
        return 1;
    }

    return 0;
}