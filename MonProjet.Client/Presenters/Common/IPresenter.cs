namespace MonProjet.Client.Presenters.Common;

public interface IPresenter<in TView> where TView : class, IView
{
    void SetView(TView view);
}