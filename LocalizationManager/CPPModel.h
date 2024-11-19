#include <iostream>
#include <string>
#include <unordered_map>

class LocalizerManager
{
private:
    static std::unordered_map<std::string, std::unordered_map<std::string, std::string>> localizerMaps;
}

std::unordered_map<std::string, std::unordered_map<std::string, std::string>> LocalizerManager::localizerMaps = {
/**/