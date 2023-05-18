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

    public bool IsFilterered => !string.IsNullOrEmpty(CandidateSearchParameters.FirstName)
        || !string.IsNullOrEmpty(CandidateSearchParameters.FirstName) 
        || CandidateSearchParameters.Companies.Count>0
        || CandidateSearchParameters.Projects.Count > 0;

}
