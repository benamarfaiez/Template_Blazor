using Microsoft.Extensions.Localization;

namespace MonProjet.Client.Helpers;

public static class LocalizationHelper
{
    /// <summary>
    /// Traduit un Enum en cherchant une clé "Type_Value" (ex: "DateRangePreset_Today").
    /// </summary>
    public static string Translate<T>(this IStringLocalizer<T> localizer, Enum value)
    {
        var key = $"{value.GetType().Name}_{value}";
        var translation = localizer[key];

        return translation.ResourceNotFound ? value.ToString() : translation.Value;
    }
}