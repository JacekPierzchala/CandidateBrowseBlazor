using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    public TokenService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _jwtSecurityTokenHandler = new ();
    }
    public async Task<List<Claim>> GetClaimsFromToken()
    {
        var savedToken = await GetTokenAsync();
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken.token);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }

    public async Task<(string token,string refreshToken)> GetTokenAsync()
    {
        var token =await _localStorageService.GetItemAsync<string>(TokenStatics.AccessToken);
        var refreshToken =await _localStorageService.GetItemAsync<string>(TokenStatics.RefreshToken);
        return(token, refreshToken);
    }

    public async Task<(TokenValidationResult, string?)> ValidateTokenAsync()
    {
        var savedToken = await GetTokenAsync();
        if (savedToken.token == null)
        {
            return (TokenValidationResult.Empty,null);
        }
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken.token);

        if (tokenContent.ValidTo < DateTime.UtcNow)
        {
            await RemoveTokenAsync();
            return (TokenValidationResult.Expired,null);
        }
        return (TokenValidationResult.Valid, savedToken.token);
    }
    public async Task RemoveTokenAsync()
    {
        await _localStorageService.RemoveItemAsync(TokenStatics.AccessToken);
        await _localStorageService.RemoveItemAsync(TokenStatics.RefreshToken);
    }

    public async Task SetTokenAsync(string token, string refreshToken)
    {
        await _localStorageService.SetItemAsync(TokenStatics.AccessToken, token);
        await _localStorageService.SetItemAsync(TokenStatics.RefreshToken, refreshToken);
    }

    public async Task<string> PrepareAuthCode(string code)
    {
        code = HttpUtility.UrlDecode(code);
        //var t= code.Substring(code.IndexOf("code=") + ("code=").Length, 20);
        //var i =  code.IndexOf("scope=") - ("scope=").Length - code.IndexOf("code=")+1;
        //return code.Substring(code.IndexOf("code=") + ("code=").Length,i);
        return code.Substring(code.IndexOf("code=") + ("code=").Length, 
            code.IndexOf("scope=") - ("scope=").Length - code.IndexOf("code=") + 1);
       
    }
}
