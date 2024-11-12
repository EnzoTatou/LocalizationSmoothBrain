using System.Collections.Generic;

public static class LocalizerManager
{
    private static Dictionary<string, Dictionary<string, string>> localizerDictionaries = new Dictionary<string, Dictionary<string, string>>()
    {
/**/
    };
    private static string culture = string.Empty;

    /// <summary>
    /// return the count of language stored
    /// </summary>
    public static int LanguageCount { get => localizerDictionaries.Count; }

    /// <summary>
    /// Return the total of elements of the key
    /// Return -1 if the key doesn't exist
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public static int KeyCount(string language)
    {
        if (language == null || !localizerDictionaries.ContainsKey(language))
            return -1;

        return localizerDictionaries[language].Count;
    }

    /// <summary>
    /// return the string based on the language and key
    /// </summary>
    /// <param name="language"></param>
    /// <param name="stringKey"></param>
    /// <returns></returns>
    public static string LocalizedStringOnLanguage(string language, string stringKey)
    {
        if (language == null || !localizerDictionaries.ContainsKey(language) || stringKey == null)
            return string.Empty;

        return localizerDictionaries[language][stringKey];
    }

    /// <summary>
    /// return the string based on the culture set and the params key
    /// </summary>
    /// <param name="language"></param>
    /// <param name="stringKey"></param>
    /// <returns></returns>
    public static string LocalizedString(string stringKey)
    {
        if (culture == null || !localizerDictionaries.ContainsKey(culture) || stringKey == null)
            return string.Empty;

        return localizerDictionaries[culture][stringKey];
    }

    /// <summary>
    /// Set the culture
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public static bool SetCulture(string language)
    {
        if (!localizerDictionaries.ContainsKey(language))
            return false;

        culture = language;
        return true;
    }
}