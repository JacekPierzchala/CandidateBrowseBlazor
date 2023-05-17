namespace CandidateBrowserCleanArch.Blazor.WASM.StateContainers;

public class CandidateSearchParameters
{
    public int PageNumber { get; set; } = 1;
    public int PageSize{ get; set; } = 5;

    public int TotalPages { get; set; }
    public int TotalCount { get; set; }


    private string? _firstName;
    public string? FirstName
    {
        get { return _firstName; }
        set { _firstName = string.IsNullOrEmpty(value?.ToString().Trim()) ? null : value; }
    }

    private string? _lastName;
    public string? LastName
    {
        get { return _lastName; }
        set { _lastName = string.IsNullOrEmpty(value?.ToString().Trim()) ? null : value; }
    }

    public IList<int> Companies { get; set; } = new int[] { };
    public IList<int> Projects { get; set; } = new int[] { };
}