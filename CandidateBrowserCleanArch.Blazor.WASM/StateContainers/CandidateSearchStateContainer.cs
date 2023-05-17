namespace CandidateBrowserCleanArch.Blazor.WASM.StateContainers;

public class CandidateSearchStateContainer
{
    public  Action? SearchTrigerred;
    public CandidateSearchParameters CandidateSearchParameters { get; set; }

    public CandidateSearchStateContainer()
    {
        CandidateSearchParameters = new();
    }

    public void ClearSearchParameters()
    {
        CandidateSearchParameters = new();
    }

}
