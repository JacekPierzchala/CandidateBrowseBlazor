using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace CandidateBrowserCleanArch.Blazor.WASM.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;


    public ApiAuthenticationStateProvider(ITokenService tokenService)
             => _tokenService = tokenService;

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var tokenValidationResult = await _tokenService.ValidateTokenAsync();
        if(tokenValidationResult.Item1==TokenValidationResult.Empty||
            tokenValidationResult.Item1 == TokenValidationResult.Expired)
        {
            return new AuthenticationState(user);
        }
        var claims = await _tokenService.GetClaimsFromToken();

        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
    }


    public async Task LoggedIn(string token, string refreshToken)
    {
        await _tokenService.SetTokenAsync(token, refreshToken);
        var claims = await _tokenService.GetClaimsFromToken();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState =  Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _tokenService.RemoveTokenAsync();
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }
}
