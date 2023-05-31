using CandidateBrowserCleanArch.Blazor.WASM.Services;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;

public partial class CandidatesList:ComponentBase, IDisposable
{
    [Inject] ICandidatesService CandidatesService { get; set; }
    [Inject] CandidateSearchStateContainer CandidateSearchStatecontainer { get; set; }
    [Inject] HttpInterceptorService HttpInterceptorService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    [Parameter]
    public EventCallback<Tuple<bool, bool>> LoadDataChanged { get; set; }
    bool LoadDataSuccess { get; set; }
    private string ErrorMessage = string.Empty;
    IEnumerable<CandidateListDto>? Candidates;

    protected async override Task OnInitializedAsync()
    {
        await LoadDataChanged.InvokeAsync(Tuple.Create(false, CandidateSearchStatecontainer.IsFilterered));
        HttpInterceptorService.RegisterEvent();
        CandidateSearchStatecontainer.SearchTrigerred += async () => await SearchAsync();
        await SearchAsync();

    }
    private async Task SearchAsync()
    {
        Candidates = null;
        StateHasChanged();
        var result = await CandidatesService.GetActiveCandidatesAsync
                    (CandidateSearchStatecontainer.CandidateSearchParameters);
        LoadDataSuccess = result.Success;
        if (result.Success)
        {
            Candidates = result.Data;
        }
        else
        {
            Candidates = new List<CandidateListDto>();
            ErrorMessage = result.Message;
        }

        await LoadDataChanged.InvokeAsync(Tuple.Create(true, CandidateSearchStatecontainer.IsFilterered));
        StateHasChanged();
    }

    private async Task SelectCandidate(int candidate)
    {
        NavigationManager.NavigateTo($"/candidate/{candidate}");
    }

    private async Task SelectedPage(int page)
    {
        CandidateSearchStatecontainer.CandidateSearchParameters.PageNumber = page;
        await SearchAsync();
    }

    private async Task SelectedSize(int size)
    {
        CandidateSearchStatecontainer.CandidateSearchParameters.PageSize = size;
        await SearchAsync();
    }

    public void Dispose()
    {
        CandidateSearchStatecontainer.SearchTrigerred -= async () => await SearchAsync();
        HttpInterceptorService.DisposeEvent();
    }

}
