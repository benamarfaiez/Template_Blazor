namespace MonProjet.Client.Helpers;

public static class FileIconHelper
{
    public static string GetIcon(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) return "📎";

        var ext = Path.GetExtension(fileName).ToLowerInvariant();

        return ext switch
        {
            ".pdf" => "📄",
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".svg" => "🖼️",
            ".xls" or ".xlsx" or ".csv" => "📊",
            ".doc" or ".docx" => "📝",
            ".zip" or ".rar" or ".7z" => "🗜️",
            ".mp3" or ".wav" => "🎵",
            ".mp4" or ".avi" => "🎬",
            _ => "📎"
        };
    }
}
