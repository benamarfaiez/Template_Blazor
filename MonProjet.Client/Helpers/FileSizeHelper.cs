using System.Globalization;

namespace MonProjet.Client.Helpers;

/// <summary>
/// Helper responsable du formatage des tailles de fichiers.
/// Respecte le SRP : ne fait que des calculs de taille.
/// </summary>
public static class FileSizeHelper
{
    private static readonly string[] Suffixes = ["o", "Ko", "Mo", "Go", "To"];

    public static string Format(long bytes)
    {
        if (bytes == 0) return "0 o";

        int counter = 0;
        decimal number = bytes;

        while (number >= 1024 && counter < Suffixes.Length - 1)
        {
            number /= 1024;
            counter++;
        }

        return string.Format(CultureInfo.CurrentCulture, "{0:n1} {1}", number, Suffixes[counter]);
    }
}