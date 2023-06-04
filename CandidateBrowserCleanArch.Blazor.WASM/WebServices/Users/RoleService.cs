using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class RoleService : BaseHttpService, IRoleService
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public RoleService(ICandidateBrowserWebAPIClient client, 
        ITokenService tokenService, 
        IConfiguration configuration,
        IMapper mapper,
        IAuthenticationService authenticationService) 
        : base(client, tokenService, configuration)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public async Task<Response<IEnumerable<RoleDto>>> GetRolesAsync()
    {
        var response = new Response<IEnumerable<RoleDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.RolesAsync(ApiVersion);
                response.Success = resultApi.Success;
                response.Data = resultApi.Data;
            }
            else
            {
                await _authenticationService.LogOut();
            }
        }
        catch (ApiException ex)
        {
            response = ConvertApiExceptions<IEnumerable<RoleDto>>(ex);
        }
        return response;
    }
}