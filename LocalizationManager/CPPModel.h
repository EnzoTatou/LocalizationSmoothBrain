#include <iostream>
#include <string>
#include <unordered_map>

static class LocalizerManager
{
private:
    static std::unordered_map<std::string, std::unordered_map<std::string, std::string>> localizerMaps = new std::unordered_map<std::string, std::unordered_map<std::string, std::string>>()
    {
/**/
    };
}