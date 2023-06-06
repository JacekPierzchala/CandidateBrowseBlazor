using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared.Candidates;

public partial class  CandidateDetailsMainInfo:ComponentBase
{

    [Inject] ICandidatesService CandidatesService { get; set; }
    [Inject] NotificationService NotificationService { get; set; }
    [Inject] IMapper mapper { get; set; }
    [Inject] IAuthorizationService AuthorizationService { get; set; }
    [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }
    [Parameter] public CandidateDetailsDto Candidate { get; set; }

    bool mainInfoDialogOpen;
    CandidateEditViewModel candidateEdit;
    CandidateDetailsForAdminDto candidateAdminInfo;
    bool updatePending;

    protected async override Task OnInitializedAsync()
    {
        var authenticationState = await authenticationStateTask;
        if ((bool)authenticationState?.User?.Identity.IsAuthenticated
        && (await AuthorizationService.AuthorizeAsync(authenticationState?.User, PermissionStatics.UserUpdate)).Succeeded)
        {
            var resposne = await CandidatesService.GetCandidateAdminDetailsAsync(Candidate.Id);
            if (resposne.Success)
            {
                candidateAdminInfo = resposne.Data;
            }
        }
    }

    public async Task EditCandidateAsync()
    {
        candidateEdit = mapper.Map<CandidateEditViewModel>(Candidate);
        mainInfoDialogOpen = true;
    }

    private async Task OnCloseAsync()
    {
        mainInfoDialogOpen = false;
    }

    private async Task OnSaveCandidateAsync(CandidateEditViewModel candidate)
    {
        if (candidate.Id != null && candidate.Id > 0)
        {
            updatePending = true;
            var response = await CandidatesService.UpdateCandidateMainInfoAsync(candidate);
            if (response.Success)
            {
                Candidate = response.Data;
                NotificationService
               .Notify(new NotificationMessage
               {
                   Severity = NotificationSeverity.Success,
                   Duration = 4000,
                   Summary = "Success",
                   Detail = $"{Candidate.FirstName} {Candidate.LastName} details updated"
               });
            }
            else
            {
                NotificationService
               .Notify(new NotificationMessage
               {
                   Severity = NotificationSeverity.Error,
                   Duration = 4000,
                   Summary = "Error",
                   Detail = $"Update has failed:{response.Message}"
               });
            }
            updatePending = false;
            mainInfoDialogOpen = false;
        }

    }

}
