using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Auth;
public partial class ForgotPassword:ComponentBase
{
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] IAuthenticationService AuthenticationService { get; set; }
    [Inject] NotificationService NotificationService { get; set; }

    ForgotPasswordViewModel ForgotPasswordViewModel = new();
    bool ResendProcessPending;
    bool ResendProcessError;
    bool ResendProcessSuccess;
    string message = string.Empty;
    string messageTitle = string.Empty;

    private async Task HandleForgotPassword()
    {
        ResendProcessPending = true;
        ResendProcessError = false;
        ResendProcessSuccess = false;
        StateHasChanged();

        var response = await AuthenticationService.ForgotPasswordAsync(ForgotPasswordViewModel);
        if (response.Success)
        {
            ResendProcessPending = false;
            NotificationService
            .Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Duration = 4000,
                Summary = response.Message,
                Detail = $"Reset link has been sent to {ForgotPasswordViewModel.Email}"
            });
            ResendProcessSuccess = true;
            BackToLogin();
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

    private void BackToLogin()
    {
        NavigationManager.NavigateTo(UrlStatics.login);
    }
}
