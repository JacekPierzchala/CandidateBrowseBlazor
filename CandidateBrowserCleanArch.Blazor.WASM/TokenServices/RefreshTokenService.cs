using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidateBrowserCleanArch.Blazor.WASM.Services
{
    public class RefreshTokenService
    {
        private readonly AuthenticationStateProvider _stateProvider;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;
        SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public RefreshTokenService(AuthenticationStateProvider stateProvider,
            IAuthenticationService authenticationService ,ITokenService tokenService)
        {
            _stateProvider = stateProvider;
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        public async Task<string>TryRefreshToken()
        {
            var authState =await _stateProvider.GetAuthenticationStateAsync();
            var user=authState.User;

            var exp = user?.FindFirst(c => c.Type.Equals(TokenStatics.Exp))?.Value;
            if(exp != null) 
            {
                var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
                var timeUTC = DateTime.UtcNow;
                var diff = expTime - timeUTC;
                if (diff.TotalMinutes <= 2)
                {
                    await _semaphoreSlim.WaitAsync();
                    var result =  await _authenticationService.RefreshToken();
                    if(result.Success)
                    {
                        await _tokenService.SetTokenAsync(result.Data.Token, result.Data.RefreshToken);
                    }
                    else
                    {
                        await _tokenService.RemoveTokenAsync();
                    }
                    _semaphoreSlim.Release();
                    return result.Data?.Token?? string.Empty ;
                }                   
            }
            return string.Empty;
        }
    }
}
