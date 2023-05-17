using Blazored.LocalStorage;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace CandidateBrowserCleanArch.Blazor.WASM.WebServices;

public class ProjectsService : BaseHttpService, IProjectsService
{
    private readonly IAuthenticationService _authenticationService;
    private IEnumerable<ReadProjectDto> _projects;
    public ProjectsService(ICandidateBrowserWebAPIClient client,
        IAuthenticationService authenticationService,
        ITokenService tokenService, IConfiguration configuration)
        : base(client, tokenService, configuration)
        => _authenticationService = authenticationService;

    public async Task<Response<IEnumerable<ReadProjectDto>>> GetAllActiveProjectsAsync(bool overWrite)
    {
        var response = new Response<IEnumerable<ReadProjectDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                if (_projects == null || overWrite)
                {
                    var result = await _client.ProjectsAsync(ApiVersion);
                    _projects = result.Data;
                }
                response.Data = _projects;
                response.Success = true;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<IEnumerable<ReadProjectDto>>(ex);
        }
        return response;
    }
}
