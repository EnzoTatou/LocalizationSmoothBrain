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


//    // Define and initialize the static member outside the class
//std::unordered_map<std::string, std::unordered_map<std::string, std::string>> LocalizerManager::localizerMaps = {
//    { "en", { {"test", "PUWET"} } },
//    { "fr", { {"test", "POUET"} } },
//    { "es", { {"test", "SENORA"} } },
//    { "ja", { {"test", "YAYAYAY"} } }
//};