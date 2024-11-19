using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

public static class LocalizerManager
{
    private static Dictionary<string, Dictionary<string, string>> localizerDictionaries = new Dictionary<string, Dictionary<string, string>>();

    public static void LoadFromXML(string filePath)
    {
        var serializer = new XmlSerializer(typeof(Dictionary<string, Dictionary<string, string>>));
        using (var reader = new StreamReader(filePath))
        {
            object? deserializedContent = serializer.Deserialize(reader);
            if (deserializedContent is Dictionary<string, Dictionary<string, string>> data)
            {
                localizerDictionaries = data;
            }
            else
            {
                throw new Exception("Invalid XML structure.");
            }
        }
    }

    public static void LoadFromCSV(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            string[]? headers = reader.ReadLine()?.Split(';');
            if (headers == null) throw new Exception("Invalid CSV file format.");

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;

                var values = line.Split(';');
                string key = values[0];

                for (int i = 1; i < headers.Length; i++)
                {
                    string language = headers[i];
                    if (!localizerDictionaries.ContainsKey(language))
                    {
                        localizerDictionaries[language] = new Dictionary<string, string>();
                    }
                    localizerDictionaries[language][key] = values[i];
                }
            }
        }
    }

    public static void LoadFromJSON(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        localizerDictionaries = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonData)
            ?? throw new Exception("couldn't deserialize JSON.");
    }

    public static string GetWord(string culture, string key)
    {
        if (localizerDictionaries.TryGetValue(culture, out var dictionary))
            if (dictionary.TryGetValue(key, out var word))
                return word;

        throw new Exception("culture or key is not valid.");
    }
}