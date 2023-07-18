using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Auth;
public partial class Register:ComponentBase
{
    [Inject] IAuthenticationService AuthenticationService { get; set; }
    [Inject]
    NavigationManager NavigationManager { get; set; }
    [Inject] NotificationService NotificationService { get; set; }

    RegisterViewModel RegisterViewModel = new();
    string message = string.Empty;
    string messageTitle = string.Empty;
    bool RegisterProcessPending;
    bool RegisterProcessError;
    bool RegisterProcessSuccess;

    private async Task HandleRegister()
    {
        RegisterProcessPending = true;
        RegisterProcessError = false;
        RegisterProcessSuccess = false;
        message = "Registering ...";
        StateHasChanged();

        var response = await AuthenticationService.RegisterAsync(RegisterViewModel);
        if (response.Success)
        {

            NotificationService
           .Notify(new NotificationMessage
           {
               Severity = NotificationSeverity.Success,
               Duration = 4000,
               Summary = "Success",
               Detail = $"Registration link has been to {RegisterViewModel.Email}"
           });

            RegisterViewModel = new();
            RegisterProcessSuccess = true;
            RegisterProcessPending = false;
            RouteToLogin();
        }
        else
        {
            message = response.ValidationErrors;
            messageTitle = response.Message;
            RegisterProcessError = true;
        }
        RegisterProcessPending = false;

        StateHasChanged();
    }

    private async Task HandleGoogleLogin()
    {
        message = "Routing to Google authentication...";
        RegisterProcessPending = true;
        var response = await AuthenticationService.GetGoogleAuthUrlAsync();
        if (!response.Success)
        {
            NotificationService.Notify(new NotificationMessage() { Detail = response.Message });
        }
        NavigationManager.NavigateTo(response.Data, false);
    }

    private void RouteToLogin()
    {
        NavigationManager.NavigateTo(UrlStatics.login);
    }

    private void RouteToResendConfirmation()
    {
        NavigationManager.NavigateTo(UrlStatics.resendConfirmation);
    }
}
