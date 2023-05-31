using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Auth;

public partial class ResendConfirmation:ComponentBase
{
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] IAuthenticationService AuthenticationService { get; set; }
    [Inject] NotificationService NotificationService { get; set; }

    ResendConfirmationViewModel ResendConfirmationViewModel = new();
    bool ResendProcessPending;
    bool ResendProcessError;
    bool ResendProcessSuccess;
    string message = string.Empty;
    string messageTitle = string.Empty;

    private async Task HandleResend()
    {
        ResendProcessPending = true;
        ResendProcessError = false;
        ResendProcessSuccess = false;
        StateHasChanged();

        var response = await AuthenticationService.ResendConfirmationAsync(ResendConfirmationViewModel);
        if (response.Success)
        {
            NotificationService
            .Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Duration = 4000,
                Summary = response.Message,
                Detail = $"Registration link has been to {ResendConfirmationViewModel.Email}"
            });
            ResendProcessPending = false;
            NavigationManager.NavigateTo(UrlStatics.login);
        }
        else
        {
            message = response.ValidationErrors;
            messageTitle = response.Message;
            ResendProcessError = true;
        }
        ResendProcessPending = false;

        StateHasChanged();
    }

    private void BackToRegister()
    {
        NavigationManager.NavigateTo(UrlStatics.register);
    }
}
