using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;
using static Google.Apis.Requests.BatchRequest;

namespace CandidateBrowserCleanArch.Blazor.WASM;

public class UserService : BaseHttpService, IUserService
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public UserService(ICandidateBrowserWebAPIClient client,
        ITokenService tokenService,
        IConfiguration configuration,
        IMapper mapper,
        IAuthenticationService authenticationService)
        : base(client, 
            tokenService, 
            configuration)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
    }

    public async Task<Response<IEnumerable<ReadUserListDto>>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var response = new Response<IEnumerable<ReadUserListDto>>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.UsersGETAsync(ApiVersion);
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
            response = ConvertApiExceptions<IEnumerable<ReadUserListDto>>(ex);
        }
        return response;
    }

    public async Task<Response<ReadUserDetailsDto>> GetUsersDetailsAsync(string userId)
    {
        var response = new Response<ReadUserDetailsDto>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var resultApi = await _client.ByIdAsync(userId,ApiVersion);
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
            response = ConvertApiExceptions<ReadUserDetailsDto>(ex);
        }
        return response;
    }

    public async Task<Response<bool>> UpdateUserAsync(EditUserViewModel model)
    {
        var response = new Response<bool>();
        try
        {
            if (await GetBearerTokenAsync())
            {
                var user=_mapper.Map<UpdateUserDto>(model);
                var resultApi = await _client.UsersPUTAsync(model.Id, ApiVersion, user);
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
            response = ConvertApiExceptions<bool>(ex);
        }
        return response;
    }
}
