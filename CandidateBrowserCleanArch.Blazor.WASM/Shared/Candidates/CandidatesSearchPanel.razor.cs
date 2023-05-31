using CandidateBrowserCleanArch.Blazor.WASM.Services;
using CandidateBrowserCleanArch.Blazor.WASM.StateContainers;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;

public partial class CandidatesSearchPanel : ComponentBase, IDisposable
{
    [Inject] CandidateSearchStateContainer CandidateSearchStateContainer { get; set; }
    [Inject] ICompaniesService CompaniesService { get; set; }
    [Inject] IProjectsService ProjectsService { get; set; }
    [Inject] HttpInterceptorService HttpInterceptorService { get; set; }

    IEnumerable<ReadCompanyDto> Companies;
    IEnumerable<ReadProjectDto> Projects;

    private void RaiseSearchStarted()
    {
        CandidateSearchStateContainer.CandidateSearchParameters.PageNumber = 1;
        CandidateSearchStateContainer.SearchTrigerred?.Invoke();
    }

    protected async override Task OnInitializedAsync()
    {
        HttpInterceptorService.RegisterEvent();
        var resultCompany = await CompaniesService.GetAllActiveCompaniesAsync(false);
        if (resultCompany.Success)
        {
            Companies = resultCompany?.Data;
        }

        var resultProject = await ProjectsService.GetAllActiveProjectsAsync(false);
        if (resultProject.Success)
        {
            Projects = resultProject?.Data;
        }

        StateHasChanged();
    }

    private void ResetFilters()
    {
        CandidateSearchStateContainer.ClearSearchParameters();
        StateHasChanged();
    }

    public void Dispose()
    {
        HttpInterceptorService.DisposeEvent();
    }
}
