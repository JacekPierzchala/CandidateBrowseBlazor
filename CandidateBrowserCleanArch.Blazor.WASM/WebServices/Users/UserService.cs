using AutoMapper;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Authenication;
using CandidateBrowserCleanArch.Blazor.WASM.WebServices.Base;

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
                var resultApi = await _client.UsersAsync(ApiVersion);
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

    public async Task<Response<ReadUserListDto>> GetUsersDetailsAsync(string userId)
    {
        throw new NotImplementedException();
    }
}
