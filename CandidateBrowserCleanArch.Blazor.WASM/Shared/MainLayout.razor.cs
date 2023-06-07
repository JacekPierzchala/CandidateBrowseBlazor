using CandidateBrowserCleanArch.Blazor.WASM.Providers;
using CandidateBrowserCleanArch.Blazor.WASM.Statics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidateBrowserCleanArch.Blazor.WASM.Shared;

public partial class MainLayout: IDisposable
{
    [Inject] IUserSettingsProvider UserSettingsService { get; set; }
    [Inject] ApiAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    AuthenticationState authenticationState;
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
    private string selectedTheme = ThemeStatics.Light;


    protected async override Task OnInitializedAsync()
    {
        UserSettingsService.SelectedThemeChange += OnSelectedThemeChange;
        AuthenticationStateProvider.AuthChange += async () => await HandleAuthenticationStateChanged();

        await GetTheme();
    }



    private string GetCssClass()
    {
        return selectedTheme == ThemeStatics.Light ? string.Empty : "dark-mode";
    }

    private async Task GetTheme()
    {
        string user = null;
        authenticationState = await authenticationStateTask;
        if (authenticationState.User.Identity.IsAuthenticated)
        {
            user = authenticationState.User.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.Uid).Value;
            await UserSettingsService.GetTheme(user);
        }
    }

    private async Task HandleAuthenticationStateChanged()
    {
        await GetTheme();
    }

    private void OnSelectedThemeChange(string theme)
    {
        selectedTheme = theme;
        StateHasChanged();
    }

    public void Dispose()
    {
        UserSettingsService.SelectedThemeChange -= OnSelectedThemeChange;
        AuthenticationStateProvider.AuthChange -= async () => await HandleAuthenticationStateChanged();
    }


}
