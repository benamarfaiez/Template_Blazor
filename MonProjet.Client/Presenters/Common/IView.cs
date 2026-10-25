namespace MonProjet.Client.Presenters.Common;

public interface IView
{
    void ShowError(string message);
    void ShowLoading(bool isLoading);
}