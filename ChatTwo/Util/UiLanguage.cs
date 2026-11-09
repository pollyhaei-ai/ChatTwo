using System.Globalization;

namespace ChatTwo.Util;

/// <summary>
/// Maps Dalamud UI language codes onto cultures that match satellite .resx assemblies.
/// Dalamud uses <c>tw</c> for Traditional Chinese and <c>zh</c> for Simplified Chinese.
/// <see cref="CultureInfo"/> treats <c>tw</c> as Twi, so it must be rewritten before use.
/// </summary>
public static class UiLanguage
{
    public static CultureInfo Resolve(string? langCode)
    {
        var name = (langCode ?? string.Empty).Trim();
        if (name.Length == 0)
            return CultureInfo.GetCultureInfo("en");

        var mapped = name.ToLowerInvariant() switch
        {
            "tw" or "zh-tw" or "zh-hk" or "zh-mo" or "zh-hant" or "zht" => "zh-Hant",
            "zh" or "zh-cn" or "zh-sg" or "zh-hans" or "zhs" => "zh-Hans",
            _ => name,
        };

        return CultureInfo.GetCultureInfo(mapped);
    }
}
