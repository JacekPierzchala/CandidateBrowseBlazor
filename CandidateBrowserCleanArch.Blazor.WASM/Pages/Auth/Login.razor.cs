using CandidateBrowserCleanArch.Blazor.WASM.Pages.Users;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Auth;
public partial class Login:ComponentBase
{

    [Inject] IUserSettingsProvider UserSettingsProvider { get; set; }
    [Inject] IAuthenticationService AuthenticationService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] NotificationService NotificationService { get; set; }
  

    LoginViewModel LoginViewModel = new();
    string message = string.Empty;
    string messageTitle = string.Empty;
    bool LoggingProcessPending;
    bool LoggingProcessError;

    private async Task HandleLogin()
    {
        LoggingProcessPending = true;
        LoggingProcessError = false;
        StateHasChanged();

        var response = await AuthenticationService.AuthenticateAsync(LoginViewModel);
        if (response.Success)
        {
            NavigationManager.NavigateTo(UrlStatics.home);
        }
        else
        {
            message = response.ValidationErrors;
            messageTitle = response.Message;
            LoggingProcessError = true;
        }
        LoggingProcessPending = false;

        StateHasChanged();
    }

    private void RouteToRegister()
    {
        NavigationManager.NavigateTo(UrlStatics.register);
    }

    private async Task HandleGoogleLogin()
    {
        var response = await AuthenticationService.GetGoogleAuthUrlAsync();
        if (!response.Success)
        {
            NotificationService.Notify(new NotificationMessage() { Detail = response.Message });
        }
        NavigationManager.NavigateTo(response.Data, false);
    }

    private void RouteToForgotPassword()
    {
        NavigationManager.NavigateTo(UrlStatics.forgotPassword);
    }
}
