using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using CandidateBrowserCleanArch.Blazor.WASM.ViewModels;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace CandidateBrowserCleanArch.Blazor.WASM.Pages.Auth;

public partial class ResetPassword:ComponentBase
{
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] IAuthenticationService AuthenticationService { get; set; }

    public string? Token;
    public string? UserId;

    ResetPasswordViewModel ResetPasswordViewModel = new();
    bool ResetProcessPending;
    bool ResetProcessError;

    string message = string.Empty;
    string messageTitle = string.Empty;

    private async Task HandleResetPassword()
    {
        StateHasChanged();
        ResetProcessPending = true;
        ResetProcessError = false;

        var url = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var queryStrings = QueryHelpers.ParseQuery(url.Query);
        if (queryStrings.TryGetValue("userId", out var userId))
        {
            ResetPasswordViewModel.UserId = userId;
        }
        if (queryStrings.TryGetValue("token", out var token))
        {
            ResetPasswordViewModel.Token = token;
        }

        var response = await AuthenticationService.ResetPasswordAsync(ResetPasswordViewModel);
        if (response.Success)
        {
            NavigationManager.NavigateTo(UrlStatics.login);
        }
        else
        {
            message = response.ValidationErrors;
            messageTitle = response.Message;
            ResetProcessError = true;
        }
        ResetProcessPending = false;

        StateHasChanged();

    }

    private void BackToLogin()
    {
        NavigationManager.NavigateTo(UrlStatics.login);
    }
}
