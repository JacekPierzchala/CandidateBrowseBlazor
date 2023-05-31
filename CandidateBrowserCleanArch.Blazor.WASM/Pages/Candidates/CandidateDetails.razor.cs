using CandidateBrowserCleanArch.Blazor.WASM.Services;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Candidates;

public partial class CandidateDetails:ComponentBase, IDisposable
{

    [Inject]
    ICandidatesService CandidatesService { get; set; }

    [Inject]
    HttpInterceptorService HttpInterceptorService { get; set; }

    [Parameter]
    public int Id { get; set; }

    public CandidateDetailsDto Candidate { get; set; }
    bool loadDataResult;
    bool loadFinished;
    string message = string.Empty;

    protected async override Task OnInitializedAsync()
    {
        loadFinished = false;
        HttpInterceptorService.RegisterEvent();

        var request = await CandidatesService.GetCandidateDetailsAsync(Id);
        loadDataResult = request.Success;
        Candidate = request.Data;
        message = request.Message;
        loadFinished = true;
    }

    public void Dispose()
    {
        HttpInterceptorService.DisposeEvent();
    }
}
