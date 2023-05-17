using System.Net.Http.Headers;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

public abstract class BaseHttpService
{
    protected readonly ICandidateBrowserWebAPIClient _client;
    protected readonly ITokenService _tokenService;

    public string ApiVersion { get; set; }
    public BaseHttpService(ICandidateBrowserWebAPIClient client,
        ITokenService tokenService, IConfiguration configuration)
    {
        _client = client;
        _tokenService = tokenService;
        ApiVersion = configuration[nameof(ApiVersion)];
    }

    public async Task<bool> GetBearerTokenAsync()
    {
        var tokenValidation = await _tokenService.ValidateTokenAsync();
        if (tokenValidation.Item1 == TokenValidationResult.Valid)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TokenStatics.Bearer, tokenValidation.Item2);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
    {
        if (apiException.StatusCode == 400)
        {
            return new Response<Guid>() 
                { Message = "Validation errors occured", 
                    ValidationErrors = apiException.Response.Replace("[","").Replace("]", "").Replace("\"", ""),
                
                Success = false };
        }
        if (apiException.StatusCode == 404)
        {
            return new Response<Guid>() { Message = "The requested item could not be found", ValidationErrors = apiException.Response, Success = false };
        }
        if (apiException.StatusCode == 401)
        {
            return new Response<Guid>()
            {
                Message = "Authorization attempt failed. Please try again",
                ValidationErrors = apiException.Response.Split(",").Any(c => c.Contains("ErrorMessage"))?
                                    apiException.Response.Split(",").FirstOrDefault(c => c.Contains("ErrorMessage")).
                                        Replace("ErrorMessage", "")
                                        .Replace("\"", "")
                                        .Replace("}", "")
                                        .Replace(":", ""):"",
                Success = false
            };
        }
        if (apiException.StatusCode == 501)
        {
            return new Response<Guid>() { 
                Message= apiException.Response.Split(",").Any(c => c.Contains("ErrorMessage")) ?
                                    apiException.Response.Split(",").FirstOrDefault(c => c.Contains("ErrorMessage")).
                                        Replace("ErrorMessage", "")
                                        .Replace("\"", "")
                                        .Replace("}", "")
                                        .Replace(":", "") : "",


            };
        }
    
            return new Response<Guid>() { Message = "Something went wrong, please try again", ValidationErrors= apiException.Message, Success = false };
    }


}
