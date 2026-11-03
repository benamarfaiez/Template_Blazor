namespace MonProjet.Client.Components.UI.Collections.State;

public class GridPaginationState
{
    public int PageSize { get; set; } = 10;
    public int CurrentPage { get; private set; } = 1;
    public int TotalItems { get; private set; }
    public int TotalPages => TotalItems == 0 ? 1 : (int)Math.Ceiling(TotalItems / (double)PageSize);

    public void UpdateTotalItems(int total)
    {
        TotalItems = total;
        AdjustPage();
    }

    public void NextPage()
    {
        if (CurrentPage < TotalPages) CurrentPage++;
    }

    public void PreviousPage()
    {
        if (CurrentPage > 1) CurrentPage--;
    }

    public void FirstPage() => CurrentPage = 1;

    public void LastPage() => CurrentPage = TotalPages;

    public void GoToPage(int page)
    {
        if (page >= 1 && page <= TotalPages) CurrentPage = page;
    }

    public void Reset() => CurrentPage = 1;

    private void AdjustPage()
    {
        if (CurrentPage > TotalPages) CurrentPage = TotalPages;
    }
}