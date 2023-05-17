using System.Security.Claims;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public enum TokenValidationResult
{
    Empty,Expired,Valid
}
public interface ITokenService
{
    Task<string>PrepareAuthCode(string code);
    Task<(string token, string refreshToken)> GetTokenAsync();
    Task SetTokenAsync(string token, string refreshToken);
    Task<(TokenValidationResult,string?)> ValidateTokenAsync();
    Task<List<Claim>> GetClaimsFromToken();
    Task RemoveTokenAsync();
}
