using Blazored.LocalStorage;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public class CompaniesService : BaseHttpService, ICompaniesService
{
    private readonly IAuthenticationService _authenticationService;
    private IEnumerable<ReadCompanyDto> _companies;
    public CompaniesService(ICandidateBrowserWebAPIClient client, 
        IAuthenticationService authenticationService, ITokenService 
        tokenService, IConfiguration configuration) 
        : base(client, tokenService, configuration)
        => _authenticationService = authenticationService;

    public async Task<Response<IEnumerable<ReadCompanyDto>>> GetAllActiveCompaniesAsync(bool overWrite)
    {
        var response=new Response<IEnumerable<ReadCompanyDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                if(_companies==null || overWrite)
                {
                    var result = await _client.CompaniesAsync(ApiVersion);
                    _companies = result.Data;                    
                }  
                response.Data = _companies;                             
                response.Success = true;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<IEnumerable<ReadCompanyDto>>(ex);
        }    
        return response;
    }
}
